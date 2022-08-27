/*****************************************************************************************
 * File Name     ： csgl.cs
 * Description   ： C# OpenGL 配置文件, 
 * Author        ： Zhanli@Uestc
 * Update Time   ： 2019-03-11
 *****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using ThreeDHeadRebuild;
using ThreeDHeadRebuild.Src;
using ThreeDHeadRebuild.Src.Util;


namespace ThreeDHeadRebuild
{
    public class csgl
    {
        private  OpenGL        glInstance;
        private  OpenGLControl scInstance;
        private  OpenGLTool    glTool;
        public OpenGL getGlInstance(){ return glInstance;}
        int mouseX;
        int mouseY;
        public bool Global_MouseDown      = false;
        public bool Global_MouseInGLPanel = false;
        public bool IsLineDisplay         = false;           // 线框显示
        public bool IsEnableLight         = true;            // 开启光照，默认开启光照 

        public float Model_HeightOffset   = 20;

        double[] matrixProjection = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        const float DEG2RAD    = 3.141593f / 180;
        const float FOV_Y      = 20.0f;              // vertical FOV in degree
        const float NEAR_PLANE = 50.0f;
        const float FAR_PLANE  = 1000.0f;

        /* 初始摄像头位置以及姿态的设置 */
        const float CAMERA_ANGLE_X  =  10.0f;      // 摄像头的俯仰角
        const float CAMERA_ANGLE_Y  =  0.0f;       // 摄像头偏航角
        const float CAMERA_DISTANCE =  100.0f;      // 摄像头距离
        const float CAMERA_HIGHT    = -15.0f;

        public uint[] TextureID = new uint[3];

        float cameraAngleX;
        float cameraAngleY;
        float cameraHight;
        float cameraDistance;
        double[] modelview = { 0.7071068, 0.5, -0.5, 0, 0, 0.7071068, 0.7071068, 0, 0.7071068, -0.0, 0.5, 0, 0, 0, -25, 1 };

        float[] cameraPosition = { 0, 0, 0 };
        float[] cameraAngle = { 0, 0, 0 };
        float[] modelPosition = { 0, 0, 0 };
        float[] modelAngle = { 0, 0, 0 };

        
        bool LoadTextureFlag = false;

        public csgl(OpenGLControl sc)
        {
            scInstance     = sc;
            glInstance     = sc.OpenGL;

            // 配置摄像头信息
            cameraAngleX   = CAMERA_ANGLE_X;
            cameraAngleY   = CAMERA_ANGLE_Y;
            cameraHight    = CAMERA_HIGHT;
            cameraDistance = CAMERA_DISTANCE;

            glTool = new OpenGLTool(this);
            // 初始化OpenGL 配置
            Initialized();

        }

        public void Draw(bool isLoadModel, MorphableModel model)
        {
            OpenGL gl = glInstance;
            // set bottom viewport
            setViewportSub(0, 0, scInstance.Width, scInstance.Height, NEAR_PLANE, FAR_PLANE);

            //gl.ClearColor(bgColor[0], bgColor[1], bgColor[2], bgColor[3]);   // background color
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            gl.LoadIdentity();
            //gl.PushMatrix();

            // 这部分配置摄像机位置和姿态
            gl.Translate(0, cameraHight, -cameraDistance);
            //move camera
            gl.Translate(cameraPosition[0], cameraPosition[1], cameraPosition[2]);

            gl.Rotate(cameraAngleX, 1, 0, 0); // pitch
            gl.Rotate(cameraAngleY, 0, 1, 0); // heading

            // 绘制网格
            glTool.drawGrid(300, 5);

            if (isLoadModel)
            {
                gl.PushAttrib(OpenGL.GL_CURRENT_BIT);
                DrawMorphableModel(model.Head, gl, LoadTextureFlag);
                DrawMorphableModel(model.Eyes, gl, LoadTextureFlag);
                gl.PopAttrib();
            }
        }

        private void DrawMorphableModel(Mesh mesh, OpenGL gl, bool isUseTexture){
            gl.PushMatrix();

            // 如果使用纹理
            if (isUseTexture) {
                //texture.Bind(gl);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID[0]);
                glTool.MaterialConfig(0);
            }

            // 如果不使用线框绘图
            if (IsLineDisplay == false)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID[0]);
                gl.Begin(OpenGL.GL_TRIANGLES);
            }else {
                gl.Begin(OpenGL.GL_LINE_STRIP);
            }
            
            for (int l= 0; l < mesh.TriangleNum; ++l)              //对所有三角形进行操作                  
            {
                int[] index = mesh.tvi[l].toArray();

                for (int lVerticeIndex = 0; lVerticeIndex < 3; ++lVerticeIndex)                      //对单个三角形进行操作
                {

                    Vec2f mUVs = mesh.Texcoords[l][lVerticeIndex];

                    Vec3f Vertex = mesh.Model_Shape[index[lVerticeIndex]];

                    if (isUseTexture) gl.TexCoord(mUVs.x, -mUVs.y);

                    gl.Vertex(Vertex.x, Vertex.y + Model_HeightOffset , Vertex.z);
                }
            }
            gl.End();
            gl.PopMatrix();
        }

        public void resize()
        {
            OpenGL gl = getGlInstance();

            //  设置当前矩阵模式,对投影矩阵应用随后的矩阵操作
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            // 重置当前指定的矩阵为单位矩阵,将当前的用户坐标系的原点移到了屏幕中心
            gl.LoadIdentity();
            // 创建透视投影变换
            gl.Perspective(FOV_Y, scInstance.Width/ scInstance.Height, 5, 100.0);

            // 视点变换
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            // 设置当前矩阵为模型视图矩阵
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        public void Initialized()
        {
            OpenGL gl = getGlInstance();

            gl.ClearColor(0, 0, 0, 0);
            gl.ShadeModel(OpenGL.GL_SMOOTH);                        // shading mathod: GL_SMOOTH or GL_FLAT
            gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 4);           // 4-byte pixel alignment

            // 配置 OpenGL 灯光
            ConfigLight(); 

            gl.ClearDepth(1f);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);           // 开启多边形平滑

            gl.Enable(OpenGL.GL_TEXTURE_2D);               // 使能2D纹理
            gl.Enable(OpenGL.GL_NORMALIZE);                // 使能法线贴图

            Debug.WriteLine("OpenGL 初始化完成");
        }


        private void ConfigLight() 
        {
            OpenGL gl = this.getGlInstance();

            float[] fLightPosition = new float[4] { 0.0f, 0.0f, 0.0f, 10.0f };      // { 5f, 8f, -8f, 1f };// 光源位置 
            float[] fLightAmbient  = new float[4] { 0f, 0f, 0f, 1f };               // 环境光参数 
            float[] fLightDiffuse  = new float[4] { 1f, 1f, 1f, 1f };               // 漫射光参数
            float[] fLightSpecular = new float[4] { 1f, 1f, 1f, 1f };               // 镜面反射

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, fLightAmbient);           // 环境光源 
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, fLightDiffuse);           // 漫射光源 
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, fLightPosition);         // 光源位置 
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, fLightSpecular);         // 镜面光源

            gl.Enable(OpenGL.GL_LIGHTING);                                          // 开启光照 
            gl.Enable(OpenGL.GL_LIGHT0);                                            // 灯光0开启
        
        }
       private double[] identity()
        {
            double[] m = new double[16];
            m[0] = m[5] = m[10] = m[15] = 1.0;
            m[1] = m[2] = m[3] = m[4] = m[6] = m[7] = m[8] = m[9] = m[11] = m[12] = m[13] = m[14] = 0.0;
            return m;
        }
        private double[] getTranspose(double[] m)
        {
            double[] tm = new double[16];
            tm[0]  = m[0]; tm[1]  = m[4]; tm[2]  = m[8];  tm[3]  = m[12];
            tm[4]  = m[1]; tm[5]  = m[5]; tm[6]  = m[9];  tm[7]  = m[13];
            tm[8]  = m[2]; tm[9]  = m[6]; tm[10] = m[10]; tm[11] = m[14];
            tm[12] = m[3]; tm[13] = m[7]; tm[14] = m[11]; tm[15] = m[15];
            return tm;
        }
      

        public bool LoadTexture(string fileName){

            FileInfo file = new FileInfo(fileName);

            if (file.Exists == false)
            {
                MessageBox.Show("无法载入" + fileName);
                return false;
            }
            OpenGL gl = glInstance; 

            // 注释的绑定贴图的方法更加简单
            //texture = new SharpGL.SceneGraph.Assets.Texture();

            //bool loadsucess = texture.Create(gl, fileName);

            //if (loadsucess == false)
            //{
            //    MessageBox.Show("无法载入");
            //    return false;
            //}
            Bitmap image = new Bitmap(fileName);

            if (image != null) { 
                
                //image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                gl.GenTextures(3, TextureID);
 
                /** 创建纹理对象 */
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID[0]);
 
                /** 控制滤波 */
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
 
                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, 
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                /** 创建纹理 */
                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, OpenGL.GL_RGB, image.Width,
                                     image.Height, OpenGL.GL_BGR_EXT, OpenGL.GL_UNSIGNED_BYTE,
                                     bitmapdata.Scan0);
                image.UnlockBits(bitmapdata);

            }
            LoadTextureFlag = true;
            return true;
        }

        public bool LoadTexture(Bitmap texture)
        {
            OpenGL gl = glInstance;

            Bitmap image = texture;

            if (image != null)
            {

                //image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                gl.GenTextures(3, TextureID);

                /** 创建纹理对象 */
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID[0]);

                /** 控制滤波 */
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                /** 创建纹理 */
                gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, OpenGL.GL_RGB, image.Width,
                                     image.Height, OpenGL.GL_BGR_EXT, OpenGL.GL_UNSIGNED_BYTE,
                                     bitmapdata.Scan0);
                image.UnlockBits(bitmapdata);

            }
            LoadTextureFlag = true;

            return true;
        }
        public void setViewportSub(int x, int y, int width, int height, float nearPlane, float farPlane)
        {
            OpenGL gl = glInstance;
            // set viewport
            gl.Viewport(x, y, width, height);
            gl.Scissor(x, y, width, height);

            // set perspective viewing frustum
            setFrustum(FOV_Y, (float)(width) / height, nearPlane, farPlane); // FOV, AspectRatio, NearClip, FarClip
            // copy projection matrix to OpenGL
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadMatrix(getTranspose(matrixProjection));
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

        }

       public  void setFrustum(float l, float r, float b, float t, float n, float f)
        {
            matrixProjection = identity();
            matrixProjection[0] = 2 * n / (r - l);
            matrixProjection[2] = (r + l) / (r - l);
            matrixProjection[5] = 2 * n / (t - b);
            matrixProjection[6] = (t + b) / (t - b);
            matrixProjection[10] = -(f + n) / (f - n);
            matrixProjection[11] = -(2 * f * n) / (f - n);
            matrixProjection[14] = -1;
            matrixProjection[15] = 0;
        }
        public void setFrustum(float fovY, float aspectRatio, float front, float back)
        {
            OpenGL gl = glInstance;
            float tangent = (float)Math.Tan(fovY / 2 * DEG2RAD);   // tangent of half fovY
            float height = front * tangent;           // half height of near plane
            float width = height * aspectRatio;       // half width of near plane

            // params: left, right, bottom, top, near, far
            setFrustum(-width, width, -height, height, front, back);
        }

        public void rotateCamera(int x, int y)
        {
            cameraAngleY += (x - mouseX);
            cameraAngleX += (y - mouseY);
            setMousePos(x, y);
            
        }

        public void moveCameraXY(int x, int y)
        {
            cameraPosition[0] += (x - mouseX) * 0.1f;
            cameraPosition[1] += (y - mouseY) * -0.1f;
            setMousePos(x, y);
            Debug.WriteLine(cameraPosition[0] + " " + cameraPosition[1] + " " + cameraPosition[2] + " ");
        }
        public void moveCameraZ(int x, int y)
        {
            //cameraPosition[1] += (x - mouseX);
            cameraPosition[2] += (y - mouseY) * 0.1f;
            setMousePos(x, y);
        }

        public void setMousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;
        }
        public void zoomCamera(int y)
        {
            cameraDistance -= (y - mouseY) * 0.1f;
            mouseY = y;
        }
        public void zoomCameraDelta(int delta)
        {
            cameraDistance -= delta;
        }
        /// <summary> 
        /// 功能 ：  重新恢复初始视角
        /// </summary> 
        public void resetView()
        {
            cameraAngleX = CAMERA_ANGLE_X;
            cameraAngleY = CAMERA_ANGLE_Y;
            cameraHight = CAMERA_HIGHT;
            cameraDistance = CAMERA_DISTANCE;
            cameraPosition[0] = cameraPosition[1] = cameraPosition[2] = 0;
        }

        /// <summary> 
        /// 功能 ： 设置摄像头高度
        /// 参数 ： 高度值
        /// </summary> 
        public void SetCameraHeight(int height) {
            cameraHight = height;
        }

        public void ConfigOpenGLight(bool status) {
            if (status == true){
                this.glInstance.Enable(OpenGL.GL_LIGHTING);
            }
            else {
                this.glInstance.Disable(OpenGL.GL_LIGHTING);
            }
        }
        public void ConfigTexture(bool status) {
            if (status == true)
            {
                this.glInstance.Enable(OpenGL.GL_TEXTURE_2D);
            }
            else {
                this.glInstance.Disable(OpenGL.GL_TEXTURE_2D);
            }
        }
        public int DrawFlag { get; set; }
    }
}


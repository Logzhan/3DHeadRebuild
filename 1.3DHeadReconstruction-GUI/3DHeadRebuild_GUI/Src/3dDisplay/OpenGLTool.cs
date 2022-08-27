using System;
using SharpGL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThreeDHeadRebuild
{
    public class OpenGLTool
    {
        csgl gl = null;

        /// <summary>
        /// 函数功能 ： OpenGLTool 构造函数
        /// </summary>
        /// <param name="gl"></param>
        public OpenGLTool(csgl gl)
        {
            this.gl = gl;
        }

        /// <summary>
        /// 函数功能 ： 绘制3D网格
        /// </summary>
        /// <param name="size"></param>
        /// <param name="step"></param>
        public void drawGrid(float size, float step)
        {

            if (this.gl == null) return;

            OpenGL gl = this.gl.getGlInstance();
            gl.PushAttrib(OpenGL.GL_ALL_ATTRIB_BITS);         // 保存当前属性

            gl.Disable(OpenGL.GL_TEXTURE_2D);                 // 需要禁用2D纹理，否则这个线的材质会带有人脸贴图的颜色纹理
            gl.Disable(OpenGL.GL_LIGHTING);                   // 关闭灯光
            gl.Begin(OpenGL.GL_LINES); 
            {
                gl.Color(0.3f, 0.3f, 0.3f);
                for (float i = step; i <= size; i += step)
                {
                    gl.Vertex(-size, 0, i);   // lines parallel to X-axis
                    gl.Vertex(size, 0, i);
                    gl.Vertex(-size, 0, -i);   // lines parallel to X-axis
                    gl.Vertex(size, 0, -i);

                    gl.Vertex(i, 0, -size);   // lines parallel to Z-axis
                    gl.Vertex(i, 0, size);
                    gl.Vertex(-i, 0, -size);   // lines parallel to Z-axis
                    gl.Vertex(-i, 0, size);
                }

                // x-axis
                gl.Color(0.5f, 0, 0);
                gl.Vertex(-size, 0, 0);
                gl.Vertex(size, 0, 0);

                // z-axis
                gl.Color(0, 0, 0.5f);
                gl.Vertex(0, 0, -size);
                gl.Vertex(0, 0, size);
            }
            gl.End();


            gl.PopAttrib();                                    //恢复之前保存的属性，重新使能纹理贴图、和灯光
        }

        /// <summary>
        /// 函数功能 ： 材质配置函数，用于配置不同模型的材质
        /// </summary>
        /// <param name="type"></param>
        public void MaterialConfig(int type) {

            float[] no_mat = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };        // 无材质颜色
            float[] mat_ambient = new float[] { 1f, 1f, 1f, 0.0f }; ;       // 环境颜色
            float[] mat_ambient_color = new float[] { 0.8f, 0.6f, 0.2f, 1.0f };
            float[] mat_diffuse = new float[] { 1f, 1f, 1f, 0.0f };         // 散射颜色
            float[] mat_specular = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };  // 镜面反射颜色
            float[] no_shininess = new float[] { 0.0f };                    // 镜面反射指数为0
            float[] low_shininess = new float[] { 5.0f };                   // 镜面反射指数为5.0
            float[] high_shininess = new float[] { 100.0f };                // 镜面反射指数为100.0
            float[] mat_emission = new float[] { 1f, 1f, 1f, 0.0f };        // 发射光颜色

            OpenGL gl = this.gl.getGlInstance();

            if (type == 0)
            {
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT, no_mat);
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_DIFFUSE, mat_diffuse);
                //gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, mat_specular);
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, low_shininess);
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_EMISSION, mat_emission);                          // 自发光材质，开启了这个材质之后，就没有阴影了
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ThreeDHeadRebuild.Src.Util;

namespace ThreeDHeadRebuild
{
    public class Mesh
    {
        public int VertexNum;                                         /* The Number of vertex.                    */
        public int UVNum;                                             /* Texture coordinates Number.              */
        public int TriangleNum;                                       /* The number of the Mesh's triangle.       */
        public int FaceShapeNum;                                      /* The BlendShape Number of the mesh        */
        public int ExpressionNum;
        public List<List<Vec3f>> FaceShape;                           // 脸型顶点数据
        public List<List<Vec3f>> Expression;                          // 表情顶点数据
        public List<List<Vec2f>> Texcoords;                           // UV数据   
        public List<Vec3i> tvi;
        public List<Vec3i> tci;
        public List<string> ShapeName;
        public List<Vec3f> Model_Shape;
        public int[] BlendShapeParameter;
        public Mesh() {
            this.VertexNum = 0;
            this.UVNum = 0;
            this.TriangleNum = 0;
            this.FaceShapeNum = 0;
            this.ExpressionNum = 0;
            this.FaceShape = new List<List<Vec3f>>();
            this.Expression = new List<List<Vec3f>>();
            this.Texcoords = new List<List<Vec2f>>();
            this.tci = new List<Vec3i>();
            this.tvi = new List<Vec3i>();
            this.Model_Shape = new List<Vec3f>();
            this.ShapeName = new List<string>();
        }
    }
    public class MorphableModel
    {

        public Mesh Head;
        public Mesh Eyes;

        /// <summary>
        /// 功能 ： MorphableModel 构造函数，用于变量的初始化
        /// </summary>
        public MorphableModel()
        {
            Head = new Mesh();
            Eyes = new Mesh();
        }


        public void ShapeDeformation(Mesh mesh, int[] BlendShape) {
            mesh.Model_Shape = new List<Vec3f>();
            if (BlendShape.Length != mesh.FaceShapeNum - 1) {
                return;
            }

            //mesh.Model_Shape = mesh.FaceShape[0];
            for (int i = 0; i < mesh.FaceShape[0].Count(); i++) {
                mesh.Model_Shape.Add(mesh.FaceShape[0][i]);
            }

            for (int i = 0; i < BlendShape.Length; i++)
            {
                float w = (float)(BlendShape[i]) / 100.0f;
                if (w < 0) w = 0;
                if (w > 1) w = 1;
                for (int j = 0; j < mesh.Model_Shape.Count(); j++)
                {
                    Vec3f v3f = new Vec3f();
                    v3f.x = w * (mesh.FaceShape[i + 1][j].x - mesh.FaceShape[0][j].x) + mesh.Model_Shape[j].x;
                    v3f.y = w * (mesh.FaceShape[i + 1][j].y - mesh.FaceShape[0][j].y) + mesh.Model_Shape[j].y;
                    v3f.z = w * (mesh.FaceShape[i + 1][j].z - mesh.FaceShape[0][j].z) + mesh.Model_Shape[j].z;
                    mesh.Model_Shape[j] = v3f;
                }
            }
        }

        public void SetMeshBlendShape(Mesh mesh, int[] BlendShape){
            int len = BlendShape.Length;
            mesh.BlendShapeParameter = new int[len];
            for (int i = 0; i < len; i++) {
                if (BlendShape[i] > 100) BlendShape[i] = 100;
                mesh.BlendShapeParameter[i] = BlendShape[i];
            }
        }
        /// <summary>
        /// 功能 ： 指定文件路径加载模型
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Load3DMorphableModel(string filePath){

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (fs == null) return false;
            
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);

            bool status = false;
            status = LoadMesh(sr, this.Head);

            status = LoadMesh(sr, this.Eyes);

            sr.Close();
            fs.Close(); 
            return status;
        }

        public bool LoadMesh(StreamReader sr, Mesh mesh)
        {
            // 读取BlendShape数量， 三角形数量 ， UV数量 ， 顶点数量
            string model_data = sr.ReadLine();
            Debug.WriteLine(model_data);
            // 如果读取不到模型数据返回false
            if (model_data == null) return false;
            // 对字符串进行分割
            string[] strarr = model_data.Split(' ');
            // 分割的数据不为4则返回false
            if (strarr.Length != 4) return false;

            // 读取重要数据
            mesh.FaceShapeNum = int.Parse(strarr[0]) + 1;
            mesh.TriangleNum  = int.Parse(strarr[1]);
            mesh.UVNum        = int.Parse(strarr[2]);
            mesh.VertexNum    = int.Parse(strarr[3]);


            for (int i = 0; i < mesh.FaceShapeNum; i++)
            {
                List<Vec3f> vertex = new List<Vec3f>();
                for (int j = 0; j < mesh.VertexNum; j++)
                {
                    model_data = sr.ReadLine();
                    if (model_data == null) return false;
                    strarr = model_data.Split(' ');
                    // 分割的数据不为4则返回false
                    if (strarr.Length < 3)return false;

                    Vec3f v3f = new Vec3f(float.Parse(strarr[0]),float.Parse(strarr[1]),float.Parse(strarr[2]));

                    vertex.Add(v3f);
                }
                mesh.FaceShape.Add(vertex);
            }

            for (int i = 0; i < mesh.TriangleNum; i++)
            {
                List<Vec2f> UVs = new List<Vec2f>();
                for (int j = 0; j < 3; j++)
                {
                    model_data = sr.ReadLine();
                    if (model_data == null) return false;
                    strarr = model_data.Split(' ');
                    // 分割的数据不为4则返回false
                    if (strarr.Length != 2) return false;
                    Vec2f v2f;
                    v2f.x = float.Parse(strarr[0]);
                    v2f.y = float.Parse(strarr[1]);
                    UVs.Add(v2f);
                }
                mesh.Texcoords.Add(UVs);
            }
            for (int i = 0; i < mesh.TriangleNum; i++)
            {
                model_data = sr.ReadLine();

                if (model_data == null) return false;

                strarr = model_data.Split(' ');

                if (strarr.Length < 3) return false;

                Vec3i v3i = new Vec3i();
                v3i.x = int.Parse(strarr[0]);
                v3i.y = int.Parse(strarr[1]);
                v3i.z = int.Parse(strarr[2]);

                mesh.tci.Add(v3i);
            }

            for (int i = 0; i < mesh.TriangleNum; i++)
            {
                model_data = sr.ReadLine();

                if (model_data == null) return false;

                strarr = model_data.Split(' ');

                if (strarr.Length < 3) return false;

                Vec3i v3i = new Vec3i();
                v3i.x = int.Parse(strarr[0]);
                v3i.y = int.Parse(strarr[1]);
                v3i.z = int.Parse(strarr[2]);

                mesh.tvi.Add(v3i);
            }
            for (int i = 0; i < mesh.FaceShapeNum - 1; i++) {
                model_data = sr.ReadLine();
                if (model_data == null) return false;
                mesh.ShapeName.Add(model_data);
                
            }

            mesh.Model_Shape = mesh.FaceShape[0];

            mesh.BlendShapeParameter = new int[mesh.FaceShapeNum - 1];

            return true;
        }
        /// <summary>
        /// 功能 ： 将MorphableModel 输出为Obj格式
        /// </summary>
        /// <returns></returns>
        public bool WriteObjFile(string filePath) {

            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            if (fs == null) return false;

            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.ASCII);

            /*   Write vertex data firstly.               */
            for (int i = 0; i < Head.VertexNum; i++)
            {
                string str = "v " + Head.Model_Shape[i].x.ToString("F7") + " "
                                  + Head.Model_Shape[i].y.ToString("F7") + " "  
                                  + Head.Model_Shape[i].z.ToString("F7");
                sw.WriteLine(str);      
            }
            for (int i = 0; i < Eyes.VertexNum; i++)
            {
                string str = "v " + Eyes.Model_Shape[i].x.ToString("F7") + " "
                                  + Eyes.Model_Shape[i].y.ToString("F7") + " "
                                  + Eyes.Model_Shape[i].z.ToString("F7");
                sw.WriteLine(str);
            }
            for (int t = 0; t < Head.TriangleNum; t++)
            {
                int[] index = Head.tci[t].toArray();
                for (int v = 0; v < 3; v++)
                {
                    int indices = index[v];
                    string str = "vt " + Head.Texcoords[t][v].x.ToString("F6") + " " +
                                         Head.Texcoords[t][v].y.ToString("F6");
                    sw.WriteLine(str);
                }
            }
            for (int t = 0; t < Eyes.TriangleNum; t++)
            {
                int[] index = Head.tci[t].toArray();
                for (int v = 0; v < 3; v++)
                {
                    int indices = index[v];
                    string str = "vt " + Eyes.Texcoords[t][v].x.ToString("F6") + " " +
                                         Eyes.Texcoords[t][v].y.ToString("F6");
                    sw.WriteLine(str);
                }
            }
            for (int t = 0; t < Head.TriangleNum; t++)
            {
                string str = "f ";
                int[] index = Head.tci[t].toArray();
                for (int v = 0; v < 3; v++)
                {
                    int indices = index[v];
                    str += (indices + 1).ToString() + "/" + ( t * 3 + v + 1).ToString() + " ";
                }
                sw.WriteLine(str);
            }
            for (int t = 0; t < Eyes.TriangleNum; t++)
            {
                string str = "f ";
                int[] index = Eyes.tci[t].toArray();
                for (int v = 0; v < 3; v++)
                {
                    int indices = index[v] + Head.VertexNum;
                    str += (indices + 1).ToString() + "/" + (t * 3 + v + 1 + Head.TriangleNum * 3).ToString() + " ";
                }
                sw.WriteLine(str);
            }

            sw.Close();
            fs.Close();
            

            return true;
        }

    }
}

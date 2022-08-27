/****************************************************************************************
 * File         :   DrawScene.cpp 
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   
 * Time         :   2018-01-03
 * CopyRights   :   UESTC Kb545
****************************************************************************************/
#include "GlobalConfig.h"

#include "DrawScene.h"
#include <iostream>
#include <opencv2\opencv.hpp>
#include <opencv2\core.hpp>
#include <opencv2\highgui.hpp>
#include <opencv2\imgproc.hpp>
#include "windows.h"
#include "TextureGL.h"
#include "TextureCV.h"
#include "facefitting.h"
#include "FitFaceShape.h"
#define GLOBAL_MAP          1       //全局关键点映射 
#define FEATUR_MAP          0       //五官关键点映射
#define USING_ATUO_ADJUST   1       //使用模拟退火算法自动调整BlendShape

using namespace cv;


extern GLuint lTextureObject;

//bool SaveUVsAndTexture(Mat skin, vector<Vec2f> UV){
//	FILE *fp;
//	if(!imwrite("skintexture.png",Skin)){
//		printf("Save texture error! \n");
//		return false;
//	}
//	fopen_s(&fp,"UVFile.txt","a");
//	if(fp!=NULL){
//		printf("Save UVFile sucess! \n");
//		for(unsigned int i = 0; i < UV.size(); i++){
//			fprintf(fp,"U = %f V = %f\n",UV[i][0],UV[i][1]);
//		}
//		return true;
//	}
//	return false;
//}

void DrawMesh(Mesh mesh,unsigned int lTexture){

	int lPolygonCount = mesh.TriangleNum;                                                                     //如果加载贴图成功，调用OpenGL函数进行渲染
	glEnable(GL_TEXTURE_2D);
	glEnable(GL_POLYGON_SMOOTH);
	glBindTexture(GL_TEXTURE_2D, lTextureObject);
	glBegin(GL_TRIANGLES);
	for (int lPolygonIndex = 0; lPolygonIndex < lPolygonCount; ++lPolygonIndex)                               //对所有三角形进行操作                  
	{
		for (int lVerticeIndex = 0; lVerticeIndex < POLYGON_VERTEX_NUM; ++lVerticeIndex)                      //对单个三角形进行操作
		{
				int    Indices = mesh.tvi[lPolygonIndex][lVerticeIndex];

				Vec2f  mUVs    = mesh.texcoords[lPolygonIndex][lVerticeIndex];

				Vec4f  Vertex  = mesh.vertices[0][Indices];

				glTexCoord2d(mUVs[0], mUVs[1]);
				glVertex3f(Vertex[0],Vertex[1],Vertex[2]);	
		}
	}
	glEnd();
}
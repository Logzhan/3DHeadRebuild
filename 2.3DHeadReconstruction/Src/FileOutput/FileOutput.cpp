/****************************************************************************************
 * File         :   FileOutput.cpp 
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   Output 3d model in different format. such as fbx and obj¡¢txt.
 * Time         :   2017-01-15 @Zhanli : Create file.       
 * CopyRights   :   UESTC Kb545
****************************************************************************************/
#include "Globalconfig.h"
#include "FileOutput.h"


/***************************************************************
 * Descripition : Output 3d model with obj format.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
bool OutputObjModel(const char *fileName,MorphableModel OutputModel){
	FILE *fp;
	/*   Try open the file with fileName.         */
	fopen_s(&fp,fileName,"w");
	/*   If can not open the file, return false.  */
	if(fp==NULL)return false;
	/*   Write vertex data firstly.               */
	for(int i = 0; i < OutputModel.Head.VertexNum; i++){
		fprintf(fp,"v %f %f %f\n",OutputModel.Head.vertices[0][i][0],
			                      OutputModel.Head.vertices[0][i][1],
								  OutputModel.Head.vertices[0][i][2]);
	}
	//for(int i = 0; i < OutputModel.Eyes.VertexNum; i++){
	//	fprintf(fp,"v %f %f %f\n",OutputModel.Eyes.vertices[0][i][0],
	//							  OutputModel.Eyes.vertices[0][i][1],
	//							  OutputModel.Eyes.vertices[0][i][2]);		
	//}
	/*  Write uv data secondly.                   */
	vector<Vec2f> mUVs;
	mUVs.resize(OutputModel.Head.VertexNum);
	for(int t = 0; t < OutputModel.Head.TriangleNum; t++){
		for(int v = 0; v < POLYGON_VERTEX_NUM;v++){
			int indices = OutputModel.Head.tci[t][v];
			mUVs[indices] = OutputModel.Head.texcoords[t][v];
			fprintf(fp,"vt %f %f\n",mUVs[indices][0],1 - mUVs[indices][1]);
		}
	}
	mUVs.clear();
	//for(int t = 0; t < OutputModel.Eyes.TriangleNum; t++){
	//	for(int v = 0; v < POLYGON_VERTEX_NUM;v++){
	//		int indices = OutputModel.Eyes.tci[t][v];
	//		mUVs[indices] = OutputModel.Eyes.texcoords[t][v];
	//		fprintf(fp,"vt %f %f\n",mUVs[indices][0],1 - mUVs[indices][1]);
	//	}
	//}
	/*  Write uv data thirdly.                    */
	for(int t = 0; t < OutputModel.Head.TriangleNum; t++){
		fprintf(fp,"f ");
		for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
			int indices = OutputModel.Head.tci[t][v];
			fprintf(fp,"%d/%d ",indices + 1, (t)*POLYGON_VERTEX_NUM + (v + 1));
		}
		fprintf(fp,"\n");
	}
	fprintf(fp,"\n");

	///*  Write uv data thirdly.                    */
	//for(int t = 0; t < OutputModel.Eyes.TriangleNum; t++){
	//	fprintf(fp,"f ");
	//	for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
	//		int indices = OutputModel.Eyes.tci[t][v] + OutputModel.Head.VertexNum;
	//		fprintf(fp,"%d/%d ",indices + 1, (t)*POLYGON_VERTEX_NUM + (v + 1) + OutputModel.Head.TriangleNum * POLYGON_VERTEX_NUM);
	//	}
	//	fprintf(fp,"\n");
	//}
	//fprintf(fp,"\n");

	fclose(fp);
	return true;
}

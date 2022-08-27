/****************************************************************************************
 * File         :   fitfaceShape.cpp 
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   Face fitting algorithm. Reconstruction 3d head by offering front 
                    picture.
 * Time         :   2017-12-28 :  Create file.               
 * CopyRights   :   UESTC Kb545
****************************************************************************************/
#include "FitFaceShape.h"
#include "3dfaceapi.h"
#include "facefitting.h"
#include "TextureCV.h"
#include "TextureGL.h"

#include "Eigen/Core"
#include "Eigen/QR"
#include "nnls.h"

using namespace Eigen;
using namespace std;
using namespace cv;
/***************************************************************
 * The definition of private varible
***************************************************************/
/* The maximum of algorithum iterations number. */
const  int         Max_Iterations       = 3500;    
/* The postion of 68 landmarks on face.         */
vector<Vec2f>      vFaceLandmarksPos;                         
int                nBlendShapeParameter[BLENDSHAPE_NUM];
/* The front picture of user face.              */
Mat                FrontPicture;
/* General skin texture using for all usr.      */
Mat                GeneralSkinTexture;
/* Scale Ortho Projection parameters.           */
ScaledOrthoProjectionParameters sp;
/* Second scale parameters.                     */
Vec3f              ssp;
/* Generate a output 3d head model.             */
MorphableModel     OutputModel;

/***************************************************************
 * Descripition : Reconstruction a 3d head model by given front
                  picture file path and a 3d morphable model.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
MorphableModel Reconstruction3DHead(const char *cFrontPicFilePath,
									const char *cSkinPicFilePath,
									MorphableModel InputModel)
{
	/* Loading user front face picture.           */
	FrontPicture         = imread(cFrontPicFilePath);
	/* Loading general skin texture picture.      */
	GeneralSkinTexture   = imread(cSkinPicFilePath);

#if OUTPUT_DEBUG_INFORMATION 
	printf("Detecting usr face...\n");
#endif
	/* Detection the front face of input picture. */
	FaceDetection(vFaceLandmarksPos,FrontPicture);

#if OUTPUT_DEBUG_INFORMATION 
	printf("Detect face sucess!\n");
#endif

#if OPENCV_DEBUG_INFORMATION
	imshow("Detect face",FrontPicture);
	waitKey(0);
#endif 

#if OUTPUT_DEBUG_INFORMATION 
	printf("Start fit usr face...\n");
#endif
	/* Fitting user face Shape.                   */
	FittingFaceShapeNNLS(FrontPicture,vFaceLandmarksPos,InputModel,nBlendShapeParameter);

#if OUTPUT_DEBUG_INFORMATION 
	printf("Start fit usr face finish.\n");
#endif

	/* Fitting user facial feature.               */
	FittingFacialFearture(vFaceLandmarksPos,InputModel);

	ssp = GenerateTexture(FrontPicture,GeneralSkinTexture,
		                               vFaceLandmarksPos);

	/* Mapping the texture.                       */
	MappingTexture(InputModel);

	/* Generate 3d head model data struct.        */
	Generate3DHeadModel(InputModel,OutputModel);

	/* Return head reconstruction result.         */
	return OutputModel;
}
/***************************************************************
 * Descripition : Fitting face shape. Output face blendshape pa-
                  rameter.
 * Input        : 1. Input Morphable model.
                  2. User's front picture.
				  3. Face landmark postion.
 * Ouput        : 1. BlendShape parameter.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void FittingFaceShape(Mat FrontPic,vector<Vec2f> vFaceLandmarksPos,MorphableModel Model, 
					  int* nBlendShapeParameter)
{
	/* Initial fitting err is infinite.           */
	static float FittingErr           = 1.0e+015f;
	/* Record the number of iterations.           */
	static int   IterationCount       = 0;
	/* A accelerate way of algorithm. When we def-
	   romation the 3d morphable, we can just com-
	   pute the face key points.                  */
	static bool  bUseKeyPoint          = true;

	/* The model vertex count.                    */
	const  int   nVertexCount          = Model.Head.VertexNum;

	Vec4f* VertexCache   = new Vec4f[nVertexCount];
	Vec4f* lVertexArray  = new Vec4f[nVertexCount];
	for(int i = 0; i < nVertexCount ;i++){	
		VertexCache[i] = Model.Head.vertices[0][i];
	}
	memcpy(lVertexArray, VertexCache, nVertexCount * sizeof(Vec4f));

	std::vector<cv::Vec2f> cvimage_points;
	std::vector<cv::Vec4f> cvmodel_points;

	/* Using 2d face keypoints and 3d vertex estimate 
		linear orthographic projection.            */
	for (int i = 16; i < FACE_LANDMRAK_NUM; i++){
		Vec4f tmp4f;
		int indices = VertexIndices[i];
		tmp4f[0] = (float)( lVertexArray[indices][0]);
		tmp4f[1] = (float)(-lVertexArray[indices][1]);
		tmp4f[2] = (float)(-0);
		tmp4f[3] = 1.0f;
		cvmodel_points.push_back(tmp4f);
		cvimage_points.push_back(vFaceLandmarksPos[i]);
	}
	sp = estimate_orthographic_projection_linear(cvimage_points, cvmodel_points);

#if OUTPUT_DEBUG_INFORMATION 
	printf("Estimate the orthographic projection finish\n");
	printf("tx = %f ty = %f s = %f\n",sp.tx,sp.ty,sp.s);
#endif 
	cvimage_points.clear();
	cvmodel_points.clear();

	/* Loop iterate compute blendshape parameters. */
	while(IterationCount < Max_Iterations)
	{
		memcpy(lVertexArray, VertexCache, nVertexCount * sizeof(Vec4f));
#if     OPENCV_DEBUG_INFORMATION
		Mat Output = FrontPic.clone();
#endif 
		int TmpBlendShapeParameter[BLENDSHAPE_NUM];
		memcpy(TmpBlendShapeParameter,nBlendShapeParameter,BLENDSHAPE_NUM*sizeof(int));
		/* Generate Random weight.                     */
		GenerateRandomWeight(TmpBlendShapeParameter);

		ShapeDeformation(lVertexArray,TmpBlendShapeParameter,bUseKeyPoint, Model); 

		for (unsigned int i = 0; i < vFaceLandmarksPos.size(); i++)
		{
			int Indices = VertexIndices[i]; 
			Vec4f tmp4f;
			tmp4f[0] = (float)( lVertexArray[Indices][0]);
			tmp4f[1] = (float)(-lVertexArray[Indices][1]);
			tmp4f[2] = (float)( lVertexArray[Indices][2]);
			tmp4f[3] = 1.0f;				
			tmp4f[0] = (float)((tmp4f[0] + sp.tx)*sp.s);
			tmp4f[1] = (float)((tmp4f[1] + sp.ty)*sp.s);

			cvmodel_points.push_back(tmp4f);
			cvimage_points.push_back(vFaceLandmarksPos[i]);

#if     OPENCV_DEBUG_INFORMATION
			circle(Output, Point2f(tmp4f[0], tmp4f[1]), 
				   2, Scalar(255, 0, 0, 0), CV_FILLED, CV_AA, 0);
			circle(Output, Point2f(vFaceLandmarksPos[i][0], vFaceLandmarksPos[i][1]), 
				   2, Scalar(0, 255, 0, 0), CV_FILLED, CV_AA, 0);
#endif 
		}
		float error = ComputeError(cvmodel_points, cvimage_points);
		if (error < FittingErr){
			FittingErr = error;
			memcpy(nBlendShapeParameter,TmpBlendShapeParameter,BLENDSHAPE_NUM*sizeof(int));
#if     OUTPUT_DEBUG_INFORMATION                         
			printf("Fitting Error = %f\n", FittingErr);
#endif 
#if     OPENCV_DEBUG_INFORMATION
		imshow("OPENCV_DEBUG",Output);
		waitKey(1);
#endif 
		}
		cvimage_points.clear();
		cvmodel_points.clear();

		IterationCount++;
#if     OUTPUT_DEBUG_INFORMATION 
		printf("Iteration count = %d\n",IterationCount);
#endif 
	}
	delete [] lVertexArray;
	delete [] VertexCache;
}
/***************************************************************
 * Descripition : Mapping 2D picture to 3d texture. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void MappingTexture(MorphableModel Model){

	/* Mapping head texture coordinate.            */
	int    nVertexCount = Model.Head.VertexNum;

	Vec4f* lVertexArray = new Vec4f[nVertexCount];

	for(int i = 0; i < nVertexCount ;i++)
		for(int j = 0; j < 4; j++)
			lVertexArray[i][j] = Model.Head.vertices[0][i][j];

	/* Deform the shape by blendshape parameters.  */
	ShapeDeformation(lVertexArray, nBlendShapeParameter, false, Model);
	Mat Output = GeneralSkinTexture.clone();

	/* There we directly mapping vertex to face pict-
	   ure. It is a sample way, we can figure out a 
	   better way replace it and achieve a better re-
	   sult.                                       */
	float threshold = FACE_UV_RANGE_Threshold / GeneralSkinTexture.size().width;
	for (int t = 0; t < Model.Head.TriangleNum; t++){
		/* If this triangle has any UV of vertex samller 
		   than threshold, this  triangle is part of fa-
		   ce. So this part of UV should be replaced. */
		if(Model.Head.texcoords[t][0][0] < threshold){
			vector<Vec2f> tmpUV;
			for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
				Vec2f UVs;
				int indices = Model.Head.tci[t][v];
				UVs[0] = (float)( lVertexArray[indices][0]);
				UVs[1] = (float)(-lVertexArray[indices][1]);
				/* Convert model coordinate to image coordinate. */
				UVs[0] = (float)((UVs[0] + sp.tx)*sp.s) / 512.0f;
				UVs[1] = (float)((UVs[1] + sp.ty)*sp.s) / 512.0f;
				/* Convert front picture coordinate to General 
				   skin picture coordinate.                      */

				float U = (float)((UVs[0] * 512 + ssp[1])*(1.0f/ssp[0]));
				float V = (float)((UVs[1] * 512 + ssp[2])*(1.0f/ssp[0]));

				/* Convert horizontal coordinates to vertical 
				   coordinates                                   */
				UVs[0] =      V   / GeneralSkinTexture.size().width;
				UVs[1] = 1 -  U   / GeneralSkinTexture.size().height;

				tmpUV.push_back(UVs);
			}
			OutputModel.Head.texcoords.push_back(tmpUV);
		}else{
			/* Compute head part of 3d model                 */
			vector<Vec2f> tmpUV;
			for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
				Vec2f UVs;
				int indices = Model.Head.tci[t][v];
				if(isEdgePoints(indices)){
					UVs[0] = (float)( lVertexArray[indices][0]);
					UVs[1] = (float)(-lVertexArray[indices][1]);
					/* Convert model coordinate to image coordinate. */
					UVs[0] = (float)((UVs[0] + sp.tx)*sp.s) / 512.0f;
					UVs[1] = (float)((UVs[1] + sp.ty)*sp.s) / 512.0f;
					/* Convert front picture coordinate to General 
					   skin picture coordinate.                      */

					float U = (float)((UVs[0] * 512 + ssp[1])*(1.0f/ssp[0]));
					float V = (float)((UVs[1] * 512 + ssp[2])*(1.0f/ssp[0]));

					UVs[0] =      V   / GeneralSkinTexture.size().width;
					UVs[1] = 1 -  U   / GeneralSkinTexture.size().height;
				}else{
					/* Head part, these uv not need to change.       */
					UVs[0] =     Model.Head.texcoords[t][v][0];
					UVs[1] = 1 - Model.Head.texcoords[t][v][1];
				}
				tmpUV.push_back(UVs);
			}
			OutputModel.Head.texcoords.push_back(tmpUV);
		}
	}
	/* Mapping eyes texture coordinate by the same 
	   way                                         */
	nVertexCount = Model.Eyes.VertexNum;

	for(int i = 0; i < nVertexCount ;i++)
		for(int j = 0; j < 4; j++)
			lVertexArray[i][j] = Model.Eyes.vertices[0][i][j];

	/* Directly mapping 2D picture to 3D texture   
	   coordinate.                                 */
	for (int t = 0; t < Model.Eyes.TriangleNum; t++){
		/* If this triangle has any UV of vertex samller 
		   than threshold, this  triangle is part of fa-
		   ce. So this part of UV should be replaced. */
		vector<Vec2f> tmpUV;
		for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
			Vec2f UVs;
			int indices = Model.Eyes.tci[t][v];
			UVs[0] = (float)( lVertexArray[indices][0]);
			UVs[1] = (float)(-lVertexArray[indices][1]);
			/* Convert model coordinate to image coordinate. */
			UVs[0] = (float)((UVs[0] + sp.tx)*sp.s) / 512.0f;
			UVs[1] = (float)((UVs[1] + sp.ty)*sp.s) / 512.0f;
			/* Convert front picture coordinate to General 
				skin picture coordinate.                      */
			float U = (float)((UVs[0] * 512 + ssp[1])*(1.0f/ssp[0]));
			float V = (float)((UVs[1] * 512 + ssp[2])*(1.0f/ssp[0]));

			UVs[0] =         V   / GeneralSkinTexture.size().width;
			UVs[1] = 1.0f -  U   / GeneralSkinTexture.size().height;

			tmpUV.push_back(UVs);
		}
		OutputModel.Eyes.texcoords.push_back(tmpUV);	
	}
}

/***************************************************************
 * Descripition : Generate radom weight by rand function. And 
                  randomly change the index between two weights.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void GenerateRandomWeight(int *weight){
	/* Setting Rand range to -1 to 1                  */
	int Range_min = -1, Range_max = 1;
	int RandomWeight = 0;
	for (int i = 0; i < BLENDSHAPE_NUM; i++){
		/* Randomly generate the weight between the min
		   and max.                                   */
		RandomWeight = (rand() % (Range_max - Range_min + 1)) 
			           + Range_min;
		/* Apply the random weight.                   */
		weight[i] = weight[i] + RandomWeight;
		/* Limit variable range.                      */
		if (weight[i]>100)weight[i] = 100;
		if (weight[i]< 0)weight[i]  = 0;
	}
	/* Resetting Rand range to 0 to BLENDSHAPE_NUM - 1*/
	Range_min = 0, Range_max = BLENDSHAPE_NUM - 1;
	/* Generate swap index.                           */
	int r1 = (rand() % (Range_max - Range_min  + 1)) + Range_min;
	int r2 = (rand() % (Range_max - Range_min  + 1)) + Range_min;
	/* If r1 unequal to r2, swap these two weght.     */
	if(r1 != r2){
		/* Swap the two weight.                       */
		int tmp    =  weight[r1];
		weight[r1] =  weight[r2];
		weight[r2] =  tmp;
	}
}

/***************************************************************
 * Descripition : Deform head shape by given BlendShape weight 
                  parameter.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void ShapeDeformation(Vec4f* pVertexArray, int weight[BLENDSHAPE_NUM], 
					  bool bUseKeyPoint,   MorphableModel model)
{
	/*   Getting the number of Vertex.                */
	int    lVertexCount    = model.Head.VertexNum;
	Vec4f* lSrcVertexArray = pVertexArray;

	/*   Copy the Srcouce array to dst array.         */
	Vec4f* lDstVertexArray = new Vec4f[lVertexCount];
	memcpy(lDstVertexArray, pVertexArray, lVertexCount * sizeof(Vec4f));

	/*    Getting the channel number of a BlendShape. */
	int lBlendShapeChannelCount = model.Head.BlendShapeNum;
	lBlendShapeChannelCount = lBlendShapeChannelCount > BLENDSHAPE_NUM ? BLENDSHAPE_NUM : lBlendShapeChannelCount;

	for (int lChannelIndex = 0; lChannelIndex < lBlendShapeChannelCount; lChannelIndex++){
		int lWeight = weight[lChannelIndex];
		if(lWeight > 100)lWeight = 100;
		if(lWeight < 0)lWeight = 0;
		if (!bUseKeyPoint){
			for (int j = 0; j < lVertexCount; j++){
				lDstVertexArray[j] += (model.Head.vertices[lChannelIndex + 1][j] - 
						               lSrcVertexArray[j]) * lWeight * 0.01f;
			}
		}else{
			for (int k = 0; k < 68; k++){
				int j = VertexIndices[k];
				lDstVertexArray[j] += (model.Head.vertices[lChannelIndex + 1][j] - 
					                   lSrcVertexArray[j]) * lWeight * 0.01f;
			}
		}
	}
    memcpy(pVertexArray, lDstVertexArray, lVertexCount * sizeof(Vec4f));
    delete [] lDstVertexArray;
}
/***************************************************************
 * Descripition : Generate a new 3D head model. This model only 
                  has shape but not blendshape.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-09
***************************************************************/
void Generate3DHeadModel(MorphableModel Model, MorphableModel &ModelOutput){

	/* Vertex num and triangle¡¢tvi¡¢tci is same. so 
	   we just copy.                                 */
	/* Generate head part firstly.                   */
	ModelOutput.Head.VertexNum   = Model.Head.VertexNum;
	ModelOutput.Head.TriangleNum = Model.Head.TriangleNum;
	ModelOutput.Head.tci.assign(Model.Head.tci.begin(),Model.Head.tci.end());
	ModelOutput.Head.tvi.assign(Model.Head.tvi.begin(),Model.Head.tvi.end());

	int    nVertexCount = Model.Head.VertexNum;

	Vec4f* lVertexArray = new Vec4f[nVertexCount];
	for(int i = 0; i < nVertexCount ;i++)
		lVertexArray[i] = Model.Head.vertices[0][i];

	/* Deform the shape by blendshape parameters.  */
	ShapeDeformation(lVertexArray, nBlendShapeParameter, false, Model);
	
	ModelOutput.Head.BlendShapeNum = 1;

	vector<Vec4f> Tmpvertices;
	for(int i = 0; i < nVertexCount ;i++){
		Tmpvertices.push_back(lVertexArray[i]);
	}
	/* Save the vertices.                          */
	ModelOutput.Head.vertices.push_back(Tmpvertices);
	
	/* Generate eyes part seconedly.               */
	ModelOutput.Eyes.VertexNum   = Model.Eyes.VertexNum;
	ModelOutput.Eyes.TriangleNum = Model.Eyes.TriangleNum;
	ModelOutput.Eyes.tci.assign(Model.Eyes.tci.begin(), Model.Eyes.tci.end());
	ModelOutput.Eyes.tvi.assign(Model.Eyes.tvi.begin(), Model.Eyes.tvi.end());
	/* There only one type blendshape in eyes.     */
	ModelOutput.Eyes.BlendShapeNum = 1;

	nVertexCount = Model.Eyes.VertexNum;
	Tmpvertices.clear();
	for(int i = 0; i < nVertexCount ;i++)
		Tmpvertices.push_back(Model.Eyes.vertices[0][i]);

	ModelOutput.Eyes.vertices.push_back(Tmpvertices);
}
/***************************************************************
 * Descripition : Compute the fitting error between the 3d model
                  and picture.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-09
***************************************************************/
float ComputeError(vector<Vec4f> model_points, vector<Vec2f> image_points){
	float sum = 0.0f;
	if (model_points.size() != image_points.size()){ 
#if     OUTPUT_DEBUG_INFORMATION 
		printf("Compute Error :: Size not same\n"); 
		system("pause");
#endif  
		exit(1); 
	}
	for (unsigned int i = 1; i <= 15; i++){
		sum = sum + (model_points[i][0] - image_points[i][0])*(model_points[i][0] - image_points[i][0]) +
			        (model_points[i][1] - image_points[i][1])*(model_points[i][1] - image_points[i][1]);
	}
	return sum;
}

void FittingFacialFearture(vector<Vec2f> FaceLandmarksPos,MorphableModel model){

}
/***************************************************************
 * Descripition : Determine whether this point is edge point.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-11
***************************************************************/
bool isEdgePoints(int index){
	for(int i = 0; i < 76;i++){
		if(index == UVEdgePoints[i]){
			return true;
		}
	}
	return false;
}
/***************************************************************
 * Descripition : Get generated texture.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-11
***************************************************************/
Mat GetTexture(void){
	return GeneralSkinTexture;
}


bool SaveTexture(const char* path){
	return imwrite(path,GeneralSkinTexture);
}

bool SaveBlenShape(const char *path){
	FILE *fp;
	fopen_s(&fp,path,"w");
	if(fp!=NULL){
		for(unsigned int i = 0; i < BLENDSHAPE_NUM; i++){
			fprintf(fp,"%d\n",nBlendShapeParameter[i]);
		}
		fclose(fp);
		return true;
	}else{
		printf("Save BlendShape Error! \n");
		return false;
	}
}

/***************************************************************
 * Descripition : Save UV the vertex file. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-12 : 
                  Change write mode 'a' to write model to 'w'
***************************************************************/
bool SaveUVFile(const char* path, MorphableModel model){
	FILE *fp;
	int  nPolygonCount; 
	Mesh HeadMesh      =  model.Head;
	Mesh EyesMesh      =  model.Eyes;
	int  nVertexCount  =  HeadMesh.VertexNum;
	
	fopen_s(&fp,path,"w");
	if(fp!=NULL){
		nPolygonCount =  HeadMesh.TriangleNum;
		for(int t = 0; t < nPolygonCount; t++){
			for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
				int indices    = HeadMesh.tvi[t][v];
				Vec2f mUVs     = HeadMesh.texcoords[t][v];
				Vec4f Vertices = HeadMesh.vertices[0][indices];
				fprintf(fp,"h::%f %f %f %f %f\n",mUVs[0],mUVs[1],
					        Vertices[0],Vertices[1],Vertices[2]);
			}
		}
		nPolygonCount =  EyesMesh.TriangleNum;
		for(int t = 0; t < nPolygonCount; t++){
			for(int v = 0; v < POLYGON_VERTEX_NUM; v++){
				int indices    = EyesMesh.tvi[t][v];
				Vec2f mUVs     = EyesMesh.texcoords[t][v];
				Vec4f Vertices = EyesMesh.vertices[0][indices];
				fprintf(fp,"e::%f %f %f %f %f\n",mUVs[0],mUVs[1],
					        Vertices[0],Vertices[1],Vertices[2]);
			}
		}
		fclose(fp);
		return true;
	}
	return false;
}

/***************************************************************
 * Descripition : Fitting face shape. Output face blendshape pa-
                  rameter.
 * Input        : 1. Input Morphable model.
                  2. User's front picture.
				  3. Face landmark postion.
 * Ouput        : 1. BlendShape parameter.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void FittingFaceShapeNNLS(Mat FrontPic,vector<Vec2f> vFaceLandmarksPos,MorphableModel Model, 
					  int* nBlendShapeParameter)
{
	/* The model vertex count.                    */
	const  int   nVertexCount          = Model.Head.VertexNum;

	Vec4f* VertexCache   = new Vec4f[nVertexCount];
	Vec4f* lVertexArray  = new Vec4f[nVertexCount];
	for(int i = 0; i < nVertexCount ;i++){	
		VertexCache[i] = Model.Head.vertices[0][i];
	}
	memcpy(lVertexArray, VertexCache, nVertexCount * sizeof(Vec4f));

	std::vector<cv::Vec2f> cvimage_points;
	std::vector<cv::Vec4f> cvmodel_points;

	

	//for(int iteratorCount = 0; iteratorCount < 2; iteratorCount++)
	{

		Mat Output = FrontPic.clone();
		/* Using 2d face keypoints and 3d vertex estimate 
			linear orthographic projection.            */
		for (int i = 0; i < FACE_LANDMRAK_NUM; i++){
			Vec4f tmp4f;
			int indices = VertexIndices[i];
			tmp4f[0] = (float)( lVertexArray[indices][0]);
			tmp4f[1] = (float)(-lVertexArray[indices][1]);
			tmp4f[2] = (float)(-0);
			tmp4f[3] = 1.0f;
			cvmodel_points.push_back(tmp4f);
			cvimage_points.push_back(vFaceLandmarksPos[i]);
			circle(Output,Point((int)vFaceLandmarksPos[i][0],(int)vFaceLandmarksPos[i][1]),2,CV_RGB(0,255,0),2);
		}

		sp = estimate_orthographic_projection_linear(cvimage_points, cvmodel_points);

	#if OUTPUT_DEBUG_INFORMATION 
		printf("Estimate the orthographic projection finish\n");
		printf("tx = %f ty = %f s = %f\n",sp.tx,sp.ty,sp.s);
	#endif 
		cvimage_points.clear();
		cvmodel_points.clear();

		int64 start=0,end=0;  
		start = getTickCount();

		/* Construct equations ||Ax-b||_2^2           */
		MatrixXf A = MatrixXf::Zero(TWO_DIMENSIONAL_COORDINATES * FACE_LANDMRAK_NUM, BLENDSHAPE_NUM);
		VectorXf b = VectorXf::Zero(TWO_DIMENSIONAL_COORDINATES * FACE_LANDMRAK_NUM);
		VectorXf x;

	
		for(int n = 0; n < BLENDSHAPE_NUM; n++){
			for(int j = 0; j < TWO_DIMENSIONAL_COORDINATES; j++){
				for(int i = 0; i < FACE_LANDMRAK_NUM; i++){
					int indices = VertexIndices[i];
					if( j == 0){
						A(j * FACE_LANDMRAK_NUM + i, n) = (float)(( (Model.Head.vertices[n + 1][indices][j] - Model.Head.vertices[0][indices][j])) * sp.s);
					}else if( j == 1){
						A(j * FACE_LANDMRAK_NUM + i, n) = (float)((-(Model.Head.vertices[n + 1][indices][j] - Model.Head.vertices[0][indices][j])) * sp.s);
					}
				}
			}	
		}
		for(int j = 0; j < TWO_DIMENSIONAL_COORDINATES; j++){
			for(int i = 0; i < FACE_LANDMRAK_NUM; i++){
				int indices = VertexIndices[i];
				if(j == 0){
					b(j*FACE_LANDMRAK_NUM + i) = vFaceLandmarksPos[i][j] - (float)((Model.Head.vertices[0][indices][j] + sp.tx) * sp.s);
				}else if(j == 1){
					b(j*FACE_LANDMRAK_NUM + i) = vFaceLandmarksPos[i][j] - (float)(((-Model.Head.vertices[0][indices][j]) + sp.ty) * sp.s);
				}			
			}
		}

		bool non_singular = Eigen::NNLS<MatrixXf>::solve(A, b, x);
		end = getTickCount(); 
		cout << "Fitting face shape: " << 1000.0*(end - start)/getTickFrequency()<<" ms"<< endl; 
		for(int i = 0; i < BLENDSHAPE_NUM; i++){
			nBlendShapeParameter[i] = (int)(x(i)*100);
			//nBlendShapeParameter[26] = 100;
			printf("BlendShape Channel %d weight = %d\n",i,nBlendShapeParameter[i]);
			
		}
		 
		

		ShapeDeformation(lVertexArray, nBlendShapeParameter, false, Model);
		
		for (int i = 0; i < FACE_LANDMRAK_NUM; i++){
			Vec4f tmp4f;
			int indices = VertexIndices[i];
			tmp4f[0] = (float)( lVertexArray[indices][0]);
			tmp4f[1] = (float)(-lVertexArray[indices][1]);
			tmp4f[0] = (float)((tmp4f[0] + sp.tx) * sp.s);
			tmp4f[1] = (float)((tmp4f[1] + sp.ty) * sp.s);
			circle(Output,Point((int)tmp4f[0],(int)tmp4f[1]),2,CV_RGB(0,0,255),2);
		}
		imshow("Fitting Result",Output);
		waitKey(0);

		Output.release();
	}
	delete[] VertexCache;
	delete[] lVertexArray;
}
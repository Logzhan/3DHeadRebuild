#ifndef  _FIT_FACE_SHAPE_H_
#define  _FIT_FACE_SHAPE_H_

#include "GlobalConfig.h"

MorphableModel Reconstruction3DHead(const char *cFrontPicFilePath,
									const char *cSkinPicFilePath,
									MorphableModel InputModel);

/*  Getting BlendShape weight parameter,compute the shape deformation. */
void ShapeDeformation(Vec4f*         pVertexArray, 
					  int            weight[BLENDSHAPE_NUM], 
					  bool           bUseKeyPoint, 
					  MorphableModel mm);

/*  Computing the error between the model and the image.               */
float ComputeError(vector<Vec4f> model_points, vector<Vec2f> image_points);

/*  Generate the reconstruction 3d head model.                         */
void Generate3DHeadModel(MorphableModel Model, 
						 MorphableModel &ModelOutput);


void FittingFaceShape(Mat FrontPic,
					  vector<Vec2f> vFaceLandmarksPos,
					  MorphableModel Model, 
					  int* nBlendShapeParameter);

void FittingFacialFearture(vector<Vec2f> FaceLandmarksPos,
						   MorphableModel model);

void MappingTexture(MorphableModel Model);

/*  Checking wheter the points is face to head edge points.            */
bool isEdgePoints(int index);

void GenerateRandomWeight(int *weight);

Mat GetTexture(void);

bool SaveTexture(const char* path);

bool SaveBlenShape(const char *path);

void FittingFaceShapeNNLS(Mat FrontPic,vector<Vec2f> vFaceLandmarksPos,MorphableModel Model, 
					  int* nBlendShapeParameter);

bool SaveUVFile(const char* path, MorphableModel model);

#endif
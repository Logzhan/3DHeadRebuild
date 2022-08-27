#ifndef  _3D_FACE_API_
#define  _3D_FACE_API_

#include "GlobalConfig.h"

/* Loading the 3D face model by file name                      */
__declspec(dllexport) bool Load3dfaceModel(char *FileName);

/* Get the MorphableModel, you must call the Load3dfaceModel()
firstly and then call  this function.                       */
__declspec(dllexport) int GetMorphableModel(MorphableModel &model, int BlendShapeChannelCount = 15, int ExpressionBlendShapeCount = 0);

__declspec(dllexport) bool ReWriteFbxUVs(MorphableModel model, const char *filePath);


#endif
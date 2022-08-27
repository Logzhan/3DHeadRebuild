/****************************************************************************************

Copyright (C) 2014 Autodesk, Inc.
All rights reserved.

Use of this software is subject to the terms of the Autodesk license agreement
provided at the time of installation or download, or which otherwise accompanies
this software in either electronic or hard copy form.

****************************************************************************************/

#ifndef _SET_CAMERA_H
#define _SET_CAMERA_H

#include <opencv2\core.hpp>
using namespace cv;

typedef struct  _Camera{
	Vec4f postition;
	float Roll;
	float FieldOfViewY;
	Vec4f Eye,Up;
	Vec4f Center;
}Camera;

void InitCamera(void);

Camera GetCurrentCamera(void);

void SetCamera(int pWindowWidth, int pWindowHeight);

void CameraZoom(int pZoomDepth);

void CameraOrbit(Vec4f lOrigCamPos, double OrigRoll, int dX, int dY);

void RotateVector(Vec4f srcVec, int rx, int ry, int rz, Vec4f &dstVec);

#endif // #ifndef _SET_CAMERA_H







/****************************************************************************************
 * File         :   SetCamera.cpp 
 * Author       :   Zhanli  719901725@qq.com UESTC
 * Description  :   1¡¢find the current camera;
                    2¡¢get the relevant settings of a camera depending on it's projection
                       type and aperture mode;
                    3¡¢ compute the orientation of a camera.
****************************************************************************************/
#include "SetCamera.h"
#include "GL/glut.h"
#include "SceneContext.h"
#include "OpenglCfg.h"
#include "iostream"

#define   PI_DIV_180   3.1459265358979 / 180.0000f 
Camera mCamera;

/***************************************************************
 * Descripition : Config the camera initial parameters. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void InitCamera(void){
	/* Setting camera intialize parameters.       */
	mCamera.Eye    = Vec4f(0.0f,75.0f,300.0f,1.0f);
	mCamera.Up     = Vec4f(0.0f,1.0f, 0.0f,  1.0f);
	mCamera.Center = Vec4f(0.0f,0.0f, 0.0f,  1.0f);
	mCamera.Roll   = 0.0f;
	mCamera.FieldOfViewY = 5.0f;
	mCamera.postition = mCamera.Eye;
}
/***************************************************************
 * Descripition : Getting current camera data struct. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
Camera GetCurrentCamera(void){
	return mCamera;
}
/***************************************************************
 * Descripition : Set the view to the current camera settings. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void SetCamera(int pWindowWidth, int pWindowHeight)
{
    /* Compute the camera position and direction. */
    Vec3f lEye(0,0,1);
    Vec3f lCenter(0,0,0);
    Vec3f lUp(0,1,0);
    Vec3f lForward, lRight;

	float lAspectRatio        = 1.6f;

	/* Compute the viewport.                      */

    double lRealScreenRatio = (double)pWindowWidth / (double)pWindowHeight;
    int  lViewPortPosX  = 0, lViewPortPosY = 0, 
         lViewPortSizeX = pWindowWidth, 
         lViewPortSizeY = pWindowHeight;
    if( lRealScreenRatio > lAspectRatio){
        lViewPortSizeY = pWindowHeight;
        lViewPortSizeX = (int)( lViewPortSizeY * lAspectRatio);
        lViewPortPosY = 0;
        lViewPortPosX = (int)((pWindowWidth - lViewPortSizeX) * 0.5);
    }else{
        lViewPortSizeX = pWindowWidth;
        lViewPortSizeY = (int)(lViewPortSizeX / lAspectRatio);
        lViewPortPosX = 0;
        lViewPortPosY = (int)((pWindowHeight - lViewPortSizeY) * 0.5);
    }

	for(int i = 0; i < 3; i++){
		lEye[i] = mCamera.postition[i];
		lUp[i]  = mCamera.Up[i];
	}

	lForward = lCenter - lEye;
	lForward = normalize(lForward);
	lRight   = lForward.cross(lUp);
	lRight   = normalize(lRight);
	lUp      = lRight.cross(lForward);
	lUp      = normalize(lUp);

	double lRadians = mCamera.Roll * PI_DIV_180;
	lUp = lUp * cos( lRadians) + lRight * sin(lRadians);

	double lNearPlane    = 0.1;
	double lFarPlane     = 10000;
	float  lFilmOffsetX  = 0;
	float  lFilmOffsetY  = 0;

	double lFieldOfViewY = mCamera.FieldOfViewY;

    GlSetCameraPerspective(lFieldOfViewY, lAspectRatio, lNearPlane, lFarPlane, lEye, lCenter, lUp, 0, 0);

    glViewport( lViewPortPosX, lViewPortPosY, lViewPortSizeX, lViewPortSizeY);
}
/***************************************************************
 * Descripition : Scale the scene by  the key '+' and '-'. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void CameraZoom(int pZoomDepth)
{
	mCamera.FieldOfViewY = mCamera.FieldOfViewY - pZoomDepth*0.05f;
	/*Field View Y must bigger than zero.         */
	if(mCamera.FieldOfViewY < 0){
		mCamera.FieldOfViewY = 0;
	}  
}
/***************************************************************
 * Descripition : Using it to rotate the 3D scene.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void CameraOrbit(Vec4f lOrigCamPos, double OrigRoll, int dX, int dY)
{
	/* If the mouse not drag, don't do anythings. */
    if (dX == 0 && dY == 0) return;

	Vec4f lCenter   = mCamera.Center;
	Vec4f lPosition = mCamera.postition;
	
    Vec4f lCurPosition = lPosition   - lCenter;
    Vec4f lNewPosition = lOrigCamPos - lCenter;

	/* Rotate the camera position by dX and dY.   */
	RotateVector(lNewPosition, -dY, dX, 0, lNewPosition);

    /* Detect camera flip                         */
    if (   lNewPosition[0]*lCurPosition[0] < 0 
        && lNewPosition[2]*lCurPosition[2] < 0) {
			float lRoll  = mCamera.Roll;
            lRoll        = 180.0f - lRoll;
			mCamera.Roll = lRoll;
    }
	/* Update the new camera position.            */
    lNewPosition = lNewPosition + lCenter;
	for(int i = 0; i < 4; i++)
		mCamera.postition[i] = (float)lNewPosition[i];
}

/***************************************************************
 * Descripition : Rotate the vector by Euler Angle. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void RotateVector(Vec4f srcVec, int rx, int ry, int rz, Vec4f &dstVec){
	
	float Q[4];
	float T[3][3];
	Vec4f src = srcVec;

	/* Conver the distance to Euler Angle.        */
	float theta_z =  rz * 3.1416f/180.f;
	float theta_y =  ry * 3.1416f/180.f;
	float theta_x =  rx * 3.1416f/180.f;

	float cos_z_2 = cos(0.5f*theta_z);   float sin_z_2 = sin(0.5f*theta_z);
	float cos_y_2 = cos(0.5f*theta_y);   float sin_y_2 = sin(0.5f*theta_y);
	float cos_x_2 = cos(0.5f*theta_x);   float sin_x_2 = sin(0.5f*theta_x);

	/* Convert Euler Angle to Quaternion.         */
	Q[0] = cos_z_2*cos_y_2*cos_x_2 + sin_z_2*sin_y_2*sin_x_2;
	Q[1] = cos_z_2*cos_y_2*sin_x_2 - sin_z_2*sin_y_2*cos_x_2;
	Q[2] = cos_z_2*sin_y_2*cos_x_2 + sin_z_2*cos_y_2*sin_x_2;
	Q[3] = sin_z_2*cos_y_2*cos_x_2 - cos_z_2*sin_y_2*sin_x_2;

	/* Convert quaternion to rotation matrix.     */
	T[0][0] =   Q[0]*Q[0]+Q[1]*Q[1]-Q[2]*Q[2]-Q[3]*Q[3] ;
    T[0][1] =                    2*(Q[1]*Q[2]-Q[0]*Q[3]);
    T[0][2] =                    2*(Q[1]*Q[3]+Q[0]*Q[2]);

    T[1][0] =                    2*(Q[1]*Q[2]+Q[0]*Q[3]);
    T[1][1] =   Q[0]*Q[0]-Q[1]*Q[1]+Q[2]*Q[2]-Q[3]*Q[3] ;
    T[1][2] =                    2*(Q[2]*Q[3]-Q[0]*Q[1]);

    T[2][0] =                    2*(Q[1]*Q[3]-Q[0]*Q[2]);
    T[2][1] =                    2*(Q[2]*Q[3]+Q[0]*Q[1]);
    T[2][2] =   Q[0]*Q[0]-Q[1]*Q[1]-Q[2]*Q[2]+Q[3]*Q[3] ;

	/* Rotate the vector by rotation matrix.      */
	for(int i = 0; i < 3; i++){
		      dstVec[i] = T[0][i]*src[0] + 
			              T[1][i]*src[1] + 
						  T[2][i]*src[2];
	}
}
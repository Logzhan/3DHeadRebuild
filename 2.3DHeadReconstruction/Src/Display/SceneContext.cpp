/****************************************************************************************
 * File         :   SceneContext.cpp 
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   3D Head Rebuild Project. 
 * Time         :   2017-01-08
 * CopyRights   :   UESTC Kb545 All rights reserved.
****************************************************************************************/
#include "GlobalConfig.h"
#include "GL/glew.h"
#include "GL/glut.h"
#include "SceneContext.h"
#include "SetCamera.h"
#include "DrawScene.h"
#include "facefitting.h"
/***************************************************************
 * Descripition : Construction function.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
SceneContext::SceneContext(int pWindowWidth, int pWindowHeight)
{
	/* Init the window size parameters.   */
	mStatus = MUST_BE_REFRESHED;
	mWindowWidth  = pWindowWidth;
	mWindowHeight = pWindowHeight;
	cap = VideoCapture(0);
}

SceneContext::~SceneContext(){
}
/***************************************************************
 * Descripition : Draw the 3D head functions. 
                  1.Config OpenGL parameter.
				  2.Setting the OpenGL camera.
				  3.Drawing 3D.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
bool SceneContext::OnDisplay()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    glPushAttrib(GL_ENABLE_BIT);
	/* Enable OpenGL lighting.            */
    glPushAttrib(GL_LIGHTING_BIT);

    glEnable(GL_DEPTH_TEST);
    glEnable(GL_CULL_FACE);

	vector<Point2f> result;

	Mat image; float angle = 0;

	if (cap.isOpened()){
		cap >> image;
		FaceTracking(result, image, angle);
		angle = angle / 10;
	}

	Camera mc = GetCurrentCamera();
	for (int i = 0; i < 4; i++){
		mCamPosition[i] = mc.postition[i];
		mCamCenter[i] = mc.Center[i];
	}
	mRoll = mc.Roll;

	CameraOrbit(mCamPosition, mRoll, angle, 0);

	/* Setting the camera location.       */
    SetCamera(mWindowWidth, mWindowHeight);
	/* Draw 3D Model                      */

	//DrawMesh(model.Eyes,OpenGLTextureObject);
	DrawMesh(model.Head,OpenGLTextureObject);

	glPopAttrib();
	glPopAttrib();
	/* Update the status.                 */
	mStatus = MUST_BE_REFRESHED;
    return true;
}
/***************************************************************
* Descripition : Draw the 3D head functions with face tracking.
1.Config OpenGL parameter.
2.Setting the OpenGL camera.
3.Drawing 3D.
* Author       : ZhanLi @UESTC
* Time         : 2017-03-06
***************************************************************/
bool SceneContext::OnTracking()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glPushAttrib(GL_ENABLE_BIT);
	/* Enable OpenGL lighting.            */
	glPushAttrib(GL_LIGHTING_BIT);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);

	/* Setting the camera location.       */
	SetCamera(mWindowWidth, mWindowHeight);
	/* Draw 3D Model                      */

	//DrawMesh(model.Eyes,OpenGLTextureObject);
	DrawMesh(model.Head, OpenGLTextureObject);

	glPopAttrib();
	glPopAttrib();
	/* Update the status.                 */
	mStatus = REFRESHED;
	return true;
}
/***************************************************************
 * Descripition : Resize callback function. When the window 's
                  size was be change, the function will be call
				  and update the OpenGL view. 
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void SceneContext::OnReshape(int pWidth, int pHeight)
{
    glViewport(0, 0, (GLsizei)pWidth, (GLsizei)pHeight);
    mWindowWidth  = pWidth;
    mWindowHeight = pHeight;
}
/***************************************************************
 * Descripition : Keyboard callback function.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void SceneContext::OnKeyboard(unsigned char pKey)
{
    /* Zoom In on '+' or '=' keypad keys  */
    if (pKey == 43 || pKey == 61){
		CameraZoom(10);
		mStatus = MUST_BE_REFRESHED;
    }
    /* Zoom Out on '-' or '_' keypad keys */
    if (pKey == 45 || pKey == 95)
    {
		CameraZoom(0 - 10);
        mStatus = MUST_BE_REFRESHED;
    }
}
/***************************************************************
 * Descripition : Mouse click callback function.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void SceneContext::OnMouse(int pButton, int pState, int pX, int pY)
{
	Camera mc = GetCurrentCamera();
	for(int i = 0; i < 4; i++){
		mCamPosition[i]   = mc.postition[i];
		mCamCenter[i]     = mc.Center[i];
	}
	mRoll          = mc.Roll; 
    mLastX = pX;
    mLastY = pY;
}
/***************************************************************
 * Descripition : Mouse motion callback function.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
void SceneContext::OnMouseMotion(int pX, int pY)
{
	/* Compute the camera position by pX 
	   and pY.                            */
	CameraOrbit(mCamPosition, mRoll, pX-mLastX, mLastY-pY);
	/* Camera poistion was updated, and the
	   screen must be refeshed.           */
    mStatus = MUST_BE_REFRESHED;
}


void SceneContext::DrawMesh(Mesh mesh,unsigned int lTexture){

	int lPolygonCount = mesh.TriangleNum;                                                                     //如果加载贴图成功，调用OpenGL函数进行渲染
	glEnable(GL_TEXTURE_2D);
	glEnable(GL_POLYGON_SMOOTH);
	glBindTexture(GL_TEXTURE_2D, lTexture);
	glBegin(GL_TRIANGLES);
	for (int lPolygonIndex = 0; lPolygonIndex < lPolygonCount; ++lPolygonIndex)                               //对所有三角形进行操作                  
	{
		for (int lVerticeIndex = 0; lVerticeIndex < POLYGON_VERTEX_NUM; ++lVerticeIndex)                      //对单个三角形进行操作
		{
			int    Indices = mesh.tvi[lPolygonIndex][lVerticeIndex];

			Vec2f  mUVs    = mesh.texcoords[lPolygonIndex][lVerticeIndex];

			Vec4f  Vertex  = mesh.vertices[0][Indices];

			/* Call OpenGL function to draw 3d model */
			glTexCoord2d(mUVs[0], mUVs[1]);
			glVertex3f(Vertex[0],Vertex[1],Vertex[2]);	
		}
	}
	glEnd();
}

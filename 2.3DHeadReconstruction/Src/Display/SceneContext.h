/****************************************************************************************

Copyright (C) 2014 Autodesk, Inc.
All rights reserved.

Use of this software is subject to the terms of the Autodesk license agreement
provided at the time of installation or download, or which otherwise accompanies
this software in either electronic or hard copy form.

****************************************************************************************/

#ifndef _SCENE_CONTEXT_H
#define _SCENE_CONTEXT_H

#include "GlobalConfig.h"
// This class is responsive for loading files and recording current status as
// a bridge between window system such as GLUT or Qt and a specific FBX scene.
class SceneContext
{
public:
    enum Status
    {
        UNLOADED,               // Unload file or load failure;
        MUST_BE_LOADED,         // Ready for loading file;
        MUST_BE_REFRESHED,      // Something changed and redraw needed;
        REFRESHED               // No redraw needed.
    };
    Status GetStatus() const { return mStatus; }

    // Initialize with a .FBX, .DAE or .OBJ file name and current window size.
    SceneContext(int pWindowWidth, int pWindowHeight);
    ~SceneContext();


    // Call this method when redraw is needed.
    bool OnDisplay();
    // Call this method when window size is changed.
    void OnReshape(int pWidth, int pHeight);
    // Call this method when keyboard input occurs.
    void OnKeyboard(unsigned char pKey);
    // Call this method when mouse buttons are pushed or released.
    void OnMouse(int pButton, int pState, int pX, int pY);
    // Call this method when mouse is moved.
    void OnMouseMotion(int pX, int pY);
    // Call this method when timer is finished, for animation display.
    void OnTimerClick() const;

	bool OnTracking();

    // Methods for creating menus.
    // Get all the cameras in current scene, including producer cameras.

    // The input index is corresponding to the array returned from GetAnimStackNameArray.
    bool SetCurrentAnimStack(int pIndex);
    // Set the current camera with its name.
    bool SetCurrentCamera(const char * pCameraName);
    // The input index is corresponding to the array returned from GetPoseArray.
    bool SetCurrentPoseIndex(int pPoseIndex);
    // Set the currently selected node from external window system.

	void DrawMesh(Mesh mesh,unsigned int lTexture);

    // Pause the animation.
    void SetPause(bool pPause) { mPause = pPause; }
    // Check whether the animation is paused.
    bool GetPause() const { return mPause; }

	unsigned int OpenGLTextureObject;

	MorphableModel model;

	VideoCapture cap;

    enum CameraZoomMode
    {
        ZOOM_FOCAL_LENGTH,
        ZOOM_POSITION
    };
    CameraZoomMode  GetZoomMode()        { return mCameraZoomMode; }
    void            SetZoomMode( CameraZoomMode pZoomMode);  

private:
    // Display information about current status in the left-up corner of the window.
    void DisplayWindowMessage();
    // Display a X-Z grid.

    enum CameraStatus
    {
        CAMERA_NOTHING,
        CAMERA_ORBIT,
        CAMERA_ZOOM,
        CAMERA_PAN
    };

    const char * mFileName;
    mutable Status mStatus;

    int mPoseIndex;
    // Data for camera manipulation
    mutable int mLastX, mLastY;
    mutable  Vec4f mCamPosition, mCamCenter;
    mutable double mRoll;
    mutable CameraStatus mCameraStatus;

    bool mPause;

    bool mSupportVBO;

    //camera zoom mode
    CameraZoomMode mCameraZoomMode;

    int mWindowWidth, mWindowHeight;

};

// Initialize GLEW, must be called after the window is created.
bool InitializeOpenGL();



#endif // _SCENE_CONTEXT_H


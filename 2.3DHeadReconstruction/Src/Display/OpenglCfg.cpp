/****************************************************************************************
 * File         :   OpenglCfg.cpp 
 * Author       :   Zhanli  719901725@qq.com UESTC
 * Description  :   This file is contain the functions about OpenGL. 
****************************************************************************************/
#include <windows.h>
#include "GL/glew.h"
#include "GL/glut.h"
#include "GlobalConfig.h"
#include "SceneContext.h"
#include "OpenglCfg.h"
#include "SetCamera.h"

extern SceneContext * gSceneContext;

/***************************************************************
 * Descripition : Initializing the OpenGL config parameter.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void InitOpenGLConfig(int *argc, char **argv, int winWidth,int winHeight){

    glutInit(argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
	/* Setting window size.                       */
    glutInitWindowSize(winWidth, winWidth); 
	/* Settting the windows position.             */
    glutInitWindowPosition(100, 100);
	/* Create windows which named 3D head rebuild.*/
    glutCreateWindow("3D Head Rebuild");
	
	GLenum lError = glewInit();
    if (lError != GLEW_OK){
		printf("Glew Init Error!\n");
		system("pause");
		exit(1);
    }
	/* Checking the version of OpenGL glew.       */
	if (!GLEW_VERSION_1_5){
		/* Display error imforamtion.             */
        printf("The OpenGL version should be at least \
			    to display shaded scene!\n");
        system("pause");
		exit(1);
    }
    glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	/* Display the windows with black.            */
    glClearColor(0.0, 0.0, 0.0, 0.0);
}

/***************************************************************
 * Descripition : Initializing the OpenGL callback function.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void InitOpenGLCallback()
{
	/* Handle the resize of the 3d display window.*/
	glutReshapeFunc(ReshapeCallback);
	/* Seeting keyboard callback function.        */
    glutKeyboardFunc(KeyboardCallback);           
	/* Setting mouse click callback function.     */
    glutMouseFunc(glMouseCallback);  
	/* Setting mouse drag callback function.      */
    glutMotionFunc(MotionCallback);        
	/* Setting OpenGL display callback functions. */
    glutDisplayFunc(DisplayCallback);    
	/* Init the camera config parameters.         */
	InitCamera();
	/* Start the timer.                           */
	InitTimer();
}
/***************************************************************
 * Descripition : Refresh the application window.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void DisplayCallback()
{
	/* Setting display callback function.         */
    gSceneContext->OnDisplay();
	
    glutSwapBuffers();
}
/***************************************************************
 * Descripition : Resize the application window.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void ReshapeCallback(int pWidth, int pHeight)
{
    gSceneContext->OnReshape(pWidth, pHeight);
}
/***************************************************************
 * Descripition : Select diffrent function by using keyboard.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void KeyboardCallback(unsigned char pKey, int /*pX*/, int /*pY*/)
{
    if (pKey == 27)exit(0);
    gSceneContext->OnKeyboard(pKey);
}
/***************************************************************
 * Descripition : Control the OpenGL window by using Mouse.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void glMouseCallback(int button, int state, int x, int y)
{
    gSceneContext->OnMouse(button, state, x, y);
}
/***************************************************************
 * Descripition : Control the OpenGL window by using Mouse.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-12-28
***************************************************************/
void MotionCallback(int x, int y)
{
    gSceneContext->OnMouseMotion(x, y);
}
/***************************************************************
 * Descripition : Trigger te display of the current frame.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void TimerCallback(int)
{
    // Ask to display the current frame only if necessary.
    if (gSceneContext->GetStatus() == SceneContext::MUST_BE_REFRESHED){
        glutPostRedisplay();
    }
	Sleep(1);
	int fps = 60;
    glutTimerFunc((unsigned int)(1000 / fps), TimerCallback, 0);
}

/***************************************************************
 * Descripition : Init timer.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-05
***************************************************************/
void InitTimer(void)
{
	int fps = 60;
	glutTimerFunc((unsigned int)(1000 / fps), TimerCallback, 0);
}


/***************************************************************
 * Descripition : Setting OpenGL camera perspective parameters.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-04
***************************************************************/
void GlSetCameraPerspective(double pFieldOfViewY,
                            double pAspect,
                            double pNearPlane,
                            double pFarPlane,
                            Vec3f& pEye,
                            Vec3f& pCenter,
                            Vec3f& pUp,
                            double pFilmOffsetX,
                            double pFilmOffsetY)
{
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    glTranslated( pFilmOffsetX, pFilmOffsetY, 0);
    gluPerspective(pFieldOfViewY, 
                   pAspect, 
                   pNearPlane, 
                   pFarPlane);
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
    gluLookAt(pEye[0], pEye[1], pEye[2],
              pCenter[0], pCenter[1], pCenter[2],
              pUp[0], pUp[1], pUp[2]);
}
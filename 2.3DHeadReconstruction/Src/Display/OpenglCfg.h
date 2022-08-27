#ifndef _OPENGL_CFG_H_
#define _OPENGL_CFG_H_

void InitOpenGLConfig(int *argc, char **argv, int winWidth,int winHeight);
/*       Initialize the OpenGL callback function         */
void InitOpenGLCallback(void);

void DisplayCallback();
/*       OpenGL callback function                        */
void ReshapeCallback(int pWidth, int pHeight);

void KeyboardCallback(unsigned char pKey, int /*pX*/, int /*pY*/);

void glMouseCallback(int button, int state, int x, int y);

void MotionCallback(int x, int y);

void GlSetCameraPerspective(double pFieldOfViewY,
                            double pAspect,
                            double pNearPlane,
                            double pFarPlane,
                            Vec3f& pEye,
                            Vec3f& pCenter,
                            Vec3f& pUp,
                            double pFilmOffsetX,
                            double pFilmOffsetY);

void InitTimer(void);

void TimerCallback(int);


#endif
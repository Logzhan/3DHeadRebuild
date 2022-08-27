#ifndef _TEXTURE_H_
#define _TEXTURE_H_

#include "GL/glew.h"

using namespace cv;

/* ͨ��OpenCV Mat ������ͼ */
bool LoadTextureFromMat(Mat &Texture, unsigned int & pTextureObject);
/*  Bind OpenGL textrue             */
bool BindOpenGLTexture(Mat &Pic, unsigned int & pGLTextureObject);

#endif
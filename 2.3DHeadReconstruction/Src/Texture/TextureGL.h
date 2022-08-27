#ifndef _TEXTURE_H_
#define _TEXTURE_H_

#include "GL/glew.h"

using namespace cv;

/* Í¨¹ıOpenCV Mat ¼ÓÔØÌùÍ¼ */
bool LoadTextureFromMat(Mat &Texture, unsigned int & pTextureObject);
/*  Bind OpenGL textrue             */
bool BindOpenGLTexture(Mat &Pic, unsigned int & pGLTextureObject);

#endif
#include <opencv2\imgproc.hpp>
#include "TextureGL.h"


/* 给定纹理图像以及纹理的句柄，绑定纹理 */
bool LoadTextureFromMat(Mat &Texture, unsigned int & pTextureObject)
{
	IplImage* image = &IplImage(Texture);
	if (image == NULL) return false;
	/* 产生贴图 */
	glGenTextures(1, &pTextureObject);
	/* 绑定贴图 */
	glBindTexture(GL_TEXTURE_2D, pTextureObject);
	/* 贴图参数配置 */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	/* 设置纹理环绕 */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);

	glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, image->width, image->height, 0, GL_BGR, GL_UNSIGNED_BYTE, image->imageData);
	/* 解除绑定 */
	glBindTexture(GL_TEXTURE_2D, 0);

	return true;
}

/* 给定纹理图像以及纹理的句柄，绑定纹理 */
bool BindOpenGLTexture(Mat &Pic, unsigned int & pGLTextureObject)
{
	IplImage* image = &IplImage(Pic);
	if (image == NULL) return false;
	/* 产生贴图 */
	glGenTextures(1, &pGLTextureObject);
	/* 绑定贴图 */
	glBindTexture(GL_TEXTURE_2D, pGLTextureObject);
	/* 贴图参数配置 */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	/* 设置纹理环绕 */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);

	glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, image->width, image->height, 0, GL_BGR, GL_UNSIGNED_BYTE, image->imageData);
	/* 解除绑定 */
	glBindTexture(GL_TEXTURE_2D, 0);

	return true;
}



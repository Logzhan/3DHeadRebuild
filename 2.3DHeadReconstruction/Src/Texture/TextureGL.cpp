#include <opencv2\imgproc.hpp>
#include "TextureGL.h"


/* ��������ͼ���Լ�����ľ���������� */
bool LoadTextureFromMat(Mat &Texture, unsigned int & pTextureObject)
{
	IplImage* image = &IplImage(Texture);
	if (image == NULL) return false;
	/* ������ͼ */
	glGenTextures(1, &pTextureObject);
	/* ����ͼ */
	glBindTexture(GL_TEXTURE_2D, pTextureObject);
	/* ��ͼ�������� */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	/* ���������� */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);

	glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, image->width, image->height, 0, GL_BGR, GL_UNSIGNED_BYTE, image->imageData);
	/* ����� */
	glBindTexture(GL_TEXTURE_2D, 0);

	return true;
}

/* ��������ͼ���Լ�����ľ���������� */
bool BindOpenGLTexture(Mat &Pic, unsigned int & pGLTextureObject)
{
	IplImage* image = &IplImage(Pic);
	if (image == NULL) return false;
	/* ������ͼ */
	glGenTextures(1, &pGLTextureObject);
	/* ����ͼ */
	glBindTexture(GL_TEXTURE_2D, pGLTextureObject);
	/* ��ͼ�������� */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	/* ���������� */
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);

	glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, image->width, image->height, 0, GL_BGR, GL_UNSIGNED_BYTE, image->imageData);
	/* ����� */
	glBindTexture(GL_TEXTURE_2D, 0);

	return true;
}



/****************************************************************************************
 * File         :   TextureCV.cpp
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   This document is mainly image processing part.
 * Time         :   2017-11-08
 * CopyRights   :   UESTC Kb545
****************************************************************************************/
#include "GlobalConfig.h"
#include <opencv2\highgui.hpp>
#include "facefitting.h"
#include "TextureCV.h"
/***************************************************************
 * Descripition : Generate 3d head texture. This functions has
                  serveral part.
                  1. Compute the General skin picture's mean c-
				  olor.
				  2. Extract the mean color from user's input
				  pitcure.
				  3. Blending the usr's face to general skin p-
				  icture.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
Vec3f GenerateTexture(Mat &face, Mat &skin, vector<Point2f> facedection){

	int index[6] = {36,39,42,45,60,64};
	/* Whiten the usr's face                      */
	SkinWhitening(face,face,3);
	/* */
	Vec3b Colorskin = ImageMeanColor(skin); 
	/* 提取人脸皮肤平均颜色  */
	Vec3b Colorface = ExtractSkinColor(face,facedection);
	/* 计算偏移   */
	Vec3f Offset;
	for (int i = 0; i < 3; i++)Offset[i] = (float)(Colorface[i] - Colorskin[i]);
	/* 调整皮肤贴图的颜色，以适应人脸 */
	MatchColor(skin, skin, Offset);

	Mat Mask = face.clone();

	FillBackground(face, Mask,facedection);

	vector<Vec2f> ModelPoint,PhotoPoint;
	for(int i = 0; i < 6; i++ ){
		ModelPoint.push_back(facedection[index[i]]);
	}

	PhotoPoint.push_back(Vec2f(583 ,841));
	PhotoPoint.push_back(Vec2f(633 ,842));
	PhotoPoint.push_back(Vec2f(732 ,842));
	PhotoPoint.push_back(Vec2f(772 ,841));
	PhotoPoint.push_back(Vec2f(645 ,984));
	PhotoPoint.push_back(Vec2f(719 ,984));

	Vec3f s =  LS(ModelPoint, PhotoPoint);

	Rect Range;
	Range.x = (int)((1 + s[1])*(1.0f/s[0]));
	Range.y = (int)((1 + s[2])*(1.0f/s[0]));
	Range.width =  (int)((face.size().width  + s[1])*(1.0f/s[0])) - Range.x;
	Range.height = (int)((face.size().height + s[2])*(1.0f/s[0])) - Range.y;

	Mat Maskdst;

	resize(face,face,Size(Range.width,Range.height),0,0);
	resize(Mask,Maskdst,Size(Range.width,Range.height),0,0);
	
	Mat skindst =  skin.clone();

	EdgeFeather(skin, face, Maskdst, skindst, Range);

	Mat dstface = skindst(Range).clone();
	resize(dstface,dstface,Size(512,512),0,0);

#if OPENCV_DEBUG_INFORMATION
	imshow("General Skin",skindst);
	imshow("Dst face",    dstface);
	waitKey(1);
#endif
	skindst =  RoateImage(skindst,-90);
	
	skin = skindst.clone();
	face = dstface.clone();

	return s;
}

Vec3f GenerateTexture(Mat &face, Mat &skin, vector<Vec2f> facedection){
	vector<Point2f> tmp;
	for(unsigned int i = 0; i < facedection.size(); i++){
		tmp.push_back(Point2f(facedection[i][0],
			                  facedection[i][1]));
	}
	return GenerateTexture(face, skin, tmp);
}


/* 旋转图像-90度 */
Mat RoateImage(Mat &img,int degree)
{
	IplImage* src = &IplImage(img);  
    IplImage* srcCopy = cvCreateImage(cvSize(src->height, src->width),src->depth, src->nChannels);  
    cvTranspose(src, srcCopy);  
	if(degree == -90){
	    cvFlip(srcCopy, NULL, 2);  
		cvFlip(srcCopy, NULL, 1);
		cvFlip(srcCopy, NULL, 0);
	}else if(degree == 90){
		cvFlip(srcCopy, NULL, 1);
	}
	Mat res = Mat(srcCopy).clone();
	cvReleaseImage(&srcCopy);
	return res;
}


void FillBackground(const Mat &src, Mat &dst, vector<Point2f> p)
{
	vector<Point2i> polygon;
	Point2i tmp;
	/* 调整顺序,形成正确顺序的多边形 */
	for (int i = 0; i <= 16; i++){
		tmp.x = (int)p[i].x; tmp.y = (int)p[i].y;
		polygon.push_back(tmp);
	}
	for (int i = 26; i >= 17; i--){
		tmp.x = (int)p[i].x; tmp.y = (int)p[i].y;
		polygon.push_back(tmp);
	}
	/* 调整轮廓的位置，两边往里缩，往上移 */
	for (unsigned int i = 0; i < polygon.size(); i++){
		if (i <= 6){
			polygon[i].x = polygon[i].x + 35;
		}
		else if (i >= 8 && i <= 16){
			polygon[i].x = polygon[i].x - 35;
		}
		else{
			polygon[i].x = polygon[i].x;
		}
		polygon[i].y = polygon[i].y - 10;
	}
	dst = src.clone();
	for (int i = 0; i < src.rows; ++i){
		for (int j = 0; j < src.cols; ++j){
			if (IsPointInPolygon(polygon, Point(j, i))){
				dst.at<Vec3b>(i, j)[0] = 255;
				dst.at<Vec3b>(i, j)[1] = 255;
				dst.at<Vec3b>(i, j)[2] = 255;
			}
			else{
				dst.at<Vec3b>(i, j)[0] = 0;
				dst.at<Vec3b>(i, j)[1] = 0;
				dst.at<Vec3b>(i, j)[2] = 0;
			}
		}
	}
	cvSmooth(&IplImage(dst), &IplImage(dst), CV_MEDIAN, 31, 31);
	cvSmooth(&IplImage(dst), &IplImage(dst), CV_BLUR, 51, 51);
	//cvSmooth(&IplImage(dst), &IplImage(dst), CV_BLUR, 21, 21);
	//imshow("mask-test",dst);
}
/***************************************************************
 * Descripition : Determine if a point is within the polygon a-
                  rea. If the point is within polygon, return 
				  true.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
bool IsPointInPolygon(std::vector<Point> poly, Point pt)
{
	unsigned int i, j;
	bool c = false;
	for (i = 0, j = (unsigned int)poly.size() - 1; i < poly.size(); j = i++){
		if ((((poly[i].y <= pt.y) && (pt.y < poly[j].y)) ||
			((poly[j].y <= pt.y) && (pt.y < poly[i].y)))
			&& (pt.x < (poly[j].x - poly[i].x) * (pt.y - poly[i].y) / (poly[j].y - poly[i].y) + poly[i].x))
		{
			c = !c;
		}
	}
	return c;
}

/* 边缘羽化
   face : 前脸
   bg   : 皮肤背景图像
   Mask : 图像分割的掩膜，区分前脸和背景
   dst  : 目标图像
   Range: bg处理的范围
 */
void EdgeFeather(const Mat &bg, Mat face,const Mat &mask, Mat &dst, Rect Range){

	for (int i = Range.y; i < Range.y + Range.height; ++i){
		for (int j = Range.x; j < Range.x + Range.width; ++j){
			if(i < bg.size().height && j < bg.size().width){
				dst.at<Vec3b>(i, j)[0] = (uchar)((mask.at<Vec3b>(i - Range.y, j - Range.x)[0] / 255.0f)*(face.at<Vec3b>(i - Range.y, j - Range.x)[0]) + (1 - mask.at<Vec3b>(i - Range.y, j - Range.x)[0] / 255.0f)*(bg.at<Vec3b>(i, j)[0]));
				dst.at<Vec3b>(i, j)[1] = (uchar)((mask.at<Vec3b>(i - Range.y, j - Range.x)[1] / 255.0f)*(face.at<Vec3b>(i - Range.y, j - Range.x)[1]) + (1 - mask.at<Vec3b>(i - Range.y, j - Range.x)[1] / 255.0f)*(bg.at<Vec3b>(i, j)[1]));
				dst.at<Vec3b>(i, j)[2] = (uchar)((mask.at<Vec3b>(i - Range.y, j - Range.x)[2] / 255.0f)*(face.at<Vec3b>(i - Range.y, j - Range.x)[2]) + (1 - mask.at<Vec3b>(i - Range.y, j - Range.x)[2] / 255.0f)*(bg.at<Vec3b>(i, j)[2]));
			}
		}
	}
}

/* 皮肤颜色提取 */
Vec3b ExtractSkinColor(const Mat &src,const vector<Point2f> &result){
	int index[6] = {1,28,15,13,33,3}; 
	int Count = 0;
	vector<Point> polygon;
	Vec3d sum = Vec3d(0,0,0);Vec3b res;
	for(int i = 0; i < 6; i++){
		Point tmp;
		tmp.x = (int)result[index[i]].x;
		tmp.y = (int)result[index[i]].y;
		polygon.push_back(tmp);
	}
	for (int i = 1; i <= src.rows; ++i){
		for (int j = 1; j <= src.cols; ++j){
			if (IsPointInPolygon(polygon, Point(j, i))){
				for(int k = 0; k < 3; k++){
					sum[k] += src.at<Vec3b>(i, j)[k];
				}
				Count++;
			}
		}
	}
	for(int k = 0; k < 3; k++){
		res[k] = (uchar)(sum[k] / Count);
	}
	return res;
}
/*给定源图像，同时给定颜色的偏移，对每个像素减去偏移 */
void MatchColor(const Mat &src,Mat &dst,Vec3f color){

	for (int i = 0; i < src.rows; ++i){
		for (int j = 0; j < src.cols; ++j){
			for (int k = 0; k < 3; k++){
				if (src.at<Vec3b>(i, j)[k] + color[k] > 255){
					dst.at<Vec3b>(i, j)[k] = 255;
				}else if (src.at<Vec3b>(i, j)[k] + color[k] < 0){
					dst.at<Vec3b>(i, j)[k] = 0;
				}else{
					dst.at<Vec3b>(i, j)[k] = (uchar)(src.at<Vec3b>(i, j)[k] + color[k]);
				}
			
			}
		}
	}
}
/***************************************************************
 * Descripition : Compute the mean color of input picture.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-12
***************************************************************/
Vec3b ImageMeanColor(const Mat &Img){
	
	Vec3d sum = Vec3d(0,0,0);
	long int count = 0; 
	Vec3b color;
	for (int i = 0; i < Img.rows; ++i){
		for (int j = 0; j < Img.cols; ++j){
			if(Img.at<Vec3b>(i, j)[0]!=0&&
			   Img.at<Vec3b>(i, j)[0]!=0&&
			   Img.at<Vec3b>(i, j)[0]!=0){

				sum[0] += Img.at<Vec3b>(i, j)[0];
				sum[1] += Img.at<Vec3b>(i, j)[1];
				sum[2] += Img.at<Vec3b>(i, j)[2];
				count++;
			}
		}
	}
	sum[0] = sum[0] / count;
	sum[1] = sum[1] / count;
	sum[2] = sum[2] / count;

	color[0] = (uchar)sum[0];
	color[1] = (uchar)sum[1];
	color[2] = (uchar)sum[2];
	return color;
}
/***************************************************************
 * Descripition : Make skin look whiter
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-22
***************************************************************/
void SkinWhitening(Mat src,Mat &dst,int beta){
	for (int i = 0; i < src.rows; ++i){
		for (int j = 0; j < src.cols; ++j){
			for(int c = 0; c < 3; c++){
				float t = src.at<Vec3b>(i, j)[c] / 255.0f;
				t = (float)(log(t * (beta - 1) + 1.0f) / log(beta));
				dst.at<Vec3b>(i, j)[c] = (unsigned char)(t * 255);
			}
		}
	}
}

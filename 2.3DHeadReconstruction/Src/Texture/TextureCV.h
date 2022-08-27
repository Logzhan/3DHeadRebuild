#ifndef _TEXTURECV_H_
#define _TEXTURECV_H_

using namespace cv;

/*  Making skin look whiter.                                           */
void SkinWhitening(Mat src,Mat &dst, int beta);

Vec3f GenerateTexture(Mat &face, Mat &skin, vector<Point2f> facedection);
Vec3f GenerateTexture(Mat &face, Mat &skin, vector<Vec2f> facedection);
/* Ðý×ªÍ¼Ïñ -90¶È */
Mat RoateImage(Mat &img,int degree);


void  FillBackground(const Mat &src, Mat &dst, vector<Point2f> p);

bool  IsPointInPolygon(std::vector<Point> poly, Point pt);

void  EdgeFeather(const Mat &bg, Mat face,const Mat &mask, Mat &dst, Rect Range);

void  MatchColor(const Mat &src,Mat &dst,Vec3f color);

Vec3b ExtractSkinColor(const Mat &src,const vector<Point2f> &result);

Vec3b ImageMeanColor(const Mat &Img);


#endif
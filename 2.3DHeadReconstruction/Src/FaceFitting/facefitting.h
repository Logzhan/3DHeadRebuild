#ifndef _FACE_FITTING_H_
#define _FACE_FITTING_H_

#include "glm/mat3x3.hpp"

using namespace std;
using namespace cv;


struct ScaledOrthoProjectionParameters {
	glm::mat3x3 R;
	double tx, ty;
	double s;
};
	
ScaledOrthoProjectionParameters estimate_orthographic_projection_linear(std::vector<cv::Vec2f> image_points, std::vector<cv::Vec4f> model_points);

int FaceDetection(vector<Point2f> &result, Mat &image);

int FaceDetection(vector<Vec2f> &result, Mat &Image);

int FaceTracking(vector<Point2f> &result, Mat &image, float &angle);

/* 利用最小二乘法，计算模型点到目标的位置匹配参数 */
Vec3f LS(vector<Vec2f> ModelPoint, vector<Vec2f>PhotoPoint);

#endif
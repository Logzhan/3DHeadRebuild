#include "GlobalConfig.h"
#include <opencv2\opencv.hpp>
#include <Eigen\Dense>
#include "facedetect-dll.h"	
#include "facefitting.h"

using namespace Eigen;

ScaledOrthoProjectionParameters estimate_orthographic_projection_linear(std::vector<cv::Vec2f> image_points, std::vector<cv::Vec4f> model_points)
{
	using cv::Mat;
	assert(image_points.size() == model_points.size());
	assert(image_points.size() >= 4); // Number of correspondence points given needs to be equal to or larger than 4

	const int num_correspondences = static_cast<int>(image_points.size());

	Mat A = Mat::zeros(2 * num_correspondences, 8, CV_32FC1);
	int row_index = 0;
	for (int i = 0; i < (int)model_points.size(); ++i)
	{
		Mat p = Mat(model_points[i]).t();
		p.copyTo(A.row(row_index++).colRange(0, 4)); // even row - copy to left side (first row is row 0)
		p.copyTo(A.row(row_index++).colRange(4, 8)); // odd row - copy to right side
	} // 4th coord (homogeneous) is already 1

	Mat b(2 * num_correspondences, 1, CV_32FC1);
	row_index = 0;
	for (unsigned int i = 0; i < image_points.size(); ++i)
	{
		b.at<float>(row_index++) = image_points[i][0];
		b.at<float>(row_index++) = image_points[i][1];
	}

	Mat k; // resulting affine matrix (8x1)
	bool solved = cv::solve(A, b, k, cv::DECOMP_SVD);

	const Mat R1 = k.rowRange(0, 3);
	const Mat R2 = k.rowRange(4, 7);
	const float sTx = k.at<float>(3);
	const float sTy = k.at<float>(7);
	const auto s = (cv::norm(R1) + cv::norm(R2)) / 2.0;
	Mat r1 = R1 / cv::norm(R1);
	Mat r2 = R2 / cv::norm(R2);
	Mat r3 = r1.cross(r2);
	Mat R;
	r1 = r1.t();
	r2 = r2.t();
	r3 = r3.t();
	R.push_back(r1);
	R.push_back(r2);
	R.push_back(r3);
	// Set R to the closest orthonormal matrix to the estimated affine transform:
	Mat S, U, Vt;
	cv::SVDecomp(R, S, U, Vt);
	Mat R_ortho = U * Vt;
	// The determinant of R must be 1 for it to be a valid rotation matrix
	if (cv::determinant(R_ortho) < 0)
	{
		U.row(2) = -U.row(2); // not sure this works...
		R_ortho = U * Vt;
	}

	// Remove the scale from the translations:
	const auto t1 = sTx / s;
	const auto t2 = sTy / s;

	// Convert to a glm::mat4x4:
	glm::mat3x3 R_glm; // identity
	for (int r = 0; r < 3; ++r) {
		for (int c = 0; c < 3; ++c) {
			R_glm[c][r] = R_ortho.at<float>(r, c);
		}
	}
	ScaledOrthoProjectionParameters sopp;
	sopp.R = R_glm;
	sopp.s = s;
	sopp.tx = t1;
	sopp.ty = t2;
	return sopp;
};

int FaceDetection(vector<Vec2f> &result, Mat &Image){
	vector<Point2f> v;

	FaceDetection(v,Image);

	result.resize(v.size());

	for(unsigned int i = 0; i < v.size(); i++){
		result[i][0] = v[i].x;
		result[i][1] = v[i].y;
	}


#if EYES_ENLARGEMENT
	int padding    =  EYES_ENLARGEMENT_PADDING;

	result[36][0] -=  padding;
	result[37][1] -=  padding;
	result[38][1] -=  padding;
	result[39][0] +=  padding;
	result[40][1] +=  padding;
	result[41][1] +=  padding;

	result[42][0] -=  padding;
	result[43][1] -=  padding;
	result[44][1] -=  padding;
	result[45][0] +=  padding;
	result[46][1] +=  padding;
	result[47][1] +=  padding;
#endif 

	return 0;
}

// New Add :: Zhan Li
int FaceDetection(vector<Point2f> &result,Mat &image){

	Mat gray;
	cvtColor(image, gray, CV_BGR2GRAY);
	Mat Detectresult = image.clone();
	int * pResults = NULL;
	//pBuffer is used in the detection functions.
	//If you call functions in multiple threads, please create one buffer for each thread!
	unsigned char * pBuffer = (unsigned char *)malloc(DETECT_BUFFER_SIZE);
	if (!pBuffer){
#if     OUTPUT_ERROR_INFORMATION 
		printf("Face Detection can not alloc buffer.\n");
		system("pause");
#endif 
		exit(1);
	}
	int doLandmark = 1;

	pResults = facedetect_frontal_surveillance(pBuffer, (unsigned char*)(gray.ptr(0)), gray.cols, gray.rows, (int)gray.step,
		1.2f, 2, 24, 0, doLandmark);
	
	if(!(pResults ? *pResults : 0)){
#if     OUTPUT_ERROR_INFORMATION 
		printf("The input picture can not detect the front face.\n");
		system("pause");
#endif 
		exit(1);
	}
	if((pResults ? *pResults : 0) >= 2){
#if     OUTPUT_ERROR_INFORMATION  
		printf("There are too many face in input picture.\n");
		system("pause");
#endif 
		exit(1);
	}

	//print the detection results
	for (int i = 0; i < (pResults ? *pResults : 0); i++)
	{
		short * p = ((short*)(pResults + 1)) + 142 * i;
		int x = p[0];
		int y = p[1];
		int w = p[2];
		int h = p[3];

		if (image.size().width == 512 && image.size().width == 512){
			int neighbors = p[4];
			int angle = p[5];
			if (doLandmark){
				for (int j = 0; j < 68; j++){
					Point2f tmp;
					tmp.x = p[6 + 2 * j];
					tmp.y = p[6 + 2 * j + 1];
					result.push_back(tmp);
				}
			}
		}
		else{

			float scale = 1.0f;
			for(int i = 2; i > 0; i--){
				Rect tmp;
				scale = 1 + 0.1f * i;
				//printf("scale = %f\n",scale);
				tmp.x = (int)(x - (w * scale - w) / 2.0f);
				tmp.y = (int)(y - (h * scale - h) / 2.0f);
				tmp.width  = (int)(w * scale);
				tmp.height = (int)(h * scale);
				if(tmp.x>=0&&tmp.y>=0&&tmp.x+tmp.width<=image.size().width&&tmp.y+tmp.height<=image.size().height){
					x = (int)(x - (w * scale - w) / 2.0f);
					y = (int)(y - (h * scale - h) / 2.0f);
					w = (int)(w * scale);
					h = (int)(h * scale);
					break;
				}
				
			}
			image = image(Rect(x, y, w, h));
			resize(image, image, Size(512, 512));
			FaceDetection(result, image);
		}
	}
	free(pBuffer);
	return 0;
}



// New Add :: Zhan Li
int FaceTracking(vector<Point2f> &result, Mat &image, float &angle){

	Mat gray;
	cvtColor(image, gray, CV_BGR2GRAY);
	Mat Detectresult = image.clone();
	int * pResults = NULL;
	//pBuffer is used in the detection functions.
	//If you call functions in multiple threads, please create one buffer for each thread!
	unsigned char * pBuffer = (unsigned char *)malloc(DETECT_BUFFER_SIZE);
	if (!pBuffer){
#if     OUTPUT_ERROR_INFORMATION 
		printf("Face Detection can not alloc buffer.\n");
		system("pause");
#endif 
		exit(1);
	}
	int doLandmark = 1;

	pResults = facedetect_multiview_reinforce(pBuffer, (unsigned char*)(gray.ptr(0)), gray.cols, gray.rows, (int)gray.step,
		1.2f, 2, 24, 0, doLandmark);

	if (!(pResults ? *pResults : 0)){
#if     OUTPUT_ERROR_INFORMATION 
		printf("The input picture can not detect the front face.\n");
		system("pause");
#endif 
		exit(1);
	}
//	if ((pResults ? *pResults : 0) >= 2){
//#if     OUTPUT_ERROR_INFORMATION  
//		printf("There are too many face in input picture.\n");
//		system("pause");
//#endif 
//		exit(1);
//	}
	Mat result_frontal_surveillance = image.clone();
	//print the detection results
	for (int i = 0; i < (pResults ? *pResults : 0); i++)
	{
		short * p = ((short*)(pResults + 1)) + 142 * i;
		int x = p[0];
		int y = p[1];
		int w = p[2];
		int h = p[3];
		int neighbors = p[4];
		angle = p[5];
		if (doLandmark){
			for (int j = 0; j < 68; j++){
				Point2f tmp;
				tmp.x = p[6 + 2 * j];
				tmp.y = p[6 + 2 * j + 1];
				result.push_back(tmp);

				circle(result_frontal_surveillance, Point((int)p[6 + 2 * j], (int)p[6 + 2 * j + 1]), 1, Scalar(0, 255, 0));
			}
		}
	}
	namedWindow("Results_frontal_surveillance", 1);
	imshow("Results_frontal_surveillance", result_frontal_surveillance);
	waitKey(1);
	free(pBuffer);
	return 0;
}


Vec3f LS(vector<Vec2f> ModelPoint, vector<Vec2f>PhotoPoint)
{
	int NumPoint = ModelPoint.size();
	int tempRow;
	float ErrorValue = 0;


	MatrixXf A(2 * NumPoint, 3);
	for (int row = 0; row < 2 * NumPoint; row++)
	{
		if (row < NumPoint)
		{
			A(row, 0) = PhotoPoint[row][0];
			A(row, 1) = -1;
			A(row, 2) = 0;
		}
		else
		{
			tempRow = row - NumPoint;
			A(row, 0) = PhotoPoint[tempRow][1];
			A(row, 1) = 0;
			A(row, 2) = -1;
		}

	}

	VectorXf b(2 * NumPoint);
	for (int row = 0; row < 2 * NumPoint; row++)
	{
		if (row < NumPoint)
		{
			b(row) = ModelPoint[row][0];
		}
		else
		{
			tempRow = row - NumPoint;
			b(row) = ModelPoint[tempRow][1];
		}
	}

	Vector3f coeff = A.jacobiSvd(ComputeThinU | ComputeThinV).solve(b);
	Vec3f s = Vec3f(coeff[0], coeff[1], coeff[2]);
	return s;
}
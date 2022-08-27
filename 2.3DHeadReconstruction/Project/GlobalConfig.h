/*******************************************************************
 * File        :  GlobalConfig.h
 * Destription :  This is important file. We are here define some i-
                  mportant data struct and some important config se-
				  tting. 
 * Author      :  Zhanli @ UESTC  719901725@qq.com		
 * Time        :  2018-1-11 
 * CopyRights  :  UESTC Kb545
 *******************************************************************/
#ifndef _GLOBAL_CONFIG_H_
#define _GLOBAL_CONFIG_H_

/* OpenCV general header file.                             */
#include <opencv2\core.hpp>
#include <opencv2\opencv.hpp>
#include <opencv2\imgproc.hpp>
#include <vector>

using namespace std;
using namespace cv;
/*******************************************************************
 * Note   :  We are here to set the porgramm compile and run mode.
             The mode one is Develop mode and mode two is server run
			 mode.
             In development model, We fitting face with default file
			 path. And we dont save anything.  But you can set path 
			 to save these information. In addition, we also output 
			 some debug information, such as OpenCV image¡¢programm
			 running logs.
			 In sever mode, we don't output any debug informaiton.
			 We load source file and output file by given file path 
			 from command line. So if we compile this programm serv-
			 er mode, we should using command line to start-up this 
			 programm.
			 In diffrent mode, we config with default setting, but 
			 you can reset these with your need.
 * Author :  Zhanli @ UESTC  719901725@qq.com		
 * Time   :  2018-1-11 
 *******************************************************************/
#define PROGRAMM_RUN_MODEL                               1

#if     PROGRAMM_RUN_MODEL == 1
	#define USE_DEVELOP_MODEL_CONFIG    
#endif
#if     PROGRAMM_RUN_MODEL == 2
	#define USE_SERVER_MODEL_CONFIG
#endif 


#ifdef  USE_DEVELOP_MODEL_CONFIG

/* Setting whether ouput the Error information             */
#define OUTPUT_ERROR_INFORMATION                         2
/* Setting whether ouput the Debug information             */
#define OUTPUT_DEBUG_INFORMATION                         0
/* Setting whether output the OpenCV Debug information.    */
#define OPENCV_DEBUG_INFORMATION                         0
/* Set whether enlargement the eyes.                       */
#define EYES_ENLARGEMENT                                 1
/* Setting the padding of eyes enlargement.                */
#define EYES_ENLARGEMENT_PADDING                         4

#endif

#ifdef  USE_SERVER_MODEL_CONFIG

/* Setting whether ouput the Error information             */
#define OUTPUT_ERROR_INFORMATION                         1
/* Setting whether ouput the Debug information             */
#define OUTPUT_DEBUG_INFORMATION                         0
/* Setting whether output the OpenCV Debug information.    */
#define OPENCV_DEBUG_INFORMATION                         0
/* Setting whether enlargement the eyes.                   */
#define EYES_ENLARGEMENT                                 1
/* Setting the padding of eyes enlargement.                */
#define EYES_ENLARGEMENT_PADDING                         8

#endif 

/* Define the number of BlendShape of the morphable model. */
#define BLENDSHAPE_NUM                                   29
/* Define the number of expression BlendShape number of the 
   morphable model.                                        */
#define EXPRESSION_NUM                                   7
/* Define the number of face landmraks.                    */
#define FACE_LANDMRAK_NUM                                68
/* Define the number of OpenCV polygon.                    */
#define POLYGON_VERTEX_NUM                               3

#define THREE_DIMENSIONAL_COORDINATES                    3

#define TWO_DIMENSIONAL_COORDINATES                      2

#define FACE_UV_RANGE_Threshold                          416.0f              

/* The 2d face landmarks to 3d vertex indices.             */
const int VertexIndices[FACE_LANDMRAK_NUM] = 
{
	1390,   2406,   1585,   2405,   1583,   1505,   1885,  1581,
	19  ,   321 ,   625 ,   245 ,   323 ,   1166,   325 ,  1167,
	130 ,   1905,   1906,   1683,   1681,   1684,   424 ,  421 ,
	423 ,   646 ,   645 ,   847 ,   44  ,   34  ,   23  ,  1750,
	1796,   36  ,   536,    490 ,   1726,   1707,   1712,  1548,
	1717,   1720,   288 ,   452 ,   447 ,   301 ,   462 ,  457 ,
	1508,   1432,   2245,   14  ,   999 ,   172 ,   248 ,  216 ,
	540 ,   15  ,   1800,   1476,   1317,   1354,   9   ,  94  , 
	57  ,   760 ,   11  ,   2018,
};
const int UVEdgePoints[76] = 
{
	227 , 317 , 322 , 549 , 616 , 619,  631 , 666 , 773 ,
	775 , 778 , 779 , 780 , 781 , 782,  783 , 784 , 785 ,
	786 , 787 , 788 , 789 , 790 , 791,  808 , 1028, 1033,
	1034, 1036, 1037, 1039, 1043, 1051, 1168, 1198, 1199,
	1204, 1206, 1211, 1487, 1577, 1582, 1809, 1876, 1879,
	1891, 1926, 2031, 2033, 2036, 2037, 2038, 2039, 2040,
	2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2065,
	2273, 2277, 2278, 2280, 2281, 2283, 2287, 2294, 2407,
	2437, 2438, 2443,
};

/* The Define of Texture Coordinate struct.                */
typedef struct _TextureCoordinate{
	vector <vector<Vec2f>> Headtexcoords,
		                   Eyestexcoords;
}TextureCoordinate; 

typedef struct _Mesh{
	int VertexNum;                                         /* The Number of vertex.                    */
	int UVNum;                                             /* Texture coordinates Number.              */
	int TriangleNum;                                       /* The number of the Mesh's triangle.       */
	int BlendShapeNum;                                     /* The BlendShape Number of the mesh        */
	std::vector<std::vector<cv::Vec4f>> vertices;	       /* Vertex coordinates.                      */
	std::vector<std::vector<cv::Vec4f>> expression;        /* Facial expression number.                */
	std::vector<std::vector<cv::Vec2f>> texcoords;         /* Texture Texture coordinates.             */
	std::vector<Vec3i> tvi;                                /* Triangle vertex indices.                 */
	std::vector<Vec3i> tci;                                /* Triangle color indices                   */
}Mesh;

typedef struct _MorphableModel{
	Mesh Head;
	Mesh Eyes;
}MorphableModel;

typedef struct _FilePath{
	string MorphableModelPath;                             /* Server load Morphable model path (*.fbx).*/
	string SkinFilePath;                                   /* General skin texture file path(*.png).   */
	string InputUsrPath;                                   /* User input front picture file path(.jpg).*/
	string UVFilePath;                                     /* Save UV file path(*.txt).                */
	string BlendShapePath;                                 /* Save blendshape file path(*.txt).        */
	string OutputFbxFilePath;                              /* Save morphable model file path.(*.fbx).  */
	string TextureFilePath;                                /* Save generated texture file path.(*.png) */
	string OutputObjFilePath;
}FilePath;

#endif
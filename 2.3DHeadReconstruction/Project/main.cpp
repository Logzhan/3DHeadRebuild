/****************************************************************************************
 * File         :   main.cpp 
 * Author       :   Zhanli  719901725@qq.com
 * Description  :   3D Head Rebuild Project. 
 * Time         :   2017-12-28 @Zhanli : Create this 3d head reconstruction framework.       
				    2017-01-05 @Zhanli : Add fitting part.					    
				    2017-01-08 @Zhanli : Add OpenGL display part.
                    2017-01-11 @Zhanli : Add Server save file part.  
					2017-01-16 @Zhanli : Add Obj model output support.
 * CopyRights   :   UESTC Kb545
****************************************************************************************/
#include "GlobalConfig.h"
#include "windows.h"
#include "time.h"
#include "TextureGL.h"
#include "GL/glut.h"
#include "SceneContext.h"
#include "3dfaceapi.h"
#include "OpenglCfg.h"
#include "FitfaceShape.h"
#include "FileOutput.h"
/***************************************************************
 * The definition of private varible
***************************************************************/
MorphableModel        mMorphableModel;            /*  Original Model we load from fbx file.     */
MorphableModel        OutputHeadModel;            /*  Target model, we generate for usr.        */
SceneContext *          gSceneContext;            /*  Scene handle using for controling display.*/
const int DEFAULT_WINDOW_WIDTH  = 720;            /*  Define intial window size width.          */
const int DEFAULT_WINDOW_HEIGHT = 486;            /*  Define intial window size height.         */
GLuint             lTextureObject = 0;            /*  OpenGL texture handle.                    */
FilePath                    mFilePath;            /*  Resoure file path and target file path we
												      need.                                     */
using namespace std;
/***************************************************************
 * Descripition : The enter of the programm.
 * Author       : ZhanLi @UESTC
 * Time         : 2017-01-08
***************************************************************/
//#pragma comment( linker, "/subsystem:\"windows\" /entry:\"mainCRTStartup\"" )

int main(int argc, char** argv)
{
#ifdef USE_DEVELOP_MODEL_CONFIG


	/*      Loading Morphable 3D head model.      */
	Load3dfaceModel("MorphableModel.fbx");
	/*      Get the model data struct             */
	GetMorphableModel(mMorphableModel, BLENDSHAPE_NUM, EXPRESSION_NUM);
 

	/* Init the rand function by time             */
	srand((unsigned)time(NULL));                   


	OutputHeadModel = Reconstruction3DHead("test.jpg",
		                                   "skin.png",
										   mMorphableModel);
	

	/* Create the scene to display 3d head model. */
	gSceneContext = new SceneContext(DEFAULT_WINDOW_WIDTH, 
		                             DEFAULT_WINDOW_HEIGHT);
	/* OpenGL init and config parameter.          */
	InitOpenGLConfig(&argc, argv, DEFAULT_WINDOW_WIDTH,
		                          DEFAULT_WINDOW_HEIGHT);
	/* Init OpenGL callback functions.            */
	InitOpenGLCallback();

	BindOpenGLTexture(GetTexture(), lTextureObject);

	gSceneContext->model               = OutputHeadModel;
	gSceneContext->OpenGLTextureObject = lTextureObject;

	/* Enter OpenGL mian loop.                    */
	glutMainLoop();

#endif

#ifdef USE_SERVER_MODEL_CONFIG

	for( int i = 1, c = argc; i < c; ++i )
	{
		if(i == 1){
		/*   Loading Morphable 3D head model.         */
			 mFilePath.MorphableModelPath = argv[i];
		}else if(i == 2){
		/*  Loading skin texture file path.           */
			mFilePath.SkinFilePath        = argv[i];
		}else if(i == 3){
		/*  Input usr picture file path.              */
			mFilePath.InputUsrPath        = argv[i];
		}
		else if(i == 4){
		/*  Output blendshape file path.              */
			mFilePath.OutputFbxFilePath   = argv[i];
		}else if(i == 5){
		/*  Output blendshape file path.              */
			mFilePath.BlendShapePath      = argv[i];
		}/* 输出模型路径      */
		else if(i == 6){
			mFilePath.TextureFilePath     = argv[i];
		}else if(i == 7){
			mFilePath.UVFilePath          = argv[i];
		}else if(i == 8){
			mFilePath.OutputObjFilePath   = argv[i];
		}
	}

	/*      Loading Morphable 3D head model.      */
	Load3dfaceModel(const_cast<char*>(mFilePath.MorphableModelPath.c_str()));
	/*      Get the model data struct             */
	GetMorphableModel(mMorphableModel,BLENDSHAPE_NUM);
	/* Init the rand function by time             */
	std::srand((unsigned)time(NULL));                   

	OutputHeadModel = Reconstruction3DHead( mFilePath.InputUsrPath.c_str(),
											mFilePath.SkinFilePath.c_str(),
										    mMorphableModel);

	/* Save Texture                               */
	SaveTexture( mFilePath.TextureFilePath.c_str());
	/* Save BlendShape File.                      */
	SaveBlenShape(mFilePath.BlendShapePath.c_str());
	/* Save UVs and vertices data.                */
	SaveUVFile(mFilePath.UVFilePath.c_str(),OutputHeadModel);
	/* Save 3d head model with obj format.        */
	OutputObjModel(mFilePath.OutputObjFilePath.c_str(),OutputHeadModel);
	/* Save 3d head model with fbx format.        */
	ReWriteFbxUVs(OutputHeadModel,mFilePath.OutputFbxFilePath.c_str());
#endif 
    return 0;
}


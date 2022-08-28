# **三维人脸重建**

## 一、概述

基于单张照片的三位人头重建 ：软件运行效果

**软件图形界面：** winForm、CSharpGL(OpenGL的C#版本、负责模型的呈现和渲染)、利用DLL导入方式调用人脸重建算法

**人脸重建算法：** fbx模型文件解析、OpenCV人脸图像纹理处理、libfacedetection人脸检测、Eigen的人脸姿态求解、NNLS非负最小二乘拟合。

![](./4.Image/3DHeadReconstructon-Main.png)

**如何运行：** 安装VC++ 2012 运行库，或者安装Visual Studio 2012即可运行。

## 二、编译环境

1、编译环境Visual Studio 2012 ： 主要原因是opencv的dll库是vc110，理论上重新配置opencv的依赖适配其他版本的Visual Studio没有任何问题。

## 三、目录结构

## 四、论文

**CNKI：** https://kns.cnki.net/kcms/detail/detail.aspx?dbcode=CMFD&dbname=CMFD201902&filename=1019852370.nh&uniplatform=NZKPT&v=SSN-7J1dk7ehMb8SFxA5Fk10D2RLdtD47MIAKy5laORtWozmFPUE57_JD7JtyMTE

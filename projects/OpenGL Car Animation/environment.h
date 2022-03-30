//Amandeep Singh
//CS 47101 - Computer Graphics
//environment.h - contains all the code to create the scene including car, ground etc.

#ifndef ENVIRONMENT_H
#define ENVIRONMENT_H

#include "transformation.h"
#include "features.h"

//*********************************************************//
//***********************SceneObject Class*****************//
//*********************************************************//

class SceneObject
{
public:
	// Member Variables
	Transformation transformation;
	Material material;
	Mesh mesh;

	// Constructors
	SceneObject();
	SceneObject(const Mesh&);
	SceneObject(const SceneObject&);

	// Destructor
	~SceneObject() {};

	// Assignment
	SceneObject& operator=(SceneObject);
	void swap(SceneObject&);

	void glDraw();
};

//*********************************************************//
//***********************Axis Class************************//
//*********************************************************//

class Axis
{
public:
	Transformation transformation;

	Axis();

	void draw();

private:

	Color xAxisColor_;
	Color yAxisColor_;
	Color zAxisColor_;

	Transformation xAxisTransform_;
	Transformation yAxisTransform_;
	Transformation zAxisTransform_;
};


//*********************************************************//
//************************Car Class************************//
//*********************************************************//

class Car
{
public:
	Transformation transformation;

	Car();

	void draw();

	void makeSolid();
	void makeWireframe();

	void animateRunning();

	void triangularPrism();

	SceneObject leftHeadLight_;
	SceneObject rightHeadLight_;

private:
	SceneObject mainBody_;
	SceneObject hood_;
	SceneObject frontGlass;
	SceneObject backGlass;
	SceneObject trunk_;
	SceneObject frontLeftWheel_;
	SceneObject frontRightWheel_;
	SceneObject backLeftWheel_;
	SceneObject backRightWheel_;
	SceneObject leftBackLight_;
	SceneObject rightBackLight_;

	Transformation frontLeftWheelAxel_;
	Transformation frontRightWheelAxel_;
	Transformation backLeftWheelAxel_;
	Transformation backRightWheelAxel_;


	float frontLeftWheelVelocity_;
	float frontRightWheelVelocity_;
	float backLeftWheelVelocity_;
	float backRightWheelVelocity_;
	
	bool isSolid_;
};


#endif
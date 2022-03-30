//Amandeep Singh
//CS 47101 - Computer Graphics
//environment.cpp - contains all the code to create the scene including car, ground etc.

#include "environment.h"

//*********************************************************//
//***********************SceneObject Class*****************//
//*********************************************************//

SceneObject::SceneObject()
{
	transformation = Transformation();
	material = Material();
	mesh = standardTextureCube;
}

SceneObject::SceneObject(const Mesh& mesh_)
{
	transformation = Transformation();
	material = Material();
	mesh = mesh_;
}

SceneObject::SceneObject(const SceneObject& rhs)
{
	transformation = rhs.transformation;
	material = rhs.material;
	mesh = rhs.mesh;
}

SceneObject& SceneObject::operator=(SceneObject rhs)
{
	swap(rhs);
	return *this;
}

void SceneObject::swap(SceneObject& rhs)
{
	std::swap(transformation, rhs.transformation);
	std::swap(material, rhs.material);
	std::swap(mesh, rhs.mesh);
}

void SceneObject::glDraw()
{
	glPushMatrix();
	{
		transformation.transform();
		material.materialize();
		mesh();
	}
	glPopMatrix();
}

//*********************************************************//
//***********************Axis Class************************//
//*********************************************************//

Axis::Axis()
{
	transformation = Transformation();

	xAxisColor_ = Color::red();
	yAxisColor_ = Color::green();
	zAxisColor_ = Color::blue();
	

	xAxisTransform_ = Transformation(Vector3(0.5, 3, 0), Vector3(0, 0, 0), Vector3(1, 1, 1));
	yAxisTransform_ = Transformation(Vector3(0, 3.5, 0), Vector3(0, 0, 90), Vector3(1, 1, 1));
	zAxisTransform_ = Transformation(Vector3(0, 3, 0.5), Vector3(0, 90, 0), Vector3(1, 1, 1));
}

void Axis::draw()
{

	glPushMatrix();
	{
		transformation.transform();

		glPushMatrix();
		{
			xAxisTransform_.transform();
			xAxisColor_.glColor();
			standardLine();
		}
		glPopMatrix();

		glPushMatrix();
		{
			yAxisTransform_.transform();
			yAxisColor_.glColor();
			standardLine();
		}
		glPopMatrix();

		glPushMatrix();
		{
			zAxisTransform_.transform();
			zAxisColor_.glColor();
			standardLine();
		}
		glPopMatrix();
	}
	glPopMatrix();

}


//*********************************************************//
//***********************Car Class***********************//
//*********************************************************//

Car::Car()
{

	// Body Parts
	mainBody_ = SceneObject();
	mainBody_.transformation.position = Vector3(0, 1, .5);
	mainBody_.transformation.scale = Vector3(3, 1, 6);

	hood_ = SceneObject();
	hood_.transformation.position = Vector3(0, 2, 0);
	hood_.transformation.scale = Vector3(3, 1, 3);

	frontLeftWheel_ = SceneObject();
	frontLeftWheel_.transformation.position = Vector3(0, 0, 0);
	frontLeftWheel_.transformation.scale = Vector3(.5, 1, 1);

	frontRightWheel_ = SceneObject();
	frontRightWheel_.transformation.position = Vector3(0, 0, 0);
	frontRightWheel_.transformation.scale = Vector3(.5, 1, 1);

	backLeftWheel_ = SceneObject();
	backLeftWheel_.transformation.position = Vector3(0, 0, 0);
	backLeftWheel_.transformation.scale = Vector3(.5, 1, 1);

	backRightWheel_ = SceneObject();
	backRightWheel_.transformation.position = Vector3(0, 0, 0);
	backRightWheel_.transformation.scale = Vector3(.5, 1, 1);

	leftHeadLight_ = SceneObject();
	leftHeadLight_.transformation.position = Vector3(1, 1, 3.5);
	leftHeadLight_.transformation.scale = Vector3(.5, .5, .2);

	rightHeadLight_ = SceneObject();
	rightHeadLight_.transformation.position = Vector3(-1, 1, 3.5);
	rightHeadLight_.transformation.scale = Vector3(.5, .5, .2);

	rightBackLight_ = SceneObject();
	rightBackLight_.transformation.position = Vector3(1, 1, -2.5);
	rightBackLight_.transformation.scale = Vector3(.5, .5, .2);

	leftBackLight_ = SceneObject();
	leftBackLight_.transformation.position = Vector3(-1, 1, -2.5);
	leftBackLight_.transformation.scale = Vector3(.5, .5, .2);

	// Joints
	frontLeftWheelAxel_ = Transformation(Vector3(1.5, .5, 2.25), Vector3(0, 0, 0), Vector3(1, 1, 1));
	frontRightWheelAxel_= Transformation(Vector3(-1.5, .5, 2.25), Vector3(0, 0, 0), Vector3(1, 1, 1));
	backLeftWheelAxel_= Transformation(Vector3(1.5, .5, -1.25), Vector3(0, 0, 0), Vector3(1, 1, 1));
	backRightWheelAxel_= Transformation(Vector3(-1.5, .5, -1.25), Vector3(0,0, 0), Vector3(1, 1, 1));
	//leftHip_ = Transformation(Vector3(0.5, -2, 0), Vector3(0, 0, 0), Vector3(1, 1, 1));
	//rightHip_ = Transformation(Vector3(-0.5, -2, 0), Vector3(0, 0, 0), Vector3(1, 1, 1));
	//leftKnee_ = Transformation(Vector3(0, -2, 0), Vector3(0, 0, 0), Vector3(1, 1, 1));
	//rightKnee_ = Transformation(Vector3(0, -2, 0), Vector3(0, 0, 0), Vector3(1, 1, 1));

	isSolid_ = true;

	frontLeftWheelVelocity_  = 0;
	frontRightWheelVelocity_ = 0;
	backLeftWheelVelocity_ = 0;
	backRightWheelVelocity_ = 0;
	
	//leftHipVelocity_ = 0;
	//rightHipVelocity_ = 0;
	//leftKneeVelocity_ = 0;
	//rightKneeVelocity_ = 0;
}

void Car::triangularPrism()
{
	glBegin(GL_QUADS);
	glVertex3f(0.5, 0, 0.5);
	glVertex3f(0.5, 0, -0.5);
	glVertex3f(-0.5, 0, -0.5);
	glVertex3f(-0.5, 0, 0.5);

	glVertex3f(0.5, 0, -0.5);
	glVertex3f(0.5, 1, -0.5);
	glVertex3f(-0.5, 1, -0.5);
	glVertex3f(-0.5, 0, -0.5);

	glVertex3f(0.5, 1, -0.5);
	glVertex3f(-0.5, 1, -0.5);
	glVertex3f(-0.5, 0, 0.5);
	glVertex3f(0.5, 0, 0.5);
	glEnd();
	glBegin(GL_TRIANGLES);
	glVertex3f(0.5, 0, 0.5);
	glVertex3f(0.5, 1, -0.5);
	glVertex3f(0.5, 0, -0.5);

	glVertex3f(-0.5, 0, 0.5);
	glVertex3f(-0.5, 1, -0.5);
	glVertex3f(-0.5, 0, -0.5);
	glEnd();
}

void Car::draw()
{
	glEnable(GL_COLOR_MATERIAL);
	glPushMatrix();
	{
		transformation.transform();
		glPushMatrix();
		{
			GLfloat offset[] = { 0,0,0,1 };
			GLfloat offset2[] = { 1,0,0,1 };
			glColor4f(0, 1, 1, 0);
			mainBody_.glDraw();
			glColor4f(0, 0, 1, 0);
			hood_.glDraw();
			glColor4f(1, 1, 1, 0);
			leftHeadLight_.glDraw();
			glPushMatrix(); {
				glLightfv(GL_LIGHT0, GL_POSITION, offset);
				glPopMatrix();
			}
			rightHeadLight_.glDraw();
			glPushMatrix(); {
				glLightfv(GL_LIGHT0, GL_POSITION, offset2);
				glPopMatrix();
			}
			glColor4f(1, 0, 0, 0);
			leftBackLight_.glDraw();
			rightBackLight_.glDraw();

			// Front Left Wheel
			glPushMatrix();
			{
				glColor4f(0, 0, 0, 0);
				frontLeftWheelAxel_.transform();
				frontLeftWheel_.glDraw();
				glPopMatrix();

				// Back Left Wheel
				glPushMatrix();
				{
					backLeftWheelAxel_.transform();
					backLeftWheel_.glDraw();
					glPopMatrix();

					// Front Right Wheel
					glPushMatrix();
					{
						frontRightWheelAxel_.transform();
						frontRightWheel_.glDraw();
						glPopMatrix();

						// Back Right Wheel
						glPushMatrix();
						{
							backRightWheelAxel_.transform();
							backRightWheel_.glDraw();
							glPopMatrix();
						}
						glPopMatrix();
					}
					glPopMatrix();
				}
				glPopMatrix();
			}
			glPopMatrix();

		}
		glPopMatrix();
	}
	glPopMatrix();
	glDisable(GL_COLOR_MATERIAL);
}

void Car::animateRunning()
{
	const float standardVelocity = 0.4f;

	//glRotatef(0, 0, 0, 0);

	// Front Left Wheel Axel
	if (frontLeftWheelVelocity_ == 0)
		frontLeftWheelVelocity_ = standardVelocity;

	// Front Right Wheel Axel
	if (frontRightWheelVelocity_ == 0)
		frontRightWheelVelocity_ = standardVelocity;


	// Back Left Wheel Axel
	if (backLeftWheelVelocity_ == 0)
		backLeftWheelVelocity_ = standardVelocity;

	// Back Right Wheel Axel
	if (backRightWheelVelocity_ == 0)
		backRightWheelVelocity_ = standardVelocity;

	frontLeftWheelAxel_.rotation += Vector3(frontLeftWheelVelocity_, 0, 0);
	frontRightWheelAxel_.rotation += Vector3(frontRightWheelVelocity_, 0, 0);

	backRightWheelAxel_.rotation += Vector3(backRightWheelVelocity_, 0, 0);
	backLeftWheelAxel_.rotation += Vector3(backRightWheelVelocity_, 0, 0);

}

void Car::makeSolid()
{
	if (isSolid_ == false)
	{
		mainBody_.mesh = standardTextureCube;
		hood_.mesh = standardTextureCube;
		trunk_.mesh = standardTextureCube;
		frontLeftWheel_.mesh = standardTextureCube;
		frontRightWheel_.mesh = standardTextureCube;
		backLeftWheel_.mesh = standardTextureCube;
		backRightWheel_.mesh = standardTextureCube;
		leftHeadLight_.mesh = standardTextureCube;
		rightHeadLight_.mesh = standardTextureCube;
		leftBackLight_.mesh = standardTextureCube;
		rightBackLight_.mesh = standardTextureCube;
		isSolid_ = true;
	}
}

void Car::makeWireframe()
{
	if (isSolid_ == true)
	{
		mainBody_.mesh = standardWireCube;;
		hood_.mesh = standardWireCube;;
		trunk_.mesh = standardWireCube;;
		frontLeftWheel_.mesh = standardWireCube;;
		frontRightWheel_.mesh = standardWireCube;;
		backLeftWheel_.mesh = standardWireCube;;
		backRightWheel_.mesh = standardWireCube;;
		leftHeadLight_.mesh = standardWireCube;;
		rightHeadLight_.mesh = standardWireCube;;
		leftBackLight_.mesh = standardWireCube;;
		rightBackLight_.mesh = standardWireCube;;

		isSolid_ = false;
	}
}
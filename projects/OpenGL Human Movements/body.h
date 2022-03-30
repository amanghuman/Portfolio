//Amandeep Singh
//CS 47101 - Computer Graphics
//Assignment 2 - Making a Robot with movable limbs, and creating a Camera to move through the scene
//body.h - contains the core code of making a robot

#ifndef BODY_H
#define BODY_H

#include <freeglut.h>
#include <stdio.h>

void solidBox(GLdouble width, GLdouble height, GLdouble depth);

namespace robot {
	class body {
	private:
		const int _incrementAngle = 5;
		int _currentAngle = 0;

		GLfloat _postion[3] = { 0.0, 0.0, 0.0 };
		GLfloat _color[3] = { 0.0, 0.0, 1.0 };
		GLfloat _size[3] = { 2.0, 0.4, 1.0 };
		GLfloat _rotationPoint[3] = { 1.0, 0.0, 0.0 };

	public:
		void draw();
		void setAngle(int newAngle);
		void incrementAngle();
		void decrementAngle();
		void setPosition(GLfloat x, GLfloat y, GLfloat z);
		void setRotationPoint(GLfloat x, GLfloat y, GLfloat z);
	};
}

void robot::body::draw()
{
	glTranslatef(_postion[0], _postion[1], _postion[2]);
	glRotatef((GLfloat)_currentAngle, 0.0, 0.0, 1.0);
	glTranslatef(_rotationPoint[0], _rotationPoint[1], _rotationPoint[2]);
	solidBox(_size[0], _size[1], _size[2]);
}

inline void robot::body::setAngle(int newAngle)
{
	_currentAngle = newAngle;
}

inline void robot::body::incrementAngle()
{
	(_currentAngle += _incrementAngle) %= 360;
}

inline void robot::body::decrementAngle()
{
	(_currentAngle -= _incrementAngle) %= 360;
}

inline void robot::body::setPosition(GLfloat x, GLfloat y, GLfloat z)
{
	_postion[0] = x; _postion[1] = y; _postion[2] = z;
}

inline void robot::body::setRotationPoint(GLfloat x, GLfloat y, GLfloat z)
{
	_rotationPoint[0] = x; _rotationPoint[1] = y; _rotationPoint[2] = z;
}

#endif

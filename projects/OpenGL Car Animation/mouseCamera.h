//Amandeep Singh
//CS 47101 - Computer Graphics
//Assignment 2 - Making a Robot with movable limbs, and creating a Camera to move through the scene
//mouseCamera.h - contains core code of using camera through mouse input

#ifndef CAMERA_H
#define CAMERA_H

#include <freeglut.h>
#include <stdio.h>
#include <math.h>
#define PI 3.141592

int mouseX = 0, mouseY = 0;					//last known X and Y values of the mouse
float cameraTheta, cameraPhi, cameraRadius; //camera position in spherical coordinates
float x, y, z;								//camera position in cartesian coordinates
float lx = 0.0f, ly = 0.0f, lz = -1.0f;		//camera direction vectors
float angle = 0.0f;
GLint leftMouseButton, rightMouseButton;	//to record mouse button states
void calculateOrientation()
{
	x = cameraRadius * sinf(cameraTheta) * sinf(cameraPhi);
	z = cameraRadius * cosf(cameraTheta) * sinf(cameraPhi);
	y = cameraRadius * cosf(cameraPhi);

	glutPostRedisplay();
}

void mouseCallback(int button, int state, int thisX, int thisY)
{
	//if needed, update the left and right mouse button state
	if (button == GLUT_LEFT_BUTTON)
		leftMouseButton = state;
	else if (button == GLUT_RIGHT_BUTTON)
		rightMouseButton = state;
	//update X and Y mouse values
	mouseX = thisX;
	mouseY = thisY;
}


void mouseMotion(int x, int y)
{
	if (leftMouseButton == GLUT_DOWN)
	{
		cameraTheta += (mouseX - x) * 0.005;
		cameraPhi += (mouseY - y) * 0.005;
		//phi should be always between zero and PI, i.e., 0 < phi < PI
		if (cameraPhi <= 0)
			cameraPhi = 0 + 0.001;
		if (cameraPhi >= PI)
			cameraPhi = PI - 0.001;

		//update camera position
		calculateOrientation();
	} 
	// camera zoom in/out
	else if (rightMouseButton == GLUT_DOWN) {
		double totalChangeSq = (x - mouseX) + (y - mouseY);
		cameraRadius += totalChangeSq * 0.01;

		//limit the camera radius 
		if (cameraRadius < 2.0)
			cameraRadius = 2.0;
		if (cameraRadius > 300.0)
			cameraRadius = 300.0;

		//update camera position
		calculateOrientation(); 
	}
	mouseX = x;
	mouseY = y;
}

void mouseWheel(int button, int dir, int x, int y)
{
	if (dir > 0)
	{
		cameraRadius--;
		if (cameraRadius < 2.0) cameraRadius = 2.0;
	}
	else
	{
		cameraRadius++;
		if (cameraRadius > 300.0) cameraRadius = 300.0;
	}

	//update camera position
	calculateOrientation();
}

#endif
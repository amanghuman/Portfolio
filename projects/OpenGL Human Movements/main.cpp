//Amandeep Singh
//CS 47101 - Computer Graphics
//Assignment 2 - Making a Robot with movable limbs, and creating a Camera to move through the scene
//main.cpp - contains all the working of scene

#include <stdio.h>
#include <iostream>
#include <glew.h>
#include <freeglut.h>
#include "mouseCamera.h"
#include "body.h";

using std::cout;
using std::cin;
using std::endl;

//This program is from the OpenGL Programming Guide. It shows a robot.
//This program is a modified version of the original source codes https://cs.lmu.edu/~ray/notes/openglexamples/
//This program runs well under the settings you have done for the Assignment1. Please go
//back to settings if you have compile or link errors for this program.



#define BODY_WIDTH 2
#define BODY_HEIGHT 4
#define BODY_DEPTH 1

//The robot arm is specified by (1) the angle that the upper arm makes
//relative to the x-axis, called shoulderAngle, and (2) the angle that the
//lower arm makes relative to the upper arm, called elbowAngle. These angles
//are adjusted in 5 degree increments by a keyboard callback.

//Create robot bodyparts
//Each bodypart has its own unique modifiable position and rotation

static robot::body
leftForearm,
rightForearm,
leftUpperarm,
rightUpperarm,
leftUpperleg,
rightUpperleg,
leftLowerleg,
rightLowerleg;

static bool wireFrame = false;
static bool showAxis = true;

bool asAssigned = true;

//Handles the keyboard event for special keys
void special(int key, int, int) {
	float fraction = 0.1f;
	switch (key) {
	case GLUT_KEY_LEFT:  angle -= 0.01f; lx = sin(angle); lz = -cos(angle);  break; //rotates camera to left
	case GLUT_KEY_RIGHT: angle += 0.01f; lx = sin(angle); lz = -cos(angle);  break; //rotates camera to right
	case GLUT_KEY_UP:    x += lx * fraction; z += lz * fraction; break;				//move camera forward
	case GLUT_KEY_DOWN:  x -= lx * fraction; z -= lz * fraction; break;				//move camera backward
	
	case GLUT_KEY_F4:	 leftForearm.decrementAngle();  rightForearm.incrementAngle();  break;	//controls the fore arm decrements of both arms
	case GLUT_KEY_F5:	 leftUpperarm.decrementAngle(); rightUpperarm.incrementAngle(); break;  //controls the upper arm decrements of both arms
	case GLUT_KEY_F6:	 leftUpperleg.decrementAngle(); rightUpperleg.decrementAngle(); break;	//controls the upper leg decrements of both legs
	case GLUT_KEY_F7:	 leftLowerleg.decrementAngle(); rightLowerleg.decrementAngle(); break;	//controls the lower leg decrements of both legs

	default: return;
	}
	glutPostRedisplay();
}

//wireBox(w, h, d) makes a wireFrame box with width w, height h and
//depth d centered at the origin. It uses the GLUT wire cube function.
void solidBox(GLdouble width, GLdouble height, GLdouble depth) {
	glPushMatrix();
	glColor3f(0.0, 0.0, 1.0);
	glScalef(width, height, depth);
	glutSolidCube(1.0);
	glPopMatrix();
}

void drawAxes()
{
	//Draw a red x-axis, a green y-axis, and a blue z-axis.
	glBegin(GL_LINES);
	glColor3f(1, 0, 0); glVertex3f(0, 6, 0); glVertex3f(5, 6, 0);
	glColor3f(0, 1, 0); glVertex3f(0, 6, 0); glVertex3f(0, 11, 0);
	glColor3f(0, 0, 1); glVertex3f(0, 6, 0); glVertex3f(0, 6, 5);
	glEnd();
}
void drawRobot()
{
	//Draw the torso at the orgin
	solidBox(BODY_WIDTH, BODY_HEIGHT, BODY_DEPTH);

	//Draw head right on the top of torso
	glPushMatrix();

	glTranslatef(0.0,3.0, 0.0);
	glutSolidSphere(1.0, 50, 50);

	glPopMatrix();
	
	//Draw right arm at the end of upper right torso

	glPushMatrix();

	rightUpperarm.setPosition(1.0, 1.5, 0.0);
	rightUpperarm.draw();
	
	//Both the forearm, and upperarm share the same rotation matrix
	//Draw right fore arm right at the outer side of upper arm

	rightForearm.setPosition(1.0, 0.0, 0.0);
	rightForearm.draw();


	glPopMatrix();

	//Draw left upper arm at the end of upper left torso
	glPushMatrix();

	leftUpperarm.setPosition(-1.0, 1.5, 0.0);
	leftUpperarm.setRotationPoint(-1.0, 0.0, 0.0);
	leftUpperarm.draw();

	//Both the forearm, and upperarm share the same rotation matrix
	//Draw left fore arm right at the outer side of upper arm

	leftForearm.setPosition(-1.0, 0.0, 0.0);
	leftForearm.draw();


	glPopMatrix();

	//Drawing legs work same as drawing arms

	glPushMatrix();
	glTranslatef(0.80, -2.0, 0.0);
	glRotatef(90, -1.0, 0.0, 0.0);
	glPushMatrix();
	glRotatef(90, 0.0, 1.0, 0.0);
	glPushMatrix();

	//Draw right upper leg at the at the end of lower right torso

	rightUpperleg.setPosition(0.0, 0.0, 0.0);
	rightUpperleg.draw();

	//Both the upper, and lower leg share the same rotation matrix
	//Draw right lower leg at the bottom of right upper leg

	rightLowerleg.setPosition(1.0, 0.0, 0.0);
	rightLowerleg.draw();
	glPopMatrix();
	glPopMatrix();
	glPopMatrix();

	//Draw left leg at the at the end of lower left torso

	glPushMatrix();
	glTranslatef(-0.80, -2.0, 0.0);
	glRotatef(90, -1.0, 0.0, 0.0);
	glPushMatrix();
	glRotatef(90, 0.0, 1.0, 0.0);
	glPushMatrix();
	leftUpperleg.setPosition(0.0, 0.0, 0.0);
	leftUpperleg.draw();

	//Both the upper, and lower leg share the same rotation matrix
	//Draw left lower leg at the bottom of left upper leg

	leftLowerleg.setPosition(1.0, 0.0, 0.0);
	leftLowerleg.draw();
	glPopMatrix();
	glPopMatrix();
	glPopMatrix();

	glPopMatrix();
}

void display() {
	glClear(GL_COLOR_BUFFER_BIT);
	glMatrixMode(GL_MODELVIEW);

	//Render model as a wireframe
	if (wireFrame) glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
	else           glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);

	glPushMatrix();

	if (asAssigned) {
		gluLookAt(x, y, z, //camera is located at (x,y,z)
			x + lx, 1.0, z + lz, //camera is looking at this position, modified according to user input
			0.0f, 1.0f, 0.0f); //up vector is (0,1,0) (positive Y)
	}
	else if (!asAssigned) {
		gluLookAt(x, y, z, //camera is located at (x,y,z)
			0, 0, 0, //camera is looking at (0,0,0)
			0.0f, 1.0f, 0.0f); //up vector is (0,1,0) (positive Y)
	}

	//Generating Platform
	glColor3f(0.75f, 0.75f, 0.75f);
	glBegin(GL_QUADS);
	glVertex3f(-150.0f, -6.0f, -150.0f);
	glVertex3f(-100.0f, -6.0f,  150.0f);
	glVertex3f( 150.0f, -6.0f,  150.0f);
	glVertex3f( 150.0f, -6.0f, -150.0f);
	glEnd();

	//Creating an army of Robot Puppets
	if (showAxis) drawAxes(); //draw axes
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			glPushMatrix();
			glTranslatef(i * 15, 0.0, j * 15);
			drawRobot(); //draw a robot
		}
	}
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			glPushMatrix();
			glTranslatef(i * -15, 0.0, j * 15);
			drawRobot(); //draw a robot
		}
	}
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			glPushMatrix();
			glTranslatef(i * 15, 0.0, j * -15);
			drawRobot(); //draw a robot
		}
	}
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			glPushMatrix();
			glTranslatef(i * -15, 0.0, j * -15);
			drawRobot(); //draw a robot
		}
	}

	//Popping out to the main matrix
	for (int i = 0; i < 64; i++) {
		glPopMatrix();
	}

	glPopMatrix();
	
	glFlush();
	glutSwapBuffers();
}

//Handling the reshape when zoomed in or out
void reshape(GLint w, GLint h) {
	if (h == 0) h = 1;
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(65.0, GLfloat(w) / GLfloat(h), 1.0, 200.0);
}

//Handling keyboard events for normal keys
void procKeys(unsigned char key, int x, int y)
{
	switch (key) {
	case '1':  wireFrame = true; break;													//turn all the models into wireframe mode
	case '2':  wireFrame = false; break;												//turn all the models into solid mode
	case '3':  showAxis = !showAxis; break;												//toggle direction axes on/off
	case '4':  leftForearm.incrementAngle();  rightForearm.decrementAngle();  break;	//controls the fore arm increments of both arms
	case '5':  leftUpperarm.incrementAngle(); rightUpperarm.decrementAngle(); break;	//controls the upper arm increments of both arms
	case '6':  leftUpperleg.incrementAngle(); rightUpperleg.incrementAngle(); break;	//controls the upper leg increments of both legs
	case '7':  leftLowerleg.incrementAngle(); rightLowerleg.incrementAngle(); break;	//controls the lower leg increments of both legs
	case  27:  exit(0); break;															//exit the window/program
	}
	glutPostRedisplay();
}
//Perfroms application specific initialization: turn off smooth shading,
//sets the viewing transformation once and for all.
void init() {
	glShadeModel(GL_FLAT);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	leftForearm.setAngle(180);
}
//Initializes GLUT, the display mode, and main window; registers callbacks;
//does application initialization; enters the main event loop.
int main(int argc, char** argv) {

	//Ask user to choose between camera modes
	cout << "Choose camera mode:\n\n For as requested in assignment press(1) \n For other press(2) " << endl;

	char input;

	cout << ">";
	cin >> input;

	//use as required in assignment camera 
	if (input == '1') {
		asAssigned = true;
	}

	//use camera with mouse input
	else if (input == '2') {
		asAssigned = false;
	}

	//This camera mode is assigned in the assignment.
	if (asAssigned) {
	printf("\n\
*************************************************************************\n\
* User Manual for the robot:                                            *\n\
* '1' : Show wireFrame                                                  *\n\
* '2' : Show solid                                                      *\n\
* '3' : Toggle axes on/off                                              *\n\
* '4' : Increment the elbowAngle                                        *\n\
* 'F4': Decrement the elbowAngle                                        *\n\
* '5' : Increment shoulderAngle                                         *\n\
* 'F5': Decrement shoulderAngle                                         *\n\
* '6' : Increase the upper leg angle                                    *\n\
* 'F6': Decrease the upper leg angle                                    *\n\
* '7' : Increase the lower leg angle                                    *\n\
* 'F7': Increase the lower leg angle                                    *\n\
*  Arrow key 'Up'   : Camera moving Forward                             *\n\
*  Arrow key 'Down' : Camera moving Back                                *\n\
*  Arrow key 'Left' : Camera rotating to the Left                       *\n\
*  Arrow key 'Right': Camera rotating to the Right                      *\n\
*  ESC: Quit                                                            *\n\
*************************************************************************\n");
	}
	//This camera mode is personally implemented by me. This camera mode is basic camera mode which uses mouse input. This is similar to
	//camera use in the main unity engine game designing window. You can zoom and rotate camera using mouse input.
	else if (!asAssigned) {
		printf("\n\
*************************************************************************\n\
* User Manual for the robot:                                            *\n\
* 'Left Mouse Button and Drag' : Rotate                                 *\n\
* 'Right Mouse Button and Drag': Zoom in/out                            *\n\
* 'Middle Mouse Wheel'         : Also Zoom in/out                       *\n\
* '1' : Show wireFrame                                                  *\n\
* '2' : Show solid                                                      *\n\
* '3' : Toggle axes on/off                                              *\n\
* '4' : Increment the elbowAngle                                        *\n\
* 'F4': Decrement the elbowAngle                                        *\n\
* '5' : Increment shoulderAngle                                         *\n\
* 'F5': Decrement shoulderAngle                                         *\n\
* '6' : Increase the upper leg angle                                    *\n\
* 'F6': Decrease the upper leg angle                                    *\n\
* '7' : Increase the lower leg angle                                    *\n\
* 'F7': Increase the lower leg angle                                    *\n\
*  ESC: Quit                                                            *\n\
*************************************************************************\n");
	}
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowPosition(80, 80);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Amandeep Singh - Assignment 2");

	//These are required variables for assigned camera requirements to work
	//x, y, z is the position of camera
	if (asAssigned) {
		x = 0.0;
	    y = 1.0;
	    z = 15.0;
	}

	//These variables are required for camera to work with mouse input
	//No x, y, z are needed as they are calculated in mouseCamera header file
	else if (!asAssigned) {
		cameraRadius = 7.0f;
		cameraTheta = 2.80;
		cameraPhi = 2.0;
		calculateOrientation();
	}
	

	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	glutIdleFunc(display);
	glutKeyboardFunc(procKeys);
	glutSpecialFunc(special);

	if (!asAssigned) {
		glutMouseFunc(mouseCallback);
		glutMotionFunc(mouseMotion);
		glutMouseWheelFunc(mouseWheel);
	}
	init();
	glutMainLoop();
}
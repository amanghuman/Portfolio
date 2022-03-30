//Amandeep Singh
//CS 47101 - Computer Graphics
//transformation.h - contains all code for all display tranformations and physics working

#ifndef TRANSFORMATION_H
#define TRANSFORMATION_H

#include <glut.h>
#include <iostream>
#include <math.h>

//*********************************************************//
//*********************Vector3 Class**********************//
//*********************************************************//

class Vector3
{
public:
	// Member Variables
	float x, y, z;

	// Constructors
	Vector3();
	Vector3(float, float, float);
	Vector3(const Vector3&);

	// Destructor
	~Vector3() {};

	// Basic Vector | Vector Math
	Vector3 operator+(const Vector3&) const;
	Vector3 operator-(const Vector3&) const;
	Vector3& operator-=(const Vector3&);
	Vector3& operator+=(const Vector3&);

	// Basic Vector | Scalar Math
	Vector3 operator*(float) const;
	Vector3 operator/(float) const;
	Vector3& operator*=(float);
	Vector3& operator/=(float);

	// Vector Operations
	float magnitude() const;
	float squareMagnitude() const;
	void normalize();
	Vector3 cross(const Vector3&) const;
	float dot(const Vector3&) const;

	// Assignment
	Vector3& operator=(Vector3);
	void swap(Vector3&);

	// Comparison
	bool operator==(const Vector3&) const;
	bool operator!=(const Vector3&) const;

	// IO
	friend std::ostream& operator<<(std::ostream&, const Vector3&);
	//friend std::istream& operator>>(std::istream&, Vector3&);

	// create Vertex in openGL space
	void glVertex() const;
};

Vector3 operator*(float, const Vector3&);
Vector3 operator/(float, const Vector3&);

//*********************************************************//
//******************Transformation Class*******************//
//*********************************************************//

class Transformation
{
public:
	// Member Variables
	Vector3 position, rotation, scale;

	// Constructors
	Transformation();
	Transformation(const Vector3&, const Vector3&, const Vector3&);
	Transformation(const Transformation&);

	// Destructor
	~Transformation() {};

	// Assignement
	Transformation& operator=(Transformation);
	void swap(Transformation&);

	// Comparison
	bool operator==(const Transformation&) const;
	bool operator!=(const Transformation&) const;

	// IO
	//friend std::ostream& operator<<(std::ostream&, const Transformation&);
	//friend std::istream& operator>>(std::istream&, Transformation&);

	// Perform Transformation
	void transform();
};

class Vector3f
{
public:
	Vector3f();
	Vector3f(float, float, float);

	void swap(Vector3f&);
	Vector3f& operator=(Vector3f);

	Vector3f	 operator+	(const Vector3f&) const;
	Vector3f& operator+=	(const Vector3f&);

	bool operator==	(const Vector3f&) const;

	friend std::ostream& operator<<(std::ostream&, const Vector3f&);

	// Shifts the x, y, and z values to be within the specified range
	void restrict(float, float);

	void setX(float);
	void setY(float);
	void setZ(float);

	float getX() const;
	float getY() const;
	float getZ() const;

private:
	float x_;
	float y_;
	float z_;
};

class Transformationf
{
public:
	Transformationf();
	Transformationf(const Vector3f&, const Vector3f&, const Vector3f&);

	void setTranslation(const Vector3f&);
	void setRotation(const Vector3f&);
	void setScale(const Vector3f&);

	Vector3f getTranslation() const;
	Vector3f getRotation()	 const;
	Vector3f getScale()		 const;

	void translate(const Vector3f&);
	void rotate(const Vector3f&);
	void scale(const Vector3f&);

	void transform() const;

private:
	Vector3f translation_, rotation_, scale_;
};

#endif
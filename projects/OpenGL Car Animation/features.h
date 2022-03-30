//Amandeep Singh
//CS 47101 - Computer Graphics
//features.h - contains all the code defining features of car, ground etc.

#ifndef FEATURES_H
#define FEATURES_H
#include <glut.h>
#include <functional>
#include <iostream>
#include "transformation.h"

//*********************************************************//
//***********************Color Class***********************//
//*********************************************************//

class Color
{
public:
	// Constructors
	Color();
	Color(int, int, int, int);
	Color(const Color&);

	// Destructor
	~Color() {};

	// Accessors
	int getRed() const;
	int getGreen() const;
	int getBlue() const;
	int getAphla() const;

	// Mutators
	void setRed(int);
	void setGreen(int);
	void setBlue(int);
	void setAlpha(int);

	// Assignment
	Color& operator=(Color);
	void swap(Color&);

	// Comparison
	bool operator==(const Color&) const;
	bool operator!=(const Color&) const;

	// Output
	friend std::ostream& operator<<(std::ostream&, const Color&);

	// Prebuilt Colors
	static Color red();
	static Color orange();
	static Color yellow();
	static Color green();
	static Color blue();
	static Color indego();
	static Color violet();
	static Color white();
	static Color black();
	static Color brown();

	void glColor() const;

private:
	int red_, green_, blue_;
	int alpha_;
};

//*********************************************************//
//***********************Mesh Class************************//
//*********************************************************//

typedef std::function<void(void)> Mesh;

// Sphere
static const Mesh standardSphere = []() { glutSolidSphere(0.5, 20, 20); };
static const Mesh standardWireSphere = []() { glutWireSphere(0.5, 20, 20); };

// Cube
static const Mesh standardCube = []() { glutSolidCube(1); };
static const Mesh standardWireCube = []() { glutWireCube(1); };
static const Mesh standardTextureCube = []()
{
	// Top Face
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, 0.5, -0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, 0.5, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, 0.5, 0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, 0.5, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();

	// Bottom Face
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, -0.5, -0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, -0.5, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, -0.5, 0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, -0.5, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();

	// Left Face
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, -0.5, 0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(-0.5, -0.5, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(-0.5, 0.5, -0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, 0.5, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();

	// Right Face
	glBegin(GL_QUADS);
	{
		Vector3(0.5, -0.5, 0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, -0.5, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, 0.5, -0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(0.5, 0.5, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();

	// Front Face
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, -0.5, 0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, -0.5, 0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, 0.5, 0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, 0.5, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();

	// Back Face
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, -0.5, -0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, -0.5, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, 0.5, -0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, 0.5, -0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();
};

// Cone
static const Mesh standardCone = []() { glutSolidCone(0.5, 1, 20, 20); };
static const Mesh standardWireCone = []() { glutWireCone(0.5, 1, 20, 20); };

// Torus
static const Mesh standardTorus = []() { glutSolidTorus(0.25, 0.5, 20, 20); };
static const Mesh standardWireTorus = []() { glutWireTorus(0.25, 0.5, 20, 20); };

// Dodecahedron
static const Mesh standardDodecahedron = []() { glutSolidDodecahedron(); };
static const Mesh standardWireDodecahedron = []() { glutWireDodecahedron(); };

// Octahedron
static const Mesh standardOctahedron = []() { glutSolidOctahedron(); };
static const Mesh standardWireOctahedron = []() { glutWireOctahedron(); };

// Tetrahedron
static const Mesh standardTetrahedron = []() {glutSolidTetrahedron(); };
static const Mesh standardWireTetrahedron = []() {glutWireTetrahedron(); };

// Icosahedron
static const Mesh standardIcosahedron = []() {glutSolidIcosahedron(); };
static const Mesh standardWireIcosahedron = []() {glutWireIcosahedron(); };

// Teapot
static const Mesh standardTeapot = []() {glutSolidTeapot(1); };
static const Mesh standardWireTeapot = []() {glutWireTeapot(1); };

// Line
static const Mesh standardLine = []()
{
	glBegin(GL_LINES);
	{
		Vector3(-0.5, 0, 0).glVertex();
		Vector3(0.5, 0, 0).glVertex();
	}
	glEnd();
};

// Plane
static const Mesh standardTexturePlane = []()
{
	glBegin(GL_QUADS);
	{
		Vector3(-0.5, 0, -0.5).glVertex(); glTexCoord2f(0, 0);
		Vector3(0.5, 0, -0.5).glVertex(); glTexCoord2f(1, 0);
		Vector3(0.5, 0, 0.5).glVertex(); glTexCoord2f(1, 1);
		Vector3(-0.5, 0, 0.5).glVertex(); glTexCoord2f(0, 1);
	}
	glEnd();
};

//*********************************************************//
//*********************Material Class**********************//
//*********************************************************//

class Material
{
public:
	// Member Variable
	Color color;

	// Constructors
	Material();
	Material(const Color&);
	Material(const Material&);

	// Destructor
	~Material() {};

	void materialize();
};
class Colorf
{
public:
	Colorf() : red_(0), green_(0), blue_(0) {};
	Colorf(int, int, int);

	void colorf();

private:
	int red_, green_, blue_;
};

class Red : public Colorf
{
public:
	Red() : Colorf(255, 0, 0) {};
};

class Green : public Colorf
{
public:
	Green() : Colorf(0, 255, 0) {};
};

class Blue : public Colorf
{
public:
	Blue() : Colorf(0, 0, 255) {};
};

class White : public Colorf
{
public:
	White() : Colorf(255, 255, 255) {};
};

class Black : public Colorf
{
public:
	Black() : Colorf(0, 0, 0) {};
};

class Orange : public Colorf
{
public:
	Orange() : Colorf(255, 128, 0) {};
};

class Yellow : public Colorf
{
public:
	Yellow() : Colorf(255, 255, 0) {};
};

class Purple : public Colorf
{
public:
	Purple() : Colorf(128, 0, 255) {};
};

class Meshf
{
public:
	virtual void drawMeshf() = 0;
};

class CubeMeshf : public Meshf
{
public:
	CubeMeshf() : size_(1) {};
	CubeMeshf(double s) : size_(s) {};

	virtual void drawMeshf() override;

protected:
	double size_;
};

class WireCubeMeshf : public CubeMeshf
{
public:
	WireCubeMeshf() : CubeMeshf() {};
	WireCubeMeshf(double s) : CubeMeshf(s) {};

	void drawMeshf() final;
};

class SphereMeshf : public Meshf
{
public:
	SphereMeshf() : radius_(0.5), slices_(20), stacks_(20) {};
	SphereMeshf(double r, int s1, int s2) : radius_(r), slices_(s1), stacks_(s2) {};

	virtual void drawMeshf() override;

protected:
	double radius_;
	int slices_;
	int stacks_;
};

class WireSphereMeshf : public SphereMeshf
{
public:
	WireSphereMeshf() : SphereMeshf() {};
	WireSphereMeshf(double r, int s1, int s2) : SphereMeshf(r, s1, s2) {};

	void drawMeshf() final;
};

class ConeMeshf : public Meshf
{
public:
	ConeMeshf() : radius_(0.5), height_(1), slices_(20), stacks_(20) {};
	ConeMeshf(double r, double h, int s1, int s2) : radius_(r), height_(h), slices_(s1), stacks_(s2) {};

	virtual void drawMeshf() override;

protected:
	double radius_;
	double height_;
	int slices_;
	int stacks_;
};

class WireConeMeshf : public ConeMeshf
{
public:
	WireConeMeshf() : ConeMeshf() {};
	WireConeMeshf(double r, double h, int s1, int s2) : ConeMeshf(r, h, s1, s2) {};

	void drawMeshf() final;
};

class LineMeshf : public Meshf
{
public:
	LineMeshf() {};

	void drawMeshf() final;
};

class PlaneMeshf : public Meshf
{
public:
	PlaneMeshf() {};

	void drawMeshf() override;
};

class WirePlaneMeshf : public PlaneMeshf
{
public:
	WirePlaneMeshf() : PlaneMeshf() {};

	void drawMeshf() final;
};

class Shape
{
public:
	Shape();
	Shape(const Vector3f&, const Vector3f&, const Vector3f&, Colorf*, Meshf*);

	void setTranslation(const Vector3f&);
	void setRotation(const Vector3f&);
	void setScale(const Vector3f&);

	Vector3f getTranslation() const;
	Vector3f getRotation()	 const;
	Vector3f getScale()		 const;

	void translate(const Vector3f&);
	void rotate(const Vector3f&);
	void scale(const Vector3f&);

	// Make sure parameter is not used elsewhere in program
	void changeColorf(Colorf*);

	virtual void draw();

protected:
	Meshf* meshf_;
	Transformationf transformationf_;
	Colorf* colorf_;
};

// A shape that can be drawn as a solid or wire meshf
class Toggleshfape : public Shape
{
public:
	Toggleshfape();
	Toggleshfape(const Vector3f&, const Vector3f&, const Vector3f&, Colorf*, Meshf*, Meshf*);

	void draw() final;
	void makeSolid();
	void makeWireframe();

private:
	bool isSolid_;
	Meshf* wiremeshf_;
};

#endif
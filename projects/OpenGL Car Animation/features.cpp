//Amandeep Singhrob
//CS 47101 - Computer Graphics
//features.cpp - contains all the code defining features of car, ground etc.

#include "features.h"

//*********************************************************//
//***********************Color Class***********************//
//*********************************************************//

Color::Color()
{
	red_ = 255;
	green_ = 255;
	blue_ = 255;
	alpha_ = 255;
}

Color::Color(int r, int g, int b, int a)
{
	red_ = r % 256;
	green_ = g % 256;
	blue_ = b % 256;
	alpha_ = a % 256;
}

Color::Color(const Color& rhs)
{
	red_ = rhs.red_;
	green_ = rhs.green_;
	blue_ = rhs.blue_;
	alpha_ = rhs.alpha_;
}

int Color::getRed() const { return red_; }
int Color::getGreen() const { return green_; }
int Color::getBlue() const { return blue_; }
int Color::getAphla() const { return alpha_; }

void Color::setRed(int r) { red_ = r % 256; }
void Color::setGreen(int g) { green_ = g % 256; }
void Color::setBlue(int b) { blue_ = b % 256; }
void Color::setAlpha(int a) { alpha_ = a % 256; }

Color& Color::operator=(Color rhs)
{
	swap(rhs);
	return *this;
}

void Color::swap(Color& rhs)
{
	std::swap(red_, rhs.red_);
	std::swap(green_, rhs.green_);
	std::swap(blue_, rhs.blue_);
	std::swap(alpha_, rhs.alpha_);
}

bool Color::operator==(const Color& rhs) const
{
	bool rEqual = (red_ == rhs.red_);
	bool gEqual = (green_ == rhs.green_);
	bool bEqual = (blue_ == rhs.blue_);
	bool aEqual = (alpha_ == rhs.alpha_);

	return rEqual && gEqual && bEqual && aEqual;
}

bool Color::operator!=(const Color& rhs) const { return !(*this == rhs); }

std::ostream& operator<<(std::ostream& out, const Color& rhs)
{
	out << "<" << rhs.red_ << ", " << rhs.green_ << ", " << rhs.blue_ << ", " << rhs.alpha_ << ">";
	return out;
}

Color Color::red() { return Color(255, 0, 0, 255); }
Color Color::orange() { return Color(255, 128, 0, 255); }
Color Color::yellow() { return Color(255, 255, 0, 255); }
Color Color::green() { return Color(0, 255, 0, 255); }
Color Color::blue() { return Color(0, 0, 255, 255); }
Color Color::indego() { return Color(64, 0, 255, 255); }
Color Color::violet() { return Color(128, 0, 255, 255); }
Color Color::white() { return Color(255, 255, 255, 255); }
Color Color::black() { return Color(0, 0, 0, 255); }
Color Color::brown() { return Color(150, 75, 0, 255); }

void Color::glColor() const
{
	float r = red_ / 255.0f;
	float g = green_ / 255.0f;
	float b = blue_ / 255.0f;
	float a = alpha_ / 255.0f;

	glColor4f(r, g, b, a);
}

//*********************************************************//
//*********************Material Class**********************//
//*********************************************************//

Material::Material()
{
	color = Color();
}

Material::Material(const Color& c)
{
	color = c;
}

Material::Material(const Material& rhs)
{
	color = rhs.color;
}

void Material::materialize()
{
	float r = (float)color.getRed() / 255.0f;
	float g = (float)color.getGreen() / 255.0f;
	float b = (float)color.getBlue() / 255.0f;

	float rgb[4] = { r, g, b, 1 };

	glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT_AND_DIFFUSE, rgb);
}

//*********************************************************//
//*********************Shape Class*************************//
//*********************************************************//

Shape::Shape()
{
	transformationf_ = Transformationf();
	colorf_ = new White();
	meshf_ = new CubeMeshf();
}

Shape::Shape(const Vector3f& t, const Vector3f& r, const Vector3f& s, Colorf* c, Meshf* m)
{
	transformationf_ = Transformationf(t, r, s);
	colorf_ = c;
	meshf_ = m;
}

void Shape::setTranslation(const Vector3f& t) { transformationf_.setTranslation(t); }
void Shape::setRotation(const Vector3f& r) { transformationf_.setRotation(r); }
void Shape::setScale(const Vector3f& s) { transformationf_.setScale(s); }

Vector3f Shape::getTranslation() const { return transformationf_.getTranslation(); }
Vector3f Shape::getRotation()	const { return transformationf_.getRotation(); }
Vector3f Shape::getScale()		const { return transformationf_.getScale(); }

void Shape::translate(const Vector3f& t) { transformationf_.translate(t); }
void Shape::rotate(const Vector3f& r) { transformationf_.rotate(r); }
void Shape::scale(const Vector3f& s) { transformationf_.scale(s); }

void Shape::changeColorf(Colorf* c)
{
	delete colorf_;
	colorf_ = c;
}

void Shape::draw()
{
	glPushMatrix();
	{
		transformationf_.transform();
		colorf_->colorf();
		meshf_->drawMeshf();
	}
	glPopMatrix();
}

Toggleshfape::Toggleshfape() : Shape()
{
	wiremeshf_ = new WireCubeMeshf();
	isSolid_ = true;
}

Toggleshfape::Toggleshfape(const Vector3f& t, const Vector3f& r, const Vector3f& s, Colorf* c, Meshf* m1, Meshf* m2) : Shape(t, r, s, c, m1)
{
	wiremeshf_ = m2;
	isSolid_ = true;
}

void Toggleshfape::draw()
{
	glPushMatrix();
	{
		transformationf_.transform();
		colorf_->colorf();
		isSolid_ ? meshf_->drawMeshf() : wiremeshf_->drawMeshf();
	}
	glPopMatrix();
}

void Toggleshfape::makeSolid() { isSolid_ = true; }
void Toggleshfape::makeWireframe() { isSolid_ = false; }

Colorf::Colorf(int red, int green, int blue)
{
	if (red	  < 0)
		red   = 0;
	if (green < 0)
		green = 0;
	if (blue  < 0)
		blue  = 0;

	if (red   > 255)
		red   = 255;
	if (green > 255)
		green = 255;
	if (blue  > 255)
		blue  = 255;

	red_   = red;
	green_ = green;
	blue_  = blue;
}

void Colorf::colorf()
{
	const float divisor = 255;

	float r = red_   / divisor;
	float g = green_ / divisor;
	float b = blue_  / divisor;

	glColor3f(r, g, b);
}

void CubeMeshf		::drawMeshf() { glutSolidCube(size_); }
void WireCubeMeshf	::drawMeshf() { glutWireCube	(size_); }
void SphereMeshf		::drawMeshf() { glutSolidSphere	(radius_, slices_, stacks_); }
void WireSphereMeshf	::drawMeshf() { glutWireSphere	(radius_, slices_, stacks_); }
void ConeMeshf		::drawMeshf() { glutSolidCone	(radius_, height_, slices_, stacks_); }
void WireConeMeshf	::drawMeshf() { glutWireCone		(radius_, height_, slices_, stacks_); }

void LineMeshf::drawMeshf()
{
	glBegin(GL_LINES);
	{
		glVertex3f(-0.5f, 0, 0);
		glVertex3f( 0.5f, 0, 0);
	}
	glEnd();
}

void PlaneMeshf::drawMeshf()
{
	glBegin(GL_QUADS);
	{
		glVertex3f(-0.5f, 0, -0.5f);
		glVertex3f( 0.5f, 0, -0.5f);
		glVertex3f( 0.5f, 0,  0.5f);
		glVertex3f(-0.5f, 0,  0.5f);
	}
	glEnd();
}

void WirePlaneMeshf::drawMeshf()
{
	glBegin(GL_LINE_STRIP);
	{
		glVertex3f(-0.5f, 0, -0.5f);
		glVertex3f( 0.5f, 0, -0.5f);
		glVertex3f( 0.5f, 0,  0.5f);
		glVertex3f(-0.5f, 0,  0.5f);
		glVertex3f(-0.5f, 0, -0.5f);
	}
	glEnd();
}

//Amandeep Singh
//CS 47101 - Computer Graphics
//transformation.cpp - contains all code for all display tranformations and physics working

#include "transformation.h"

//*********************************************************//
//********************Vector3 Class************************//
//*********************************************************//

Vector3::Vector3()
{
	x = 0.0f;
	y = 0.0f;
	z = 0.0f;
}

Vector3::Vector3(float x_, float y_, float z_)
{
	x = x_;
	y = y_;
	z = z_;
}

Vector3::Vector3(const Vector3& rhs)
{
	x = rhs.x;
	y = rhs.y;
	z = rhs.z;
}

Vector3 Vector3::operator+(const Vector3& rhs) const
{
	Vector3 ret;

	ret.x = x + rhs.x;
	ret.y = y + rhs.y;
	ret.z = z + rhs.z;

	return ret;
}

Vector3& Vector3::operator+=(const Vector3& rhs)
{
	x = x + rhs.x;
	y = y + rhs.y;
	z = z + rhs.z;

	return *this;
}

Vector3 Vector3::operator-(const Vector3& rhs) const
{
	Vector3 ret;

	ret.x = x - rhs.x;
	ret.y = y - rhs.y;
	ret.z = z - rhs.z;

	return ret;
}

Vector3& Vector3::operator-=(const Vector3& rhs)
{
	x = x - rhs.x;
	y = y - rhs.y;
	z = z - rhs.z;

	return *this;
}

Vector3 Vector3::operator*(float rhs) const
{
	Vector3 ret;

	ret.x = x * rhs;
	ret.y = y * rhs;
	ret.z = z * rhs;

	return ret;
}

Vector3& Vector3::operator*=(float rhs)
{
	x = x * rhs;
	y = y * rhs;
	z = z * rhs;

	return *this;
}

Vector3 Vector3::operator/(float rhs) const
{
	Vector3 ret;

	ret.x = x / rhs;
	ret.y = y / rhs;
	ret.z = z / rhs;

	return ret;
}

Vector3& Vector3::operator/=(float rhs)
{
	x = x / rhs;
	y = y / rhs;
	z = z / rhs;

	return *this;
}

float Vector3::magnitude() const
{
	float sMag = squareMagnitude();

	if (sMag <= 0)
		return 0;

	return sqrt(sMag);
}

float Vector3::squareMagnitude() const
{
	return x * x + y * y + z * z;
}

void Vector3::normalize()
{
	float mag = magnitude();

	x /= mag;
	y /= mag;
	z /= mag;
}

Vector3 Vector3::cross(const Vector3& rhs) const
{
	Vector3 ret;

	ret.x = (y * rhs.z) - (z * rhs.y);
	ret.y = (rhs.x * z) - (x * rhs.z);
	ret.z = (x * rhs.y) - (y * rhs.x);

	return ret;
}

float Vector3::dot(const Vector3& rhs) const
{
	return (x * rhs.x) + (y * rhs.y) + (z * rhs.z);
}

Vector3& Vector3::operator=(Vector3 rhs)
{
	swap(rhs);
	return *this;
}

void Vector3::swap(Vector3& rhs)
{
	std::swap(x, rhs.x);
	std::swap(y, rhs.y);
	std::swap(z, rhs.z);
}

bool Vector3::operator==(const Vector3& rhs) const
{
	bool xEqual = (x == rhs.x);
	bool yEqual = (y == rhs.y);
	bool zEqual = (z == rhs.z);

	return xEqual && yEqual && zEqual;
}

bool Vector3::operator!=(const Vector3& rhs) const { return !(*this == rhs); }

std::ostream& operator<<(std::ostream& out, const Vector3& rhs)
{
	out << "<" << rhs.x << ", " << rhs.y << ", " << rhs.z << ">" << std::endl;

	return out;
}

void Vector3::glVertex() const { glVertex3f(x, y, z); }

Vector3 operator*(float lhs, const Vector3& rhs) { return rhs * lhs; }
Vector3 operator/(float lhs, const Vector3& rhs) { return rhs / lhs; }

//*********************************************************//
//*****************Transformation Class********************//
//*********************************************************//

Transformation::Transformation()
{
	position = Vector3();
	rotation = Vector3();
	scale = Vector3(1, 1, 1);
}

Transformation::Transformation(const Vector3& p, const Vector3& r, const Vector3& s)
{
	position = p;
	rotation = r;
	scale = s;
}

Transformation::Transformation(const Transformation& rhs)
{
	position = rhs.position;
	rotation = rhs.rotation;
	scale = rhs.scale;
}

Transformation& Transformation::operator=(Transformation rhs)
{
	swap(rhs);
	return *this;
}

void Transformation::swap(Transformation& rhs)
{
	std::swap(position, rhs.position);
	std::swap(rotation, rhs.rotation);
	std::swap(scale, rhs.scale);
}

bool Transformation::operator==(const Transformation& rhs) const
{
	bool positionEqual = (position == rhs.position);
	bool rotationEqual = (rotation == rhs.rotation);
	bool scaleEqual = (scale == rhs.scale);

	return positionEqual && rotationEqual && scaleEqual;
}

bool Transformation::operator!=(const Transformation& rhs) const { return !(*this == rhs); }

void Transformation::transform()
{
	glTranslatef(position.x, position.y, position.z);

	glScalef(scale.x, scale.y, scale.z);

	glRotatef(rotation.z, 0, 0, 1);
	glRotatef(rotation.y, 0, 1, 0);
	glRotatef(rotation.x, 1, 0, 0);
}

//*********************************************************//
//********************Vector3f Class************************//
//*********************************************************//

Vector3f::Vector3f()
{
	x_ = 0.0;
	y_ = 0.0;
	z_ = 0.0;
}

Vector3f::Vector3f(float x, float y, float z)
{
	x_ = x;
	y_ = y;
	z_ = z;
}

void Vector3f::swap(Vector3f& rhs)
{
	float temp;

	temp = x_;
	x_ = rhs.x_;
	rhs.x_ = temp;

	temp = y_;
	y_ = rhs.y_;
	rhs.y_ = temp;

	temp = z_;
	z_ = rhs.z_;
	rhs.z_ = temp;
}

Vector3f& Vector3f::operator=(Vector3f rhs)
{
	swap(rhs);
	return *this;
}

Vector3f Vector3f::operator+(const Vector3f& rhs) const
{
	return Vector3f(x_ + rhs.x_, y_ + rhs.y_, z_ + rhs.z_);
}

Vector3f& Vector3f::operator+=(const Vector3f& rhs)
{
	x_ += rhs.x_;
	y_ += rhs.y_;
	z_ += rhs.z_;

	return *this;
}

bool Vector3f::operator==(const Vector3f& rhs) const
{
	return (x_ == rhs.x_) && (y_ == rhs.y_) && (z_ == rhs.z_);
}

std::ostream& operator<<(std::ostream& out, const Vector3f& rhs)
{
	out << "<" << rhs.x_ << ", " << rhs.y_ << ", " << rhs.z_ << ">";
	return out;
}

void Vector3f::restrict(float lowerBound, float upperBound)
{
	if (lowerBound == upperBound)
	{
		x_ = lowerBound;
		y_ = lowerBound;
		z_ = lowerBound;

		return;
	}

	if (lowerBound > upperBound)
		std::swap(lowerBound, upperBound);

	while (x_ < lowerBound || x_ >= upperBound)
	{
		if (x_ < lowerBound)
			x_ += upperBound;

		if (x_ >= upperBound)
			x_ -= upperBound;
	}

	while (y_ < lowerBound || y_ >= upperBound)
	{
		if (y_ < lowerBound)
			y_ += upperBound;

		if (y_ >= upperBound)
			y_ -= upperBound;
	}

	while (z_ < lowerBound || z_ >= upperBound)
	{
		if (z_ < lowerBound)
			z_ += upperBound;

		if (z_ >= upperBound)
			z_ -= upperBound;
	}
}

float Vector3f::getX() const { return x_; }
float Vector3f::getY() const { return y_; }
float Vector3f::getZ() const { return z_; }

void Vector3f::setX(float x) { x_ = x; }
void Vector3f::setY(float y) { y_ = y; }
void Vector3f::setZ(float z) { z_ = z; }


//*********************************************************//
//*****************Transformationf Class********************//
//*********************************************************//

Transformationf::Transformationf()
{
	translation_ = Vector3f();
	rotation_ = Vector3f();
	scale_ = Vector3f(1, 1, 1);
}

Transformationf::Transformationf(const Vector3f& t, const Vector3f& r, const Vector3f& s)
{
	translation_ = t;
	(rotation_ = r).restrict(0, 360);
	scale_ = s;
}

void Transformationf::setTranslation(const Vector3f& p) { translation_ = p; }
void Transformationf::setRotation(const Vector3f& r) { (rotation_ = r).restrict(0, 360); }
void Transformationf::setScale(const Vector3f& s) { scale_ = s; }

Vector3f Transformationf::getTranslation()const { return translation_; }
Vector3f Transformationf::getRotation()	const { return rotation_; }
Vector3f Transformationf::getScale()		const { return scale_; }

void Transformationf::translate(const Vector3f& t) { translation_ += t; }
void Transformationf::rotate(const Vector3f& r) { (rotation_ += r).restrict(0, 360); }
void Transformationf::scale(const Vector3f& s) { scale_ += s; }

void Transformationf::transform() const
{
	glTranslatef(translation_.getX(), translation_.getY(), translation_.getZ());

	glScalef(scale_.getX(), scale_.getY(), scale_.getZ());

	glRotatef(rotation_.getZ(), 0, 0, 1);
	glRotatef(rotation_.getY(), 0, 1, 0);
	glRotatef(rotation_.getX(), 1, 0, 0);
}
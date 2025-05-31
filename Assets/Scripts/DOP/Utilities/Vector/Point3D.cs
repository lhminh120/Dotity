using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point3D
{
    public float _x;
    public float _y;
    public float _z;
    public static Point3D Zero { get { return new Point3D(); } }
    public static Point3D One { get { return new Point3D(1, 1, 1); } }
    public static Point3D Up { get { return new Point3D(1, 1, 1); } }

    public bool CheckZero()
    {
        return (_x == 0 && _y == 0 && _z == 0);
    }
    #region Contructor
    public Point3D(float x = 0, float y = 0, float z = 0)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    //public Point3D(float x, float y)
    //{
    //    _x = x;
    //    _y = y;
    //    _z = 0;
    //}
    //public Point3D()
    //{
    //    _x = 0;
    //    _y = 0;
    //    _z = 0;
    //}
    public Point3D(Vector3 vec)
    {
        _x = vec.x;
        _y = vec.y;
        _z = vec.z;
    }
    public Point3D(Point3D point3D)
    {
        _x = point3D._x;
        _y = point3D._y;
        _z = point3D._z;
    }
    #endregion
    #region Set Value
    public void Set(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    public void Set(float x, float y)
    {
        _x = x;
        _y = y;
    }
    public void Set(Vector3 vec3)
    {
        _x = vec3.x;
        _y = vec3.y;
        _z = vec3.z;
    }
    public void Set(Vector2 vec2)
    {
        _x = vec2.x;
        _y = vec2.y;
    }
    public void Set(Point3D point3D)
    {
        _x = point3D._x;
        _y = point3D._y;
        _z = point3D._z;
    }
    #endregion
    #region Move Forward
    public void MoveForward(Point3D b, float value)
    {
        float x = b._x - _x;
        float y = b._y - _y;
        float z = b._z - _z;
        double length = Math.Sqrt(x * x + y * y + z * z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
            z *= tempValue;
        }
        _x += x * value;
        _y += y * value;
        _z += z * value;

    }
    public void MoveForward(Point2 b, float value)
    {
        float x = b._x - _x;
        float y = b._y - _y;
        double length = Math.Sqrt(x * x + y * y + _z * _z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
        }
        //Point3D temp = new Point3D(b._x - _x, b._y - _y, _z);
        //temp.normalize();
        _x += x * value;
        _y += y * value;

    }
    public static Point3D MoveForward(Point3D a, Point3D b, float value)
    {
        float x = b._x - a._x;
        float y = b._y - a._y;
        float z = b._z - a._z;
        double length = Math.Sqrt(x * x + y * y + z * z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
            z *= tempValue;
        }
        return new Point3D(a._x + x * value, a._y + y * value, a._z + z * value);

    }
    public static Point3D MoveForward(Point3D a, Point2 b, float value)
    {
        float x = b._x - a._x;
        float y = b._y - a._y;
        double length = Math.Sqrt(x * x + y * y + a._z * a._z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
        }
        return new Point3D(a._x + x * value, a._y + y * value, a._z);

    }
    #endregion
    #region Translate
    public void Translate(Point3D b, float speed)
    {
        Point3D temp = b.Clone();
        double length = Math.Sqrt(temp._x * temp._x + temp._y * temp._y + temp._z * temp._z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            temp._x *= tempValue;
            temp._y *= tempValue;
            temp._z *= tempValue;
        }
        _x += temp._x * speed;
        _y += temp._y * speed;
        _z += temp._z * speed;
    }
    #endregion
    #region Distance
    public float sqrMagnitude()
    {
        return _x * _x + _y * _y + _z * _z;
    }
    public double magnitude()
    {
        return Math.Sqrt(_x * _x + _y * _y + _z * _z);
    }
    public double Distance()
    {
        return Math.Sqrt(_x * _x + _y * _y + _z * _z);
    }
    public void normalize()
    {
        double length = Math.Sqrt(_x * _x + _y * _y + _z * _z);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            _x *= tempValue;
            _y *= tempValue;
            _z *= tempValue;
        }

    }
    public Point3D Normalization()
    {
        double length = Distance();
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            return new Point3D(_x * tempValue, _y * tempValue, _z * tempValue);
        }
        else
        {
            return this;
        }

    }
    #endregion
    #region Operator
    public static Point3D operator *(Point3D a, float b)
    {
        Point3D temp = a.Clone();
        temp._x *= b;
        temp._y *= b;
        temp._z *= b;
        return temp;
    }
    public static Point3D operator /(Point3D a, float b)
    {
        Point3D temp = a.Clone();
        float tempValue = 1 / b;
        temp._x *= tempValue;
        temp._y *= tempValue;
        temp._z *= tempValue;
        return temp;
    }
    public static Point3D operator +(Point3D a, Point3D b)
    {
        Point3D temp = a.Clone();
        temp._x += b._x;
        temp._y += b._y;
        temp._z += b._z;
        return temp;
        //return new Point3D(a._x + b._x, a._y + b._y, a._z + b._z);
    }


    public static Point3D operator -(Point3D a, Point3D b)
    {
        Point3D temp = a.Clone();
        temp._x -= b._x;
        temp._y -= b._y;
        temp._z -= b._z;
        return temp;
        //return new Point3D(a._x - b._x, a._y - b._y, a._z - b._z);
    }
    public static bool operator ==(Point3D a, Point3D b)
    {
        if (a._x == b._x && a._y == b._y && a._z == b._z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(Point3D a, Point3D b)
    {
        if (a._x != b._x || a._y != b._y || a._z != b._z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    #region Clone
    public Point3D Clone()
    {
        return new Point3D(_x, _y, _z);
    }
    #endregion
}


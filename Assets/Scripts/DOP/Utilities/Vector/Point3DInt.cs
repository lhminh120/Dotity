﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point3DInt
{
    public int _x;
    public int _y;
    public int _z;
    public static Point3DInt Zero { get { return new Point3DInt(); } }
    public static Point3DInt One { get { return new Point3DInt(1, 1, 1); } }
    public static Point3DInt Up { get { return new Point3DInt(1, 1, 1); } }
    public bool CheckZero()
    {
        return (_x == 0 && _y == 0 && _z == 0);
    }
    #region Contructor
    public Point3DInt(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    public Point3DInt(int x, int y)
    {
        _x = x;
        _y = y;
        _z = 0;
    }
    public Point3DInt()
    {
        _x = 0;
        _y = 0;
        _z = 0;
    }
    public Point3DInt(Vector3Int vec)
    {
        _x = vec.x;
        _y = vec.y;
        _z = vec.z;
    }
    #endregion
    #region Set Value
    public virtual void Set(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    public virtual void Set(int x, int y)
    {
        _x = x;
        _y = y;
    }
    public virtual void Set(Vector3Int vec3)
    {
        _x = vec3.x;
        _y = vec3.y;
        _z = vec3.z;
    }
    public virtual void Set(Vector2Int vec2)
    {
        _x = vec2.x;
        _y = vec2.y;
    }
    #endregion
    #region Move Forward
    //public void MoveForward(Point3DInt b, float value)
    //{
    //    float x = b._x - _x;
    //    float y = b._y - _y;
    //    float z = b._z - _z;
    //    double length = Math.Sqrt(x * x + y * y + z * z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        x *= tempValue;
    //        y *= tempValue;
    //        z *= tempValue;
    //    }
    //    _x += x * value;
    //    _y += y * value;
    //    _z += z * value;

    //}
    //public void MoveForward(Point2 b, float value)
    //{
    //    float x = b._x - _x;
    //    float y = b._y - _y;
    //    double length = Math.Sqrt(x * x + y * y + _z * _z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        x *= tempValue;
    //        y *= tempValue;
    //    }
    //    //Point3D temp = new Point3D(b._x - _x, b._y - _y, _z);
    //    //temp.normalize();
    //    _x += x * value;
    //    _y += y * value;

    //}
    //public static Point3D MoveForward(Point3D a, Point3D b, float value)
    //{
    //    float x = b._x - a._x;
    //    float y = b._y - a._y;
    //    float z = b._z - a._z;
    //    double length = Math.Sqrt(x * x + y * y + z * z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        x *= tempValue;
    //        y *= tempValue;
    //        z *= tempValue;
    //    }
    //    return new Point3D(a._x + x * value, a._y + y * value, a._z + z * value);

    //}
    //public static Point3D MoveForward(Point3D a, Point2 b, float value)
    //{
    //    float x = b._x - a._x;
    //    float y = b._y - a._y;
    //    double length = Math.Sqrt(x * x + y * y + a._z * a._z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        x *= tempValue;
    //        y *= tempValue;
    //    }
    //    return new Point3D(a._x + x * value, a._y + y * value, a._z);

    //}
    #endregion
    #region Translate
    //public void Translate(Point3D b, float speed)
    //{
    //    double length = Math.Sqrt(b._x * b._x + b._y * b._y + b._z * b._z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        b._x *= tempValue;
    //        b._y *= tempValue;
    //        b._z *= tempValue;
    //    }
    //    _x += b._x * speed;
    //    _y += b._y * speed;
    //    _z += b._z * speed;
    //}
    #endregion
    #region Distance
    //public float sqrMagnitude()
    //{
    //    return _x * _x + _y * _y + _z * _z;
    //}
    //public double magnitude()
    //{
    //    return Math.Sqrt(_x * _x + _y * _y + _z * _z);
    //}
    //public double Distance()
    //{
    //    return Math.Sqrt(_x * _x + _y * _y + _z * _z);
    //}
    //public void normalize()
    //{
    //    double length = Math.Sqrt(_x * _x + _y * _y + _z * _z);
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        _x *= tempValue;
    //        _y *= tempValue;
    //        _z *= tempValue;
    //    }

    //}
    //public Point3D Normalization()
    //{
    //    double length = Distance();
    //    if (length > 0)
    //    {
    //        float tempValue = 1 / (float)length;
    //        return new Point3D(_x * tempValue, _y * tempValue, _z * tempValue;
    //    }
    //    else
    //    {
    //        return this;
    //    }

    //}
    #endregion
    #region Operator
    //public static Point3D operator *(Point3D a, float b)
    //{
    //    a._x *= b;
    //    a._y *= b;
    //    a._z *= b;
    //    return a;
    //}
    //public static Point3D operator /(Point3D a, float b)
    //{
    //    float tempValue = 1 / b;
    //    a._x *= tempValue;
    //    a._y *= tempValue;
    //    a._z *= tempValue;
    //    return a;
    //}
    //public static Point3D operator +(Point3D a, Point3D b)
    //{
    //    a._x += b._x;
    //    a._y += b._y;
    //    a._z += b._z;
    //    return a;
    //    //return new Point3D(a._x + b._x, a._y + b._y, a._z + b._z);
    //}


    //public static Point3D operator -(Point3D a, Point3D b)
    //{
    //    a._x -= b._x;
    //    a._y -= b._y;
    //    a._z -= b._z;
    //    return a;
    //    //return new Point3D(a._x - b._x, a._y - b._y, a._z - b._z);
    //}
    //public static bool operator ==(Point3D a, Point3D b)
    //{
    //    if (a._x == b._x && a._y == b._y && a._z == b._z)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    //public static bool operator !=(Point3D a, Point3D b)
    //{
    //    if (a._x != b._x || a._y != b._y || a._z != b._z)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    #endregion
    #region Clone
    public Point3DInt Clone()
    {
        return new Point3DInt(_x, _y, _z);
    }
    #endregion
}




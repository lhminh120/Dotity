using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2D : Point3D
{
    #region Contructor
    public Point2D(float x, float y)
    {
        _x = x;
        _y = y;
        _z = 0;
    }
    public Point2D()
    {
        _x = 0;
        _y = 0;
        _z = 0;
    }
    #endregion
    #region Move Forward
    public void MoveForward(Point2D b, float value)
    {
        float x = b._x - _x;
        float y = b._y - _y;
        double length = Math.Sqrt(x * x + y * y);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
        }
        _x += x * value;
        _y += y * value;
    }

    public static Point2D MoveForward(Point2D a, Point2D b, float value)
    {
        float x = b._x - a._x;
        float y = b._y - a._y;
        double length = Math.Sqrt(x * x + y * y);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
        }
        return new Point2D(a._x + x * value, a._y + y * value);
    }
    public static Point2D MoveForward(Point2D a, Point2 b, float value)
    {
        float x = b._x - a._x;
        float y = b._y - a._y;
        double length = Math.Sqrt(x * x + y * y);
        if (length > 0)
        {
            float tempValue = 1 / (float)length;
            x *= tempValue;
            y *= tempValue;
        }
        return new Point2D(a._x + x * value, a._y + y * value);
    }
    #endregion
}

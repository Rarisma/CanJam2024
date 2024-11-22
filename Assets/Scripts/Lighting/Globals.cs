using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deg">degrees</param>
    public static Vector2 DegToVector(float deg){
        float radians = Globals.ConvertToRadians(deg);
        return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }

    public static float ConvertToRadians(float angle)
    {
        return (float)((Math.PI / 180) * angle);
    }
}
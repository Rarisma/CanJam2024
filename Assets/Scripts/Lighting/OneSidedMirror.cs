using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OneSidedMirror : LightObject
{
    /// <summary>
    /// Which direction the mirror is facing (e.g 1, 1 is top right diagonal)
    /// If the incoming direction is 1, 0 (right) and you hit a -1, -1 (down left) the resulting vector should be 0, -1 (down)
    /// if the incoming direction is 1, 0 (right) and you hit a -1, 1 (up left) the resulting vector should be 0, 1 (up)
    /// if the incoming direction is 1, 0 (right) and you hit a 1, -1 (down right) there should be no vector since its the back of the mirror
    /// </summary>
    public Vector2 reflectAxis;
    void Start()
    {
        CreateLightRays(1);
    }

    public override void OnHit(Vector2 hitPos, Vector2 incomingDir, LightObject last_hit){
        reflectAxis = Globals.DegToVector(currentRotation + 45);

        print("reflector dir: " + reflectAxis);
        // If any of the axis are equal, then there should be no reflection
        if (Math.Sign(incomingDir.x) == Math.Sign(reflectAxis.x)){
            return;
        }
        if (Math.Sign(incomingDir.y) == Math.Sign(reflectAxis.y)){
            return;
        }

        Vector2 newDir = Vector2.Reflect(incomingDir.normalized, reflectAxis.normalized);

        Emit(0, hitPos, newDir, last_hit);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    public Vector2 newDir;


    public override void OnHit(Vector2 hitPos, Vector2 incomingDir, Vector2 hitNormal, LightObject last_hit, int recursions = 0){
        float currentRotation = GetCurrentRotation();
        reflectAxis = Globals.DegToVector(currentRotation + 45);

        print("reflector " + gameObject.name + " dir: " + reflectAxis + " incoming: " + incomingDir);
        // If any of the axis are equal, then there should be no reflection

        newDir = Vector2.Reflect(incomingDir.normalized, hitNormal);
        
        Emit(hitPos, newDir, last_hit);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FreeMirror : OneSidedMirror
{


    public override void OnHit(Vector2 hitPos, Vector2 incomingDir, Vector2 hitNormal, LightObject last_hit, int recursions = 0)
    {
        float currentRotation = GetCurrentRotation();
        reflectAxis = Globals.DegToVector(currentRotation + 45);

        print("reflector " + gameObject.name + " dir: " + reflectAxis + " incoming: " + incomingDir);
        // If any of the axis are equal, then there should be no reflection

        newDir = Vector2.Reflect(incomingDir.normalized, hitNormal);

        Emit(hitPos, newDir, last_hit);
    }
}
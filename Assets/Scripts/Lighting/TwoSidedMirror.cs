using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSidedMirror : LightReflector
{
    /// <summary>
    /// A two sided diagonal mirror can only have two rotation states really
    /// its either top right + bottom left
    /// or top left + bottom right
    /// </summary>
    public bool mirrored;
    

    /// <summary>
    /// Set one top right is reflectAxisOne, bottom left is -reflectAxisOne
    /// </summary>
    Vector2 reflectAxisOne = new Vector2(1, 1);

    /// <summary>
    /// Set two top left is reflectAxisTwo, bottom right is -reflexAxisTwo
    /// </summary>
    Vector2 reflectAxisTwo = new Vector2(-1, 1);

    public override Vector2[] Reflect(Vector2 incomingDir){

        Vector2 currentReflectAxis = reflectAxisOne;
        if (mirrored) currentReflectAxis = reflectAxisTwo;

        // If any of the axis are equal, then there should be no reflection
        if (incomingDir.x == currentReflectAxis.x){
            currentReflectAxis = -currentReflectAxis;
        }
        if (incomingDir.y == currentReflectAxis.y){
            currentReflectAxis = -currentReflectAxis;
        }

        return new Vector2[] {Vector2.Reflect(incomingDir.normalized, currentReflectAxis.normalized)};
    }
}
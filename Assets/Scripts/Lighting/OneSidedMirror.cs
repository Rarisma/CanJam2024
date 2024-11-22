using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidedMirror : LightReflector
{
    /// <summary>
    /// Which direction the mirror is facing (e.g 1, 1 is top right diagonal)
    /// If the incoming direction is 1, 0 (right) and you hit a -1, -1 (down left) the resulting vector should be 0, -1 (down)
    /// if the incoming direction is 1, 0 (right) and you hit a -1, 1 (up left) the resulting vector should be 0, 1 (up)
    /// if the incoming direction is 1, 0 (right) and you hit a 1, -1 (down right) there should be no vector since its the back of the mirror
    /// </summary>
    public Vector2 reflectAxis;
    public override Vector2[] Reflect(Vector2 incomingDir){

        // If any of the axis are equal, then there should be no reflection
        if (incomingDir.x == reflectAxis.x){
            return new Vector2[] {};
        }
        if (incomingDir.y == reflectAxis.y){
            return new Vector2[] {};
        }

        return new Vector2[] {Vector2.Reflect(incomingDir.normalized, reflectAxis.normalized)};
    }
}
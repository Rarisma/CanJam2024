using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : LightObject
{
    public override void Update()
    {
        base.Update();
        Vector2 emitDirection = Globals.DegToVector(GetCurrentRotation());
        transform.right = Vector2.right;
        print("emitter dir:" + emitDirection);
        Emit(transform.position, emitDirection, null);
    }
}

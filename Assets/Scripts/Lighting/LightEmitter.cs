using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : LightObject
{

    int indexes;
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Update()
    {
        base.Update();
        Vector2 emitDirection = Globals.DegToVector(GetCurrentRotation());
        print("emitter dir:" + emitDirection);
        Emit(transform.position, emitDirection, null);
    }
}

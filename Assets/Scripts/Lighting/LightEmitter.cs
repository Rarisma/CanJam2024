using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : LightObject
{

    int indexes;
    // Start is called before the first frame update
    void Start()
    {
        CreateLightRays(1);
    }

    // Update is called once per frame
    public void Update()
    {
        Vector2 emitDirection = Globals.DegToVector(currentRotation);
        print("emitter dir:" + emitDirection);
        Emit(0, transform.position, emitDirection, null);
    }
}

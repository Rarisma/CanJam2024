using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : LightObject
{

    int indexes;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRay();
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void UpdateRay()
    {
        ResetLightRays();
        Vector2 emitDirection = Globals.DegToVector(GetCurrentRotation());
        print("emitter dir:" + emitDirection);
        Emit(transform.position, emitDirection, null);
    }
}

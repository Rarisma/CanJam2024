using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemitransparentMirror : OneSidedMirror
{
    public override void OnHit(Vector2 hitPos, Vector2 incomingDir, Vector2 hitNormal, LightObject last_hit)
    {
        base.OnHit(hitPos, incomingDir, hitNormal, last_hit);
        Emit(transform.position, incomingDir, this);
    }
}

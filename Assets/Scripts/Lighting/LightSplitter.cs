using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSplitter : LightObject
{
    public override void OnHit(Vector2 hitPos, Vector2 incomingDir, Vector2 hitNormal, LightObject last_hit, int recursions = 0)
    {
        float rotation = GetCurrentRotation();
        print(rotation);
        incomingDir = incomingDir.normalized;

        print("Splitter: " + incomingDir);

        if (rotation > 45 && rotation <= 135)
        {
            if (incomingDir.y < 0.71f && incomingDir.y > -0.71f && incomingDir.x < 0)
            {
                Emit(transform.position, Vector2.up, this, recursions);
                Emit(transform.position, Vector2.down, this, recursions);
            }
        }
        if (rotation > 135 && rotation <= 225)
        {
            if (incomingDir.x < 0.71f && incomingDir.x > -0.71f && incomingDir.y < 0)
            {
                Emit(transform.position, Vector2.left, this, recursions);
                Emit(transform.position, Vector2.right, this, recursions);
            }
        }
        if (rotation > 225 && rotation <= 315)
        {
            if (incomingDir.y < 0.71f && incomingDir.y > -0.71f && incomingDir.x > 0)
            {
                Emit(transform.position, Vector2.up, this, recursions);
                Emit(transform.position, Vector2.down, this, recursions);
            }
        }
        if (rotation > 315 || rotation <= 45)
        {
            if (incomingDir.x < 0.71f && incomingDir.x > -0.71f && incomingDir.y > 0)
            {
                Emit(transform.position, Vector2.left, this, recursions);
                Emit(transform.position, Vector2.right, this, recursions);
            }
        }

    }
}
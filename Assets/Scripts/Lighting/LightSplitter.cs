using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSplitter : MonoBehaviour
{
    /// <summary>
    /// Horizontal reflection if false, vertical reflection otherwise
    /// </summary>
    public bool isUpright;

    public virtual Vector2[] Reflect(Vector2 incomingDir)
    {
        if (isUpright) //Split to left and right
        {
            return new Vector2[] {
               Vector2.left,
               Vector2.right
            };
        }
        else //Split to up and down
        {
            return new Vector2[] {
                Vector2.up,
                Vector2.down
            };
        }

    }
}

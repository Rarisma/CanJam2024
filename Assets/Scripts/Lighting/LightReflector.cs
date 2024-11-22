using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour
{
    public virtual Vector2 Reflect(Vector2 incomingDir){
        return new Vector2(1, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightObject : PlaceableObject
{
    public LineRenderer[] lrs = new LineRenderer[] {};

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        ResetLightRays();
    }

    public virtual void OnHit(Vector2 hitPos, Vector2 emitDir, LightObject last_hit = null){
    }

    public void CreateLightRays(int count){
        print("check");
        LineRenderer[] newlrs = new LineRenderer[count];
        for (int x = 0; x < count; x++){
            GameObject go = new GameObject("ray");
            go.transform.parent = transform;
            go.transform.localPosition = Vector2.zero;

            LineRenderer lr = go.AddComponent<LineRenderer>();
            
            lr.startWidth = 0.2f;
            lr.endWidth = 0.2f;
            lr.positionCount = 2;
            lr.startColor = Color.yellow;
            lr.endColor = Color.yellow;
            lr.material = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));

            newlrs[x] = lr;
        }

        lrs = newlrs;
    }

    public void ResetLightRays(){
        foreach (LineRenderer lr in lrs){
            lr.positionCount = 0;
        }
    }

    public void Emit(int LrIndex, Vector2 startPos, Vector2 direction, LightObject last_hit){
        print("an object is emitting: " + gameObject.name);
        lrs[LrIndex].positionCount = 2;
        lrs[LrIndex].SetPosition(0, transform.position);
        RaycastHit2D ray = Physics2D.CircleCast(startPos, 0.1f, direction.normalized, 20f);

        if (ray){
            lrs[LrIndex].SetPosition(1, ray.transform.position);
            if (ray.transform.TryGetComponent<LightObject>(out LightObject reflector)){
                if (!(reflector == this || reflector == last_hit)){
                    reflector.OnHit(ray.point, direction, this);
                }
                reflector.OnHit(ray.point, direction, this);
            }
        }
        else{
            lrs[LrIndex].SetPosition(1, startPos + (direction * 100f));
        }
    }
    
}

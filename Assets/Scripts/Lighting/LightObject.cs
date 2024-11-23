using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour 
{
    public List<LineRenderer> lrs = new List<LineRenderer>();

    // Update is called once per frame
    public virtual void FixedUpdate()
    {

    }

    public virtual void OnHit(Vector2 hitPos, Vector2 emitDir, Vector2 hitNormal, LightObject last_hit = null)
    {
        ResetLightRays();
    }

    public LineRenderer CreateLightRay(){
        print("check");

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
        lrs.Add(lr);
        return lr;
    }

    public void ResetLightRays(){
        foreach (LineRenderer lr in lrs){
            Destroy(lr.gameObject);
        }
        lrs.Clear();
    }

    public void Emit(Vector2 startPos, Vector2 direction, LightObject last_hit){
        print("an object is emitting: " + gameObject.name + "at direction " + direction.x);
        LineRenderer lr = CreateLightRay();
        lr.SetPosition(0, startPos);
        RaycastHit2D ray =  Physics2D.Raycast(startPos, direction.normalized, 20f, 1 << 3);

        if (ray){
            lr.SetPosition(1, ray.point);
            if (ray.transform.TryGetComponent<LightObject>(out LightObject reflector)){
                if (!(reflector == this || reflector == last_hit)){
                    reflector.OnHit(ray.point, direction, ray.normal, this);
                }
                reflector.OnHit(ray.point, direction, ray.normal, this);
            }
        }
        else{
            lr.SetPosition(1, startPos + (direction * 100f));
        }
    }

    public float GetCurrentRotation()
    {
        return transform.parent.GetComponent<PlaceableObject>()?.currentRotation ?? 0.0f;
    }
    
}

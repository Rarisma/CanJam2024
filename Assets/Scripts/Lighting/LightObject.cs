using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightObject : MonoBehaviour 
{
    public List<LineRenderer> lrs = new List<LineRenderer>();
    LineDrawer lineDrawer;

    // Update is called once per frame
    public virtual void Update()
    {
        lineDrawer = GameObject.FindObjectOfType<LineDrawer>();
    }

    public virtual void OnHit(Vector2 hitPos, Vector2 emitDir, Vector2 hitNormal, LightObject last_hit = null)
    {
        ResetLightRays();
    }

    public void DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        Material lineMaterial = new(Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default"));

        // Create the quad mesh for the line
        Mesh lineMesh = new Mesh();

        Vector3 direction = (endPoint - startPoint).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x) * 0.01f;

        Vector3[] vertices = new Vector3[4]
        {
            startPoint - (Vector2)perpendicular,
            startPoint + (Vector2)perpendicular,
            endPoint - (Vector2)perpendicular,
            endPoint + (Vector2)perpendicular
        };

        int[] triangles = new int[6] { 0, 1, 2, 2, 1, 3 };

        lineMesh.vertices = vertices;
        lineMesh.triangles = triangles;

        // Set color for the material
        lineMaterial.SetColor("_Color", Color.yellow);

        // Draw the mesh
        Graphics.DrawMesh(lineMesh, Matrix4x4.identity, lineMaterial, 0);
    }


    public void ResetLightRays(){
        foreach (LineRenderer lr in lrs){
            Destroy(lr.gameObject);
        }
        lrs.Clear();
    }

    public void Emit(Vector2 startPos, Vector2 direction, LightObject last_hit){
        print("an object is emitting: " + gameObject.name + "at direction " + direction.x);
        RaycastHit2D ray =  Physics2D.Raycast(startPos, direction.normalized, 20f, 1 << 3);

        if (ray){
            if (ray.distance < 1f) return;
            DrawLine(startPos, ray.point);
            if (ray.transform.TryGetComponent<LightObject>(out LightObject reflector)){
                if (!(reflector == this || reflector == last_hit)){
                    reflector.OnHit(ray.point, direction, ray.normal, this);
                }
                reflector.OnHit(ray.point, direction, ray.normal, this);
            }
        }
        else{
            DrawLine(startPos, startPos + (direction * 100f));
        }
    }

    public float GetCurrentRotation()
    {
        return transform.parent.GetComponent<PlaceableObject>()?.currentRotation ?? 0.0f;
    }
    
}

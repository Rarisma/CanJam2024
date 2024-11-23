using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightObject : MonoBehaviour 
{
    public List<LineRenderer> lrs = new List<LineRenderer>();
    public Material lineMaterial;

    public virtual void Start()
    {
        if (transform.parent.TryGetComponent<PlaceableObject>(out PlaceableObject po))
        {
            if (!po.isMovable)
            {
                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void OnHit(Vector2 hitPos, Vector2 emitDir, Vector2 hitNormal, LightObject last_hit = null, int recursions = 0)
    {
        ResetLightRays();
    }

    public void DrawLine(Vector2 start, Vector2 end)
    {
        Vector3 startPoint = new Vector3(start.x, start.y, -0.1f);
        Vector3 endPoint = new Vector3(end.x, end.y, -0.1f);

        // Create the quad mesh for the line
        Mesh lineMesh = new Mesh();

        Vector3 direction = (endPoint - startPoint).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x) * 0.1f;

        float length = Vector2.Distance(start, end);

        float tile_value = 0.05f;

        List<Vector3> bottom_verticies = new List<Vector3>();
        List<Vector3> top_verticies = new List<Vector3>();
        List<int> triangles = new List<int>(); // Triangle indices

        for (float x = length; x > 0; x-= tile_value)
        {
            float height = (float)Math.Sin((-x * 10) + Time.time * 10f) /3f ;
            // Calculate the bottom and top vertices
            Vector3 bottom = startPoint + (direction * x) + perpendicular;
            Vector3 top = startPoint + (direction * x) - perpendicular;

            bottom = bottom + (perpendicular * height);
            top = top + (perpendicular * height);

            bottom_verticies.Add(bottom);
            top_verticies.Add(top);
        }
        Vector3[] vertices = bottom_verticies.Concat(top_verticies).ToArray();


        // Create triangles to connect the vertices into quads
        for (int i = 0; i < bottom_verticies.Count - 2; i++)
        {
            // Define the triangles that form the line
            triangles.Add(i);
            triangles.Add(i + bottom_verticies.Count);
            triangles.Add(i + 1);

            triangles.Add(i + 1);
            triangles.Add(i + bottom_verticies.Count);
            triangles.Add(i + 1 + bottom_verticies.Count);
        }

        lineMesh.vertices = vertices.ToArray();
        lineMesh.triangles = triangles.ToArray();

        // Draw the mesh
        Graphics.DrawMesh(lineMesh, Matrix4x4.identity, lineMaterial, 0);

        Destroy(lineMesh, 0.1f );
    }


    public void ResetLightRays(){
        foreach (LineRenderer lr in lrs){
            Destroy(lr.gameObject);
        }
        lrs.Clear();
    }

    public void Emit(Vector2 startPos, Vector2 direction, LightObject last_hit, int recursions = 0){
        if (recursions > 50) return;
        direction.x = (float)Math.Round(direction.x, 2);
        direction.y = (float)Math.Round(direction.y, 2);

        print("an object is emitting: " + gameObject.name + "at direction " + direction);

        RaycastHit2D[] rays =  Physics2D.RaycastAll(startPos, direction.normalized, 20f, (1 << 0) | (1 << 3));

        foreach (RaycastHit2D ray in rays)
        {
            if (ray.transform.gameObject == gameObject)
            {
                continue;
            }

            print("an object is hitting: " + gameObject.name + " " + ray.transform.gameObject.name);
            DrawLine(startPos, ray.point);
            if (ray.distance < 0.5f) return;
            if (ray.transform.TryGetComponent<LightObject>(out LightObject reflector))
            {
                if (!(reflector == this || reflector == last_hit))
                {
                    reflector.OnHit(ray.point, direction, ray.normal, this, recursions);
                }
            }
            return;
            
        }

       
       DrawLine(startPos, startPos + (direction * 100f));
        
    }

    public float GetCurrentRotation()
    {
        return transform.parent.GetComponent<PlaceableObject>()?.currentRotation ?? 0.0f;
    }
    
}

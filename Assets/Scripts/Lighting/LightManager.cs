using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    List<LightEmitter> emitters = new List<LightEmitter>();

    // Start is called before the first frame update
    void Start()
    {
        emitters = FindLightEmitters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEmitters()
    {
        foreach (LightEmitter emitter in emitters)
        {
            print("holy");
            emitter.UpdateRay();
        }
    }

    public static List<LightEmitter> FindLightEmitters()
    {
        List<LightEmitter> gameObjects = new List<LightEmitter>();

        // Find all objects of the specified component type
        LightEmitter[] components = GameObject.FindObjectsOfType<LightEmitter>();

        // Add their associated game objects to the list
        foreach (LightEmitter component in components)
        {
            gameObjects.Add(component);
        }

        return gameObjects;
    }
}

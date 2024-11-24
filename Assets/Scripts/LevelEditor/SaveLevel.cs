using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SaveLevel : MonoBehaviour
{
    public TMP_InputField fileNameInput;
    public GridManager GMan;
    public void Save()
    {

        // Get the active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // List to hold all GameObjects
        List<GameObject> allGameObjects = new List<GameObject>();

        // Get root GameObjects in the scene
        GameObject[] rootObjects = currentScene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            // Add the root object
            allGameObjects.Add(obj);

            // Recursively add all child GameObjects
            GetChildObjects(obj, allGameObjects);
        }

        List<SerializableItem> Objects = new();
        foreach (GameObject obj in allGameObjects) {
            Objects.Add(new SerializableItem()
            {
                name = obj.name,
                Postion = obj.transform.position
            });
        }


        //Save file
        File.WriteAllText(fileNameInput.text,
            JsonUtility.ToJson(new SerializableItemList(){
                Items = Objects,
                GridWidth = GMan.width,
                GridHeight = GMan.height,
            }, true));
    }

    void GetChildObjects(GameObject parent, List<GameObject> list)
    {
        foreach (Transform child in parent.transform)
        {
            list.Add(child.gameObject);
            GetChildObjects(child.gameObject, list);
        }
    }
}

[Serializable]
public class SerializableItem
{
    public string name;
    public Vector3 Postion;
}

[Serializable]
public class SerializableItemList
{
    public int GridWidth = 0;
    public int GridHeight = 0;
    public List<SerializableItem> Items;
}
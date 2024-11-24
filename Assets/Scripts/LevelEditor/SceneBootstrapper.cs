using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneBootstrapper : MonoBehaviour
{
    public TMP_InputField fileNameInput;
    // Start is called before the first frame update
    public void Bootstrap()
    {
        string jmson = File.ReadAllText(fileNameInput.text);
        SerializableItemList Level = (SerializableItemList)JsonUtility.FromJson(jmson, typeof(SerializableItemList));

        //Reset scene
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.tag != "MainCamera") // Exclude objects with the "MainCamera" tag
            {
                Destroy(obj);
            }
        }

        GameObject gman = Instantiate(Resources.Load<GameObject>("Prefabs/gman"), new Vector3(), new Quaternion());

        //Set board size
        gman.GetComponent<GridManager>().cam = Camera.main.transform;
        gman.GetComponent<GridManager>().width = Level.GridWidth;
        gman.GetComponent<GridManager>().height = Level.GridHeight;

        //Set up inventory
        TMP_Text[] texts = new TMP_Text[4];
        for (int i = 0; i < texts.Length; i++)
        {
            GameObject textObject = new GameObject($"TMP_Text_{i}");
            texts[i] = textObject.AddComponent<TMP_Text>();
        }

        gman.GetComponent<ObjectInventory>().objectAmountText = texts;
        gman.GetComponent<ObjectInventory>().objectAmounts = new int[4] {9,9,9,9};
        gman.GetComponent<ObjectInventory>().objectNames = new string[4]{
            "Mirror",
            "FreeMirror",
            "LightSplitter",
            "TransparentMirror",
        };

        GameObject Inventory = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory UI"), new Vector3(), new Quaternion());
        Inventory.GetComponent<ObjectSummon>().objectInventory = gman.GetComponent<ObjectInventory>();
        foreach (var item in Level.Items)
        {
            GameObject Item;
            switch (item.name)
            {
                case "LightSplitter":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/LightSplitter"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "Fan":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/Fan"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "LightEnd":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/LightEnd"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "FreeMirror":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/FreeMirror"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "Mirror":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/Mirror"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "Smoke":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/Smoke"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "TransparentMirror":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/TransparentMirror"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "Wall":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/Wall"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
                case "LightStart":
                    Item = Instantiate(Resources.Load<GameObject>("Prefabs/LightStart"), item.Postion, new Quaternion());
                    Item.GetComponent<PlaceableObject>().isMovable = false;
                    break;
            }
        }

        //Add event system to make buttons clickable
        GameObject eventSystem = new ("EventSystem");
        eventSystem.AddComponent<StandaloneInputModule>();
        eventSystem.AddComponent<EventSystem>();
    }
}

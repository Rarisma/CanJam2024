using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInventory : MonoBehaviour
{
    public Dictionary<string, int> objectAmount = new();
    public TMP_Text[] objectAmountText;

    public int[] objectAmounts;
    public string[] objectNames;

    bool emptyVAPlayed = false;

    private void Start(){
        for (int i = 0; i < objectNames.Length; i++)
        {
            objectAmount.Add(objectNames[i], objectAmounts[i]);

            string buttonPrefabName = objectNames[i] + "Button";
            Debug.Log("Attempting to load object: " + buttonPrefabName);
            var loadedButton = Resources.Load<GameObject>("Prefabs/" + buttonPrefabName);
            var instantiatedButton = Instantiate(loadedButton);
            instantiatedButton.name = objectNames[i];

            ObjectSummon summonerObject = FindObjectOfType<ObjectSummon>();
            instantiatedButton.transform.parent = summonerObject.transform;

            Debug.Log(objectNames.Length + objectNames[i].ToString());
            string objectName = objectNames[i];
            instantiatedButton.GetComponent<Button>().onClick.AddListener(() =>  summonerObject.MakeObject(objectName));

            objectAmountText[i] = instantiatedButton.transform.GetChild(1).GetComponent<TMP_Text>();
        }
    }

    void Update(){
        print("Keys: " + string.Join(", ", objectAmount.Keys.ToArray()));
        print("Check");
        for (int i = 0; i < objectAmountText.Length; i++){
            objectAmountText[i].text = objectAmount[objectNames[i]].ToString();
        }


        if (objectAmount.Values.ToList().Sum() == 0 && !emptyVAPlayed)
        {
         //   JAM.PlayVoiceLine(JAMSounds.VoiceLineType.EmptyInventory);
            emptyVAPlayed = true;
        }
    }

    public void FindAndDecrement(string objectName){
        if (objectAmount.ContainsKey(objectName))
        {
            objectAmount[objectName]--;
        }
    }
    public void FindAndIncrement(string objectName){
        if (objectAmount.ContainsKey(objectName))
        {
            objectAmount[objectName]++;
        }
    }

    public int GetObjectAmount(string objectName){
        if (objectAmount.ContainsKey(objectName))
        {
            return objectAmount[objectName];
        }
        return 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorInventory : MonoBehaviour
{
    public Dictionary<string, int> objectAmount = new();
    [SerializeField] private Text[] objectAmountText;

    [SerializeField] private int[] objectAmounts;
    [SerializeField] private string[] objectNames;

    private void Start(){
        for (int i = 0; i < objectNames.Length; i++)
        {
            objectAmount.Add(objectNames[i], objectAmounts[i]);
        }
    }

    void Update(){
        foreach (var item in objectAmount)
        {
            objectAmountText[0].text = item.Value.ToString();
        }
    }

    public void FindAndDecrement(string objectName){
        if (objectAmount.ContainsKey(objectName))
        {
            objectAmount[objectName]--;
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

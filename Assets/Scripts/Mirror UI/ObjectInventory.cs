using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorInventory : MonoBehaviour
{
    public Dictionary<string, int> objectAmount = new();
    [SerializeField] private Text[] objectAmountText;

    private void Start(){
        objectAmount.Add("Mirror", 5);
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

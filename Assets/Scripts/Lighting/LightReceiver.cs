using UnityEngine;

public class LightReceiver : LightPowered
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPowered)
        {
            Debug.Log("IM POWERED UP RN");
        }
    }

    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}

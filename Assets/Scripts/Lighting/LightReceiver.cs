using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightReceiver : LightPowered
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonObject = new GameObject("ContinueButton");

        // Add UI components to the button object
        buttonObject.AddComponent<RectTransform>();
        Button button = buttonObject.AddComponent<Button>();

        // Create a text label for the button
        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform);  // Set the button as the parent
        Text text = textObject.AddComponent<Text>();
        text.text = "Click Me!";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 24;
        text.alignment = TextAnchor.MiddleCenter;

        // Make sure the text is properly positioned within the button
        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(160, 30);  // Same size as the button
        textRect.localPosition = Vector3.zero;

        // Add an OnClick event to the button
        button.onClick.AddListener(OnButtonClick);
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

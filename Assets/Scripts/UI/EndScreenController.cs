using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    static EndScreenController reference;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, rectTransform.anchorMin.y);
        rectTransform.anchorMax = new Vector2(1, rectTransform.anchorMax.y);
        reference = this;
        gameObject.SetActive(false);
    }

    public static void Show()
    {
        reference.gameObject.SetActive(true);
    }

    public void MainMenuPressed()
    {
        SceneControl.MainMenu();
    }
    public void RestartLevelPressed()
    {
        SceneControl.ReloadLevel();
    }

    public void NextLevelPressed() { 
        SceneControl.NextLevel(); 
    }
}

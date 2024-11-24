using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour
{
    public bool cutsceneFinished = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (cutsceneFinished)
        {
            Skip();
        }
    }

    public void Skip()
    {
        SceneControl.MainMenu();
    }
}

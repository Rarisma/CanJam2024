using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private static SceneControl _controller;
    static SceneControl controller {
        get
        {
            if (_controller == null)
            {
                Debug.Log("Scene Manager is not Initiated");
                return null;
            }
            return _controller;
        }
        set
        {
            _controller = value;
        }
    }

    public List<string> LevelOrder = new List<string>()
    {
        
    };

    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        DontDestroyOnLoad(gameObject);
        MainMenu();
    }

    public static void NextLevel()
    {
        LoadLevel(controller.currentLevel+1);
    }

    public static void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadLevel(int levelID)
    {
        controller.currentLevel = levelID;
        LoadScene(controller.LevelOrder[levelID]);
    }
    public static void ReloadLevel()
    {
        LoadLevel(controller.currentLevel);
    }
    public static void MainMenu()
    {
        LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        musicSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        Cutscene();
    }

    public static void Cutscene()
    {
        LoadScene("Opening");
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
        LoadJam();
        if (!controller.musicSource.isPlaying) controller.musicSource.Play();
    }
    public static void ReloadLevel()
    {
        LoadLevel(controller.currentLevel);
    }
    public static void MainMenu()
    {
        LoadScene("MainMenu");
    }
    public static void LoadJam()
    {
        SceneManager.LoadScene("JAM", LoadSceneMode.Additive);
    }
    public static int GetLevelID()
    {
        return controller.currentLevel;
    }
    public static string GetLevelName()
    {
        return controller.LevelOrder[controller.currentLevel];
    }
    public static void LevelEditor()
    {
        LoadScene("LevelEditor");
    }
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static int GetLevelCount()
    {
        return controller.LevelOrder.Count;
    }
}

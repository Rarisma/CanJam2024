using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform optionPanel;
    [SerializeField] private RectTransform playPanel;
    [SerializeField] private RectTransform backgroundPanel;
    [SerializeField] private RectTransform skyPanel;
    [SerializeField] private Image[] mainHoverImages;
    [SerializeField] private Image[] levelHoverImages;
    private RectTransform initialMainPos;
    private RectTransform initialOptionPos;
    private RectTransform initialPlayPos;
    private Image blackImage;
    private int levelToLoadNextLmaoOhio;
    void Awake()
    {
        blackPanel.SetActive(true);
        initialMainPos = mainPanel;
        initialOptionPos = optionPanel;
        initialPlayPos = playPanel;
        blackImage = blackPanel.GetComponent<Image>();
    }
    void Start()
    {
        foreach(var image in mainHoverImages)
        {
            Color invis = image.color;
            invis.a = 0.0f;
            image.color = invis;
        }
        FadeBlackOut(2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        skyPanel.position -= new Vector3(15, 0, 0) * Time.deltaTime;
    }

    void FadeBlackOut(float durationSeconds)
    {
        blackImage.DOFade(0.0f, durationSeconds).onComplete = FinishFadeOut;
    }

    void FinishFadeOut()
    {
        blackPanel.SetActive(false);
        foreach(var image in mainHoverImages)
        {
            image.DOFade(1.0f, 1.0f);
        }
    }

    void FadeBlackIn(float durationSeconds)
    {
        blackPanel.SetActive(true);
        blackImage.DOFade(1.0f, durationSeconds).onComplete = FinishFadeIn;
    }
    
    void FinishFadeIn()
    {
        SceneManager.LoadSceneAsync(levelToLoadNextLmaoOhio);
    }

    public void GoToOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
    }

    public void BackFromOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
    }

    public void GoToPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
    }

    public void BackFromPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
    }

    public void PlayLevel(int levelID)
    {
        levelToLoadNextLmaoOhio = levelID;
        FadeBlackIn(1.0f);
    }

    public void MainHoverEnter(int imageIndex)
    {
        mainHoverImages[imageIndex].DOColor(Color.yellow, 0.3f);
    }

    public void MainHoverExit(int imageIndex)
    {
        mainHoverImages[imageIndex].DOColor(Color.white, 0.3f);
    }
    public void LevelHoverEnter(int levelID)
    {
        levelHoverImages[levelID].DOColor(Color.yellow, 0.3f);
    }

    public void LevelHoverExit(int levelID)
    {
        levelHoverImages[levelID].DOColor(Color.white, 0.3f);
    }
}

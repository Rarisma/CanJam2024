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
    private RectTransform initialMainPos;
    private RectTransform initialOptionPos;
    private RectTransform initialPlayPos;
    private Image blackImage;
    private bool fading;
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
        FadeBlackOut(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeBlackOut(float durationSeconds)
    {
        fading = true;
        blackImage.DOFade(0.0f, durationSeconds).onComplete = FinishFadeOut;
    }

    void FinishFadeOut()
    {
        fading = false;
        blackPanel.SetActive(false);
    }

    void FadeBlackIn(float durationSeconds)
    {
        blackPanel.SetActive(true);
        blackImage.DOFade(1.0f, durationSeconds).onComplete = FinishFadeIn;
    }
    
    void FinishFadeIn()
    {
        fading = false;
        SceneManager.LoadSceneAsync(levelToLoadNextLmaoOhio);
    }

    public void GoToOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
    }

    public void BackFromOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
    }

    public void GoToPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
    }

    public void BackFromPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
    }

    public void PlayLevel(int levelID)
    {
        levelToLoadNextLmaoOhio = levelID + 1;
        FadeBlackIn(1.0f);
    }
}

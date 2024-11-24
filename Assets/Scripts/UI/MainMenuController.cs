using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private Image logo;
    [SerializeField] private AudioClip[] audioClips;
    private RectTransform initialMainPos;
    private RectTransform initialOptionPos;
    private RectTransform initialPlayPos;
    private AudioSource soundSource;
    private Image blackImage;
    private int levelToLoadNextLmaoOhio;
    void Awake()
    {
        blackPanel.SetActive(true);
        initialMainPos = mainPanel;
        initialOptionPos = optionPanel;
        initialPlayPos = playPanel;
        blackImage = blackPanel.GetComponent<Image>();
        soundSource = GetComponent<AudioSource>();
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
        Sequence sequence = DOTween.Sequence();
        foreach(var image in mainHoverImages)
        {
            Tween tween = image.DOFade(1.0f, 0.5f);
            tween.onPlay = PlayPopSound;
            sequence.Append(tween);
        }
        sequence.Play();
        logo.DOFade(1.0f, 2.0f);
    }


    void FadeBlackIn(float durationSeconds)
    {
        blackPanel.SetActive(true);
        blackImage.DOFade(1.0f, durationSeconds).onComplete = FinishFadeIn;
    }
    
    void FinishFadeIn()
    {
        SceneControl.LoadLevel(levelToLoadNextLmaoOhio);
    }

    public void GoToOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        PlayPopSound();
    }

    public void BackFromOptions()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        optionPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        PlayPopSound();
    }

    public void GoToPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(-1920, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        PlayPopSound();
    }

    public void BackFromPlay()
    {
        mainPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        backgroundPanel.DOAnchorPos(new Vector2(0, 0), 0.5f);
        playPanel.DOAnchorPos(new Vector2(1920, 0), 0.5f);
        PlayPopSound();
    }

    public void PlayLevel(int levelID)
    {
        PlayPopSound();
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

    public void ChangeSoundVolume()
    {
        soundMixer.SetFloat("SoundVol", soundSlider.value);
    }

    public void ChangeMusicVolume()
    {
        soundMixer.SetFloat("MusicVol", musicSlider.value);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    void PlaySoundEffect(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    void PlayPopSound()
    {
        PlaySoundEffect(audioClips[0]);
    }
}

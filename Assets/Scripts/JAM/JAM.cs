using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JAM : MonoBehaviour
{
    [SerializeField] private RectTransform topHead;
    [SerializeField] private RectTransform bottomHead;
    public AudioSource audioSource;
    public static JAM instance;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;
    private float clipLoudness;
    private float[] clipSampleData;
    private JAMSounds sounds;
    private Vector3 initPos;

    private float timeSinceLastVoiceline = 0.0f;
    private float idleVoicelineCountdown;

    // Start is called before the first frame update
    void Awake()
    {
        if (!audioSource) {
			Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
		}
		clipSampleData = new float[sampleDataLength];

        sounds = GetComponent<JAMSounds>();
        instance = this;
        print("instanced");
    }
    void Start()
    {
        initPos = bottomHead.localPosition;
        idleVoicelineCountdown = Random.Range(2, 20);
        PlayVoiceLine(JAMSounds.VoiceLineType.LevelStart);
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceLastVoiceline += Time.deltaTime;
        if (idleVoicelineCountdown != 0.0f && timeSinceLastVoiceline > 10.0f)
        {
            idleVoicelineCountdown -= Time.deltaTime;
            if (idleVoicelineCountdown < 0.0f) 
            {
                idleVoicelineCountdown = 0.0f;
                PlayVoiceLine(JAMSounds.VoiceLineType.Idle);
                idleVoicelineCountdown = Random.Range(2, 20);
            }
        }
        
        if (audioSource.clip == null) return;

        currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
			clipLoudness = 0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
		}

        bottomHead.localPosition = initPos - new Vector3(0, clipLoudness * 100, 0);


        Vector2 mousePosition = Input.mousePosition;
        Vector2 canvasSize = ((RectTransform)transform.parent).sizeDelta;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 ratios = new Vector2(canvasSize.x / screenSize.x, canvasSize.y / screenSize.y);

        Vector2 centeredMousePos = new Vector2(mousePosition.x - screenCenter.x, mousePosition.y - screenCenter.y);
        Vector2 corner = new Vector2(Mathf.Sign(centeredMousePos.x), Mathf.Sign(centeredMousePos.y));
        Vector2 canvasCenter = new Vector2(canvasSize.x / 2, canvasSize.y / 2);
        Vector2 targetPos = new Vector2(Globals.mod(corner.x * 200, (int)canvasSize.x) , Globals.mod(corner.y * 200, (int)canvasSize.y));

        print(corner);
        transform.position = Vector2.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
    }

    public static void PlayVoiceLine(JAMSounds.VoiceLineType type)
    {
        instance.audioSource.clip = instance.sounds.GetVoiceLine(type);
        instance.audioSource.Play();
        instance.timeSinceLastVoiceline = 0.0f;
    }
}

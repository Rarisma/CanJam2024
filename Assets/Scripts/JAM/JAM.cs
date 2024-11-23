using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JAM : MonoBehaviour
{
    [SerializeField] private RectTransform topHead;
    [SerializeField] private RectTransform bottomHead;
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;
    private float clipLoudness;
    private float[] clipSampleData;
    private JAMSounds sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (!audioSource) {
			Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
		}
		clipSampleData = new float[sampleDataLength];

        sounds = GetComponent<JAMSounds>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

        topHead.localPosition = new Vector3(0, -1 + clipLoudness * 300, 0);
    }
}

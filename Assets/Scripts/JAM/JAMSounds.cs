using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JAMSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] levelStartSounds;
    [SerializeField] private AudioClip[] smokePresentSounds;
    [SerializeField] private AudioClip[] levelCompleteSounds;
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] movingObjectSounds;
    [SerializeField] private AudioClip[] emptyInventorySounds;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum VoiceLineType
    {
        LevelStart,
        SmokePresent,
        LevelComplete,
        Idle,
        MovingObjects,
        EmptyInventory
    }

    public AudioClip GetVoiceLine(VoiceLineType type)
    {
        AudioClip clip;
        switch(type)
        {
            case VoiceLineType.LevelStart:
                clip = levelStartSounds[Random.Range(0, levelStartSounds.Length)];
                break;
            case VoiceLineType.SmokePresent:
                clip = smokePresentSounds[Random.Range(0, smokePresentSounds.Length)];
                break;
            case VoiceLineType.LevelComplete:
                clip = levelCompleteSounds[Random.Range(0, levelCompleteSounds.Length)];
                break;
            case VoiceLineType.Idle:
                clip = idleSounds[Random.Range(0, idleSounds.Length)];
                break;
            case VoiceLineType.MovingObjects:
                clip = movingObjectSounds[Random.Range(0, movingObjectSounds.Length)];
                break;
            case VoiceLineType.EmptyInventory:
                clip = emptyInventorySounds[Random.Range(0, emptyInventorySounds.Length)];
                break;
            default:
                clip = idleSounds[Random.Range(0, idleSounds.Length)];
                break;
        }

        return clip;
    }
}

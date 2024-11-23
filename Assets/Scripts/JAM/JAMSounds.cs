using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JAMSounds : MonoBehaviour
{
    // TODO: dont repeat voice lines until all have been played
    [SerializeField] private AudioClip[] levelStartSounds;
    private List<int> levelStartUsed;
    [SerializeField] private AudioClip[] smokePresentSounds;
    private List<int> smokePresentUsed;
    [SerializeField] private AudioClip[] levelCompleteSounds;
    private List<int> levelCompleteUsed;
    [SerializeField] private AudioClip[] idleSounds;
    private List<int> idleUsed;
    [SerializeField] private AudioClip[] movingObjectSounds;
    private List<int> movingObjectUsed;
    [SerializeField] private AudioClip[] emptyInventorySounds;
    private List<int> emptyInventoryUsed;

    void Start()
    {
        levelStartUsed = Enumerable.Range(0, levelStartSounds.Length).ToList();
        smokePresentUsed = Enumerable.Range(0, smokePresentSounds.Length).ToList();
        levelCompleteUsed = Enumerable.Range(0, levelCompleteSounds.Length).ToList();
        idleUsed = Enumerable.Range(0, idleSounds.Length).ToList();
        movingObjectUsed = Enumerable.Range(0, movingObjectSounds.Length).ToList();
        emptyInventoryUsed = Enumerable.Range(0, emptyInventorySounds.Length).ToList();
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
                if (levelStartUsed.Count == 0) levelStartUsed = Enumerable.Range(0, levelStartSounds.Length).ToList();
                clip = levelStartSounds[levelStartUsed[Random.Range(0, levelStartUsed.Count)]];
                break;
            case VoiceLineType.SmokePresent:
                if (smokePresentUsed.Count == 0) smokePresentUsed = Enumerable.Range(0, smokePresentSounds.Length).ToList();
                clip = smokePresentSounds[smokePresentUsed[Random.Range(0, smokePresentUsed.Count)]];
                break;
            case VoiceLineType.LevelComplete:
                if (levelCompleteUsed.Count == 0) levelCompleteUsed = Enumerable.Range(0, levelCompleteSounds.Length).ToList();
                clip = levelCompleteSounds[levelCompleteUsed[Random.Range(0, levelCompleteUsed.Count)]];
                break;
            case VoiceLineType.Idle:
                if (idleUsed.Count == 0) idleUsed = Enumerable.Range(0, idleSounds.Length).ToList();
                clip = idleSounds[idleUsed[Random.Range(0, idleUsed.Count)]];
                break;
            case VoiceLineType.MovingObjects:
                if (movingObjectUsed.Count == 0) movingObjectUsed = Enumerable.Range(0, movingObjectSounds.Length).ToList();
                clip = movingObjectSounds[movingObjectUsed[Random.Range(0, movingObjectUsed.Count)]];
                break;
            case VoiceLineType.EmptyInventory:
                if (emptyInventoryUsed.Count == 0) emptyInventoryUsed = Enumerable.Range(0, emptyInventorySounds.Length).ToList();
                clip = emptyInventorySounds[emptyInventoryUsed[Random.Range(0, emptyInventoryUsed.Count)]];
                break;
            default:
                if (idleUsed.Count == 0) idleUsed = Enumerable.Range(0, idleSounds.Length).ToList();
                clip = idleSounds[idleUsed[Random.Range(0, idleUsed.Count)]];
                break;
        }

        return clip;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JAMSounds : MonoBehaviour
{
    // TODO: dont repeat voice lines until all have been played
     private AudioClip[] levelStartSounds;
    private static List<int> levelStartUsed;
     private AudioClip[] smokePresentSounds;
    private static List<int> smokePresentUsed;
     private AudioClip[] levelCompleteSounds;
    private static List<int> levelCompleteUsed;
     private AudioClip[] idleSounds;
    private static List<int> idleUsed;
    private AudioClip[] movingObjectSounds;
    private static List<int> movingObjectUsed;
    private AudioClip[] emptyInventorySounds;
    private static List<int> emptyInventoryUsed;

    void Start()
    {
        LoadSounds();
        if (levelStartUsed != null) return;
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

    public void LoadSounds()
    {
        levelStartSounds = LoadSounds("levelstart");
        smokePresentSounds = LoadSounds("smokepresent");
        levelCompleteSounds = LoadSounds("levelcomplete");
        idleSounds = LoadSounds("idle");
        movingObjectSounds = LoadSounds("movingobjects").Concat(LoadSounds("emptyinventory")).ToArray();
        emptyInventorySounds = LoadSounds("emptyinventory");
    }

    public AudioClip[] LoadSounds(string folderName)
    {
        var sound = Resources.LoadAll<AudioClip>("Sounds/VoiceLines/" + folderName);
        return sound.ToArray();
    }

    public AudioClip GetVoiceLine(VoiceLineType type)
    {
        AudioClip clip;
        int randomIndex;
        switch(type)
        {
            case VoiceLineType.LevelStart:
                if (levelStartUsed.Count == 0) levelStartUsed = Enumerable.Range(0, levelStartSounds.Length).ToList();
                randomIndex = Random.Range(0, levelStartUsed.Count);
                clip = levelStartSounds[levelStartUsed[randomIndex]];
                levelStartUsed.RemoveAt(randomIndex);
                break;
            case VoiceLineType.SmokePresent:
                if (smokePresentUsed.Count == 0) smokePresentUsed = Enumerable.Range(0, smokePresentSounds.Length).ToList();
                randomIndex = Random.Range(0, smokePresentUsed.Count);
                clip = smokePresentSounds[smokePresentUsed[randomIndex]];
                smokePresentUsed.RemoveAt(randomIndex);
                break;
            case VoiceLineType.LevelComplete:
                if (levelCompleteUsed.Count == 0) levelCompleteUsed = Enumerable.Range(0, levelCompleteSounds.Length).ToList();
                randomIndex = Random.Range(0, levelCompleteUsed.Count);
                clip = levelCompleteSounds[levelCompleteUsed[randomIndex]];
                levelCompleteUsed.RemoveAt(randomIndex);
                break;
            case VoiceLineType.Idle:
                if (idleUsed.Count == 0) idleUsed = Enumerable.Range(0, idleSounds.Length).ToList();
                randomIndex = Random.Range(0, idleUsed.Count);
                clip = idleSounds[idleUsed[randomIndex]];
                idleUsed.RemoveAt(randomIndex);
                break;
            case VoiceLineType.MovingObjects:
                if (movingObjectUsed.Count == 0) movingObjectUsed = Enumerable.Range(0, movingObjectSounds.Length).ToList();
                randomIndex = Random.Range(0, movingObjectUsed.Count);
                clip = movingObjectSounds[movingObjectUsed[randomIndex]];
                movingObjectUsed.RemoveAt(randomIndex);
                break;
            case VoiceLineType.EmptyInventory:
                if (emptyInventoryUsed.Count == 0) emptyInventoryUsed = Enumerable.Range(0, emptyInventorySounds.Length).ToList();
                randomIndex = Random.Range(0, emptyInventoryUsed.Count);
                clip = emptyInventorySounds[emptyInventoryUsed[randomIndex]];
                emptyInventoryUsed.RemoveAt(randomIndex);
                break;
            default:
                if (idleUsed.Count == 0) idleUsed = Enumerable.Range(0, idleSounds.Length).ToList();
                randomIndex = Random.Range(0, idleUsed.Count);
                clip = idleSounds[idleUsed[randomIndex]];
                idleUsed.RemoveAt(randomIndex);
                break;
        }

        return clip;
    }
}

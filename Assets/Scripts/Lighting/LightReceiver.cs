using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightReceiver : LightPowered
{
    bool leaderReceiver = true;
    List<LightReceiver> lightReceivers = null;
    int lightReceiverID = 0;

    Coroutine coroutine = null;

    // Start is called before the first frame update
    public override void Start()
    {
        // Only one light receiver will do the checks each frame for the level being over (all light receivers being powered)                                                                                                               
        // This script will go through each light receiver in the game and if it has a lower instance id, it cannot be the leader
        // This means the light receiver with the smallest instance ID will do the checking each frame
        lightReceivers = new(FindObjectsOfType<LightReceiver>());

        lightReceivers.OrderBy(x => x.gameObject.GetInstanceID()).ToList();
        lightReceiverID = lightReceivers.IndexOf(this);
        leaderReceiver = lightReceiverID == 0;

        float hue_step = 360 / lightReceivers.Count;
        GetComponent<SpriteRenderer>().material.SetFloat("_shift", hue_step * lightReceiverID);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (leaderReceiver)
        {
            if (CheckForEndOfLevel())
            {
                if (coroutine == null)
                {
                    print("End Level");
                    coroutine = StartCoroutine(EndLevel());
                }
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = null;
            }
        }

        GetComponent<Animator>().SetBool("isPowered", isPowered);
    }

    public override void OnPowerDown()
    {
        base.OnPowerDown();
        GetComponentInChildren<ParticleSystem>().Play();
    }

    bool CheckForEndOfLevel()
    {
        bool finishLevel = true;
        foreach (LightReceiver lightReceiver in lightReceivers)
        {
            if (lightReceiver.isPowered == false)
            {
                finishLevel = false;
                break;
            }
        }

        return finishLevel;
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1.25f);
        // TODO: Implement
        EndScreenController.Show();
        JAM.LevelComplete();
    }

    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}

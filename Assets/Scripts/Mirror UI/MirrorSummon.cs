using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSummon : MonoBehaviour
{
    [SerializeField] private GameObject mirrorPrefab;


    public void MakeMirror()
    {
        var spawnedMirror = Instantiate(mirrorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedMirror.name = "Mirror";
    }
}

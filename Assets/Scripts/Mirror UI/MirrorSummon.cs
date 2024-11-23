using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MirrorSummon : MonoBehaviour
{
    [SerializeField] private GameObject mirrorPrefab;
    [SerializeField] private GameObject freeMirrorPrefab;
    [SerializeField] private GameObject splitterPrefab;
    [SerializeField] private ObjectInventory objectInventory;


    public void MakeMirror(GameObject mirrorButton)
    {        
        print("Mirror created");

        mirrorButton.transform.localScale = new Vector3(1, 1, 1);
        mirrorButton.transform.DOKill();

        if (objectInventory.GetObjectAmount("Mirror") <= 0) { return; }
        objectInventory.FindAndDecrement("Mirror");

        Vector3 FindEmptyPosition(Vector3 startPosition)
        {
            Vector3 position = startPosition;
            while (IsPositionOccupied(position))
            {
                position = GetNextPosition(position);
            }
            return position;
        }

        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 emptyPosition = FindEmptyPosition(startPosition);

        mirrorButton.transform.DOKill();
        mirrorButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);

        var spawnedMirror = Instantiate(mirrorPrefab, emptyPosition, Quaternion.identity);
        spawnedMirror.name = "Mirror";
        spawnedMirror.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
    }

    public void MakeSplitter(GameObject splitterButton)
    {        
        print("Splitter created");

        splitterButton.transform.localScale = new Vector3(1, 1, 1);
        splitterButton.transform.DOKill();
        
        if (objectInventory.GetObjectAmount("Splitter") <= 0) { return; }
        objectInventory.FindAndDecrement("Splitter");

        Vector3 FindEmptyPosition(Vector3 startPosition)
        {
            Vector3 position = startPosition;
            while (IsPositionOccupied(position))
            {
                position = GetNextPosition(position);
            }
            return position;
        }

        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 emptyPosition = FindEmptyPosition(startPosition);

        
        splitterButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);

        var spawnedSplitter = Instantiate(splitterPrefab, emptyPosition, Quaternion.identity);
        spawnedSplitter.name = "Splitter";
        spawnedSplitter.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
    }
    public void MakeFreeMirror(GameObject freeMirrorObject)
    {        
        print("FreeMirror created");

        if (objectInventory.GetObjectAmount("FreeMirror") <= 0) { return; }
        objectInventory.FindAndDecrement("FreeMirror");

        Vector3 FindEmptyPosition(Vector3 startPosition)
        {
            Vector3 position = startPosition;
            while (IsPositionOccupied(position))
            {
                position = GetNextPosition(position);
            }
            return position;
        }

        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 emptyPosition = FindEmptyPosition(startPosition);

        freeMirrorObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);

        var spawnedSplitter = Instantiate(freeMirrorPrefab, emptyPosition, Quaternion.identity);
        spawnedSplitter.name = "FreeMirror";
        spawnedSplitter.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
    }

    Vector3 GetNextPosition(Vector3 currentPosition)
    {
        // Define the next position logic, move to the right by 1 unit
        // If x is greater than 15, reset x to 0 and increment y by 1
        if (currentPosition.x > 14)
        {
            return new Vector3(0, currentPosition.y + 1, currentPosition.z);
        }
        return new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        // Check if the position is occupied by any other object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        return colliders.Length > 0;
    }

}

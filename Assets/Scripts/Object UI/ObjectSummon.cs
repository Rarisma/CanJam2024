using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSummon : MonoBehaviour
{
    [SerializeField] private ObjectInventory objectInventory;


    public void MakeObject(string objectName)
    {        
        if (objectInventory.GetObjectAmount(objectName) <= 0) { return; }
        objectInventory.FindAndDecrement(objectName);

        Debug.Log("Attempting to load object: " + objectName);
        var spawnedObject = Resources.Load<GameObject>("Prefabs/" + objectName);

    
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

        var spawnedMirror = Instantiate(spawnedObject, emptyPosition, Quaternion.identity);
        spawnedMirror.name = objectName;
        spawnedMirror.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);

        


/*
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
        spawnedMirror.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f); */
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

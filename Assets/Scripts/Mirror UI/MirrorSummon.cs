using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSummon : MonoBehaviour
{
    [SerializeField] private GameObject mirrorPrefab;
    [SerializeField] private MirrorInventory mirrorInventory;


    public void MakeMirror()
    {

        if (mirrorInventory.GetObjectAmount("Mirror") <= 0) { return; }
        mirrorInventory.FindAndDecrement("Mirror");

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

        var spawnedMirror = Instantiate(mirrorPrefab, emptyPosition, Quaternion.identity);
        spawnedMirror.name = "Mirror";
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

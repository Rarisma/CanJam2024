using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSummon : MonoBehaviour
{
    [SerializeField] private GameObject mirrorPrefab;


    public void MakeMirror()
    {
        
        Vector3 FindEmptyPosition(Vector3 startPosition)
        {
            Vector3 position = startPosition;
            while (IsPositionOccupied(position))
            {
                position = GetNextPosition(position);
            }
            return position;
        }

        Vector3 startPosition = new Vector3(8, 4, 0);
        Vector3 emptyPosition = FindEmptyPosition(startPosition);

        var spawnedMirror = Instantiate(mirrorPrefab, emptyPosition, Quaternion.identity);
        spawnedMirror.name = "Mirror";
    }

    Vector3 GetNextPosition(Vector3 currentPosition)
    {
        // Define the next position logic, for example, move to the right by 1 unit
        return new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        // Check if the position is occupied by any other object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        return colliders.Length > 0;
    }

}

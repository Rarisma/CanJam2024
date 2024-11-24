using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    [SerializeField] private Tile tilePrefab;

    [SerializeField] public Transform cam;

    private void Start()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Dirt BG"), new Vector3(0, 0, 5), Quaternion.identity);
        GenerateGrid();
            Camera.main.orthographicSize = height / 2 + 2f;
    }

    void GenerateGrid(){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 1), Quaternion.identity);
                spawnedTile.name = $"Tile ({x}, {y})";
                spawnedTile.transform.parent = transform;

                var isOffset = (x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0);
                spawnedTile.Init(isOffset);
            }
        }

        cam.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 1.4f, -10);
    }
}

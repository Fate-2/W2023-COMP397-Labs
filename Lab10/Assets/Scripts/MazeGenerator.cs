using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Size")]
    public int width;
    public int length;
    public List<GameObject> tilePrefabs;
    public List<GameObject> activeTiles;


    public void GenerateMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                var randomTileIndex = Random.Range(0, tilePrefabs.Count);
                var randomTilePosition = new Vector3(i * 16, 0.0f, j * 16);
                var randomTileRotation = Random.Range(0, 4) * 90.0f;
                var randomTile = Instantiate(tilePrefabs[randomTileIndex],
                randomTilePosition, Quaternion.Euler(
                tilePrefabs[randomTileIndex].transform.rotation.x,
                randomTileRotation,
                tilePrefabs[randomTileIndex].transform.rotation.y));
                randomTile.transform.parent = transform;
                activeTiles.Add(randomTile);
            }
        }

    }

}

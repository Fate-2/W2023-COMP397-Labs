using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotSpawner : MonoBehaviour
{
    public GameObject robotPrefab;
    public int robotSpawnChance = 0;

    public void SpawnRobots(List<GameObject> activemazeTiles, Difficulty difficulty)

    {
        robotSpawnChance = difficulty switch
        {
            Difficulty.Easy => 25,
            Difficulty.Normal => 50,
            Difficulty.Hard => 75,
            Difficulty.veryHard => 100,
            _ => throw new ArgumentOutOfRangeException()

        };
       
        for (int i = 0; i < activemazeTiles.Count; i++)
        {
            var robotSpawn = activemazeTiles[i].transform.GetChild(
                activemazeTiles[i].transform.childCount - 1);
            var randomChance = Random.Range(0, 101);
            if (robotSpawn.gameObject.tag.Equals("EnemySpawn") && robotSpawnChance >= randomChance)
            {
                var robot = Instantiate(robotPrefab,
              robotSpawn.position, Quaternion.identity);
                robot.transform.parent = this.transform;
            } 
        }
    }
}

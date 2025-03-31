using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string WaveName;               // Name or identifier of the wave
    public float StartDelay;              // Time before this wave starts
    public List<EnemySpawnData> Enemies;  // List of enemies to spawn

    public WaveData(string waveName, float startDelay, List<EnemySpawnData> enemies)
    {
        WaveName = waveName;
        StartDelay = startDelay;
        Enemies = enemies;
    }
}

[System.Serializable]
public class EnemySpawnData
{
    public GameObject Enemy;      // The enemy to spawn
    public int SpawnCount;        // Number of times this enemy should be spawned
    public float SpawnInterval;   // Interval between each spawn

    public EnemySpawnData(GameObject enemy, int spawnCount, float spawnInterval)
    {
        Enemy = enemy;
        SpawnCount = spawnCount;
        SpawnInterval = spawnInterval;
    }
}
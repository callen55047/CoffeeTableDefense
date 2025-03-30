using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public int spawnCount = 10;
        public float spawnInterval = 1f;
        public GameObject enemyPrefab;
        
        private Transform spawnPoint;
        private int enemeiesSpawned = 0;

        void Start()
        {
            // fetch spawn point from current map
            
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (enemeiesSpawned < spawnCount)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemeiesSpawned++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
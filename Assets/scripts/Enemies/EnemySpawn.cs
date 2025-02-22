using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform spawnPoint;
        public float spawnInterval = 1f;
        public GameObject enemyPrefab;

        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
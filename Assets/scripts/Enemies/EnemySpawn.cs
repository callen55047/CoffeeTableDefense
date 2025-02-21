using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform spawnPoint;
        public float spawnInterval = 1f;
        public GameObject enemyPrefab;
        public Transform pathsParent;

        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
                if (enemyMovement != null && pathsParent.childCount > 0)
                {
                    int randomIndex = Random.Range(0, pathsParent.childCount);
                    enemyMovement.pathParent = pathsParent.GetChild(randomIndex);
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}

using RPG.Attributes;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] float spawnRadius = 10f;
        [SerializeField] float respawnTime = 10f;
        [SerializeField] float destroyTime = 5f;
        [SerializeField] CombatTarget enemyToSpawn = null;

        const int ATTEMPTS = 30;

        private void Awake()
        {
            if (enemyToSpawn != null) 
            {
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            StartCoroutine(spawnEnemy(respawnTime, enemyToSpawn.gameObject));
        }

        IEnumerator spawnEnemy(float time, GameObject enemy)
        {
            yield return new WaitForSeconds(time);
            Vector3 spawnLocation = GetSpawnLocation();
            GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);
            Health enemyHealth = newEnemy.GetComponent<Health>();
            newEnemy.gameObject.GetComponent<NavMeshAgent>().Warp(spawnLocation);
            while (!enemyHealth.IsDead())
            {
                yield return null;
            }
            Destroy(newEnemy, destroyTime);
            SpawnEnemy();
        }

        private Vector3 GetSpawnLocation()
        {
            for (int i = 0; i < ATTEMPTS; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}

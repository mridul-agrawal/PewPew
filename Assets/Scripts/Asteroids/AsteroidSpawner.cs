using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewPew.Asteroids
{
    /// <summary>
    /// This class is responsible for Spawning Asteroids in the game.
    /// </summary>
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField]
        private Asteroid AsteroidPrefab;
        public float SpawnRate = 2.0f;
        public int SpawnAmount = 1;
        public float SpawnDistance = 12.0f;
        public float TrajectoryVariance = 15.0f;


        private void Start()
        {
            StartCoroutine(KeepSpawningAsteroids());
        }

        // A Coroutine to Spawn asteroids at certain time intervals.
        IEnumerator KeepSpawningAsteroids()
        {
            while (true)
            {
                yield return new WaitForSeconds(SpawnRate);
                SpawnAsteroids();
            }
        }

        // Method to spawn an asteroid and give it a direction.
        private void SpawnAsteroids()
        {
            for (int i = 0; i < SpawnAmount; i++)
            {
                Vector3 spawnPositionOffset = Random.insideUnitCircle.normalized * SpawnDistance;
                Vector3 spawnPosition = transform.position + spawnPositionOffset;

                float variance = Random.Range(-TrajectoryVariance, TrajectoryVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Asteroid asteroid = Instantiate(AsteroidPrefab, spawnPosition, rotation);
                asteroid.SetTrajectory(rotation * -spawnPositionOffset);
            }
        }
    }
}
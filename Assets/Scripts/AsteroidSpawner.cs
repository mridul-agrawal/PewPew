using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid AsteroidPrefab;
    public float SpawnRate = 2.0f;
    public int SpawnAmount = 1;
    public float SpawnDistance = 12.0f;
    public float TrajectoryVariance = 15.0f;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroids), SpawnRate, SpawnRate);
    }

    private void SpawnAsteroids()
    {
        for(int i=0; i<SpawnAmount; i++)
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

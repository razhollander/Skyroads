using UnityEngine;

public interface IAsteroidsModule
{
    void LoadData();
    void SpawnAsteroid(Vector3 spawnPosition, float xPositionRange);
    void StartSpawning();
    void ResetTimeForNextSpawn();
}
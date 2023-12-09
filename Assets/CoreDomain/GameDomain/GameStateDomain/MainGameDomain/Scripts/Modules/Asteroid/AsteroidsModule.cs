using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using CoreDomain.Utils.Pools;
using UnityEngine;

public class AsteroidsModule: IUpdatable, IAsteroidsModule
{
    private readonly SpawnAsteroidCommand.Factory _spawnAsteroidCommand;
    private AsteroidsCreator _asteroidsCreator;
    private AsteroidsSpawnRateData _asteroidsSpawnRateData;
    private float _secondsUntilNextSpawn;
    
    public AsteroidsModule(AsteroidsPool.Factory asteroidsPool, IAssetBundleLoaderService assetBundleLoaderService, SpawnAsteroidCommand.Factory spawnAsteroidCommand)
    {
        _spawnAsteroidCommand = spawnAsteroidCommand;
        _asteroidsCreator = new AsteroidsCreator(asteroidsPool, assetBundleLoaderService);
    }

    public void LoadData()
    {
        _asteroidsSpawnRateData = _asteroidsCreator.LoadAsteroidsSpawnRateData();
        _secondsUntilNextSpawn = 0;
    }

    public void ManagedUpdate()
    {
        _secondsUntilNextSpawn -= Time.deltaTime;

        if (_secondsUntilNextSpawn <= 0)
        {
            _spawnAsteroidCommand.Create().Execute();
        }
    }

    public void SpawnAsteroid(Vector3 spawnPosition)
    {
        var asteroid = _asteroidsCreator.CreateAsteroid();
        asteroid.transform.position = spawnPosition;
    }
}
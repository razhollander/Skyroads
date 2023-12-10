using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using CoreDomain.Utils.Pools;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class AsteroidsModule: IUpdatable, IAsteroidsModule
{
    private readonly SpawnAsteroidCommand.Factory _spawnAsteroidCommand;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private AsteroidsCreator _asteroidsCreator;
    private AsteroidsSpawnRateData _asteroidsSpawnRateData;
    private float _secondsUntilNextSpawn;
    private float _secondsPassedSinceStartedSpawning = 0;
    private AsteroidsViewModule _asteroidsViewModule;
    
    public AsteroidsModule(AsteroidsPool.Factory asteroidsPool, IAssetBundleLoaderService assetBundleLoaderService, SpawnAsteroidCommand.Factory spawnAsteroidCommand, IUpdateSubscriptionService updateSubscriptionService, IGameSpeedService gameSpeedService)
    {
        _spawnAsteroidCommand = spawnAsteroidCommand;
        _updateSubscriptionService = updateSubscriptionService;
        _asteroidsCreator = new AsteroidsCreator(asteroidsPool, assetBundleLoaderService);
        _asteroidsViewModule = new AsteroidsViewModule(gameSpeedService, updateSubscriptionService);
    }

    public void LoadData()
    {
        _asteroidsSpawnRateData = _asteroidsCreator.LoadAsteroidsSpawnRateData();
        _secondsUntilNextSpawn = 0;
    }

    public void StartSpawning()
    {
        AddListeners();
        _asteroidsViewModule.StartMovingAsteroids();
    }

    public void ResetTimeForNextSpawn()
    {
        _secondsUntilNextSpawn = Mathf.Max(math.remap(0, _asteroidsSpawnRateData.ReachFinalSpawnRateAfterSeconds, 
            _asteroidsSpawnRateData.InitialSpawnRateInSeconds, _asteroidsSpawnRateData.FinalSpawnRateInSeconds,
            _secondsPassedSinceStartedSpawning), _asteroidsSpawnRateData.FinalSpawnRateInSeconds);
        Debug.Log($"Next spawn in: {_secondsUntilNextSpawn}");
    }

    private void AddListeners()
    {
        _updateSubscriptionService.RegisterUpdatable(this);
    }
    
    private void RemoveListeners()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }

    public void PauseAsteroids()
    {
        RemoveListeners();
        _asteroidsViewModule.StopMovingAsteroids();
    }
    
    public void Dispose()
    {
        RemoveListeners();
        _asteroidsViewModule.StopMovingAsteroids();
    }
    
    public void ManagedUpdate()
    {
        _secondsUntilNextSpawn -= Time.deltaTime;
        _secondsPassedSinceStartedSpawning += Time.deltaTime;
        
        if (_secondsUntilNextSpawn <= 0)
        {
            _spawnAsteroidCommand.Create().Execute();
        }
    }

    public void SpawnAsteroid(Vector3 spawnPosition, float xPositionRange)
    {
        var asteroid = _asteroidsCreator.CreateAsteroid();
        var xPosition = Random.Range(spawnPosition.x - xPositionRange, spawnPosition.x + xPositionRange);
        asteroid.transform.position = new Vector3(xPosition, spawnPosition.y + asteroid.RendererHeight * 0.5f, spawnPosition.z);
        asteroid.gameObject.SetActive(true);
        
        _asteroidsViewModule.AddAsteroid(asteroid);
    }
}
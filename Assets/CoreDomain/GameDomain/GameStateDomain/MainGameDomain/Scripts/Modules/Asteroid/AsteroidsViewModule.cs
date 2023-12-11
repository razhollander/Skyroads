using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;
using System;
public class AsteroidsViewModule : IUpdatable
{
    private const int GapToDespawnAsteroids = 10;
    private float _zPositionDespawnAsteroids;
    private readonly IGameSpeedService _gameSpeedService;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly AsteroidPassedPlayerCommand.Factory _asteroidPassedPlayerCommand;
    private List<AsteroidView> _asteroids = new List<AsteroidView>();
    private HashSet<string> _asteroidsIdsPassedPlayer = new HashSet<string>();
    private float _asteroidsPassedZPosition;

    public AsteroidsViewModule(
        IGameSpeedService gameSpeedService,
        IUpdateSubscriptionService updateSubscriptionService,
        AsteroidPassedPlayerCommand.Factory asteroidPassedPlayerCommand)
    {
        _gameSpeedService = gameSpeedService;
        _updateSubscriptionService = updateSubscriptionService;
        _asteroidPassedPlayerCommand = asteroidPassedPlayerCommand;
    }

    public void StartMovingAsteroids()
    {
        _updateSubscriptionService.RegisterUpdatable(this);
    }
    
    public void StopMovingAsteroids()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }
    
    public void AddAsteroid(AsteroidView asteroid)
    {
        _asteroids.Add(asteroid);
    }

    public void ManagedUpdate()
    {
        for (int i = _asteroids.Count - 1; i >= 0; i--)
        {
            var currentAsteroid = _asteroids[i];
            var asteroidTransform = currentAsteroid.transform;
            
            MoveAsteroid(asteroidTransform);

            float asteroidZPosition = asteroidTransform.position.z;
            
            HandleIfAsteroidPassedPlayer(asteroidZPosition, currentAsteroid.ID);
            HandleIfShouldDespawnAsteroid(asteroidZPosition, currentAsteroid);
        }
    }

    private void MoveAsteroid(Transform asteroidTransform)
    {
        asteroidTransform.Translate(0, 0, -_gameSpeedService.CurrentGameSpeed * Time.deltaTime);
    }

    private void HandleIfAsteroidPassedPlayer(float asteroidZPosition, string asteroidID)
    {
        if (asteroidZPosition < _asteroidsPassedZPosition && !_asteroidsIdsPassedPlayer.Contains(asteroidID))
        {
            _asteroidPassedPlayerCommand.Create(new AsteroidPassedPlayerCommandData(asteroidID)).Execute();
        }
    }

    public void SetAsteroidPassedPlayer(string asteroidID)
    {
        _asteroidsIdsPassedPlayer.Add(asteroidID);
    }

    private void HandleIfShouldDespawnAsteroid(float asteroidZPosition, AsteroidView asteroid)
    {
        if (asteroidZPosition < _zPositionDespawnAsteroids)
        {
            RemoveAsteroid(asteroid);
            asteroid.Despawn();
        }
    }

    public void SetAsteroidsPassedZPosition(float zPosition)
    {
        _asteroidsPassedZPosition = zPosition;
        _zPositionDespawnAsteroids = _asteroidsPassedZPosition - GapToDespawnAsteroids;
    }

    public void RemoveAsteroid(AsteroidView asteroid)
    {
        _asteroids.Remove(asteroid);

        if (_asteroidsIdsPassedPlayer.Contains(asteroid.ID))
            _asteroidsIdsPassedPlayer.Remove(asteroid.ID);
    }
}

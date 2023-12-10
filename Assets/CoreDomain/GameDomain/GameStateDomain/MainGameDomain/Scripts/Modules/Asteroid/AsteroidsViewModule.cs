using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;

public class AsteroidsViewModule : IUpdatable
{
    private readonly IGameSpeedService _gameSpeedService;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private List<AsteroidView> _asteroids = new List<AsteroidView>();

    public AsteroidsViewModule(IGameSpeedService gameSpeedService, IUpdateSubscriptionService updateSubscriptionService)
    {
        _gameSpeedService = gameSpeedService;
        _updateSubscriptionService = updateSubscriptionService;
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
            _asteroids[i].transform.Translate(0,0,-_gameSpeedService.CurrentGameSpeed * Time.deltaTime);
        }
    }
}

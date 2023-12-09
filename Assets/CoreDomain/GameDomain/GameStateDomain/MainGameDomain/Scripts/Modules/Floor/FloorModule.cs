using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;

public class FloorModule : IFloorModule, IUpdatable
{
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly IGameSpeedService _gameSpeedService;
    private readonly FloorCreator _floorCreator;
    private FloorView _floorView;
    private float FloorMovementSpeed => _gameSpeedService.CurrentGameSpeed;
    
    public FloorModule(IAssetBundleLoaderService assetBundleLoaderService, IUpdateSubscriptionService updateSubscriptionService, IGameSpeedService gameSpeedService)
    {
        _updateSubscriptionService = updateSubscriptionService;
        _gameSpeedService = gameSpeedService;
        _floorCreator = new FloorCreator(assetBundleLoaderService);
    }

    public void StartMovement()
    {
        _updateSubscriptionService.RegisterUpdatable(this);
    }
    
    public void StopMovement()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }
    
    public void CreateFloor()
    {
        _floorView = _floorCreator.CreateFloor();
    }

    public void ManagedUpdate()
    {
        _floorView.ChangeOffset(FloorMovementSpeed * Time.deltaTime);
    }
}

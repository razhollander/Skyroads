using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;

public class FloorModule : IFloorModule, IUpdatable
{
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly FloorCreator _floorCreator;
    private FloorView _floorView;
    private float _floorMovementSpeed = 0;

    public FloorModule(IAssetBundleLoaderService assetBundleLoaderService, IUpdateSubscriptionService updateSubscriptionService)
    {
        _updateSubscriptionService = updateSubscriptionService;
        _floorCreator = new FloorCreator(assetBundleLoaderService);
    }

    public void StartMovement(float speed)
    {
        ChangeFloorMovementSpeed(speed);
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

    public void ChangeFloorMovementSpeed(float speed)
    {
        _floorMovementSpeed = speed;
    }

    public void ManagedUpdate()
    {
        _floorView.ChangeOffset(_floorMovementSpeed * Time.deltaTime);
    }
}

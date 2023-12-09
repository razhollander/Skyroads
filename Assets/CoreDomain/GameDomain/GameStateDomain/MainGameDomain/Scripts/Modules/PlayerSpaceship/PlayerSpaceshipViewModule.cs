using System;
using CoreDomain.Scripts.Extensions;
using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipViewModule: IUpdatable
    {
        private static readonly Vector2 RelativeToScreenCenterStartPosition = new (0f, -0.5f);
        
        private readonly IDeviceScreenService _deviceScreenService;
        private PlayerSpaceshipView _playerSpaceshipView;
        private readonly Vector3 _screenBoundsInWorldSpace;
        private float _playerSpaceFromBounds;
        private float _spaceshipDestRotation = 0;
        private float _spaceshipMaxRotationAngle = 20;
        private readonly IUpdateSubscriptionService _updateSubscriptionService;

        public PlayerSpaceshipViewModule(IDeviceScreenService deviceScreenService, IUpdateSubscriptionService updateSubscriptionService)
        {
            _deviceScreenService = deviceScreenService;
            _screenBoundsInWorldSpace = deviceScreenService.ScreenBoundsInWorldSpace;
            _updateSubscriptionService = updateSubscriptionService;
        }
        
        public void Setup(PlayerSpaceshipView playerSpaceshipView)
        {
            _playerSpaceshipView = playerSpaceshipView;

            var startPosition = _screenBoundsInWorldSpace * RelativeToScreenCenterStartPosition + _deviceScreenService.ScreenCenterPointInWorldSpace;
            _playerSpaceshipView.transform.position = startPosition;
            RegisterListeners();
        }
        
        public void Dispose()
        {
            UnegisterListeners();
            DestroySpaceSip();
        }

        private void RegisterListeners()
        {
            _updateSubscriptionService.RegisterUpdatable(this);
        }
        
        private void UnegisterListeners()
        {
            _updateSubscriptionService.UnregisterUpdatable(this);
        }

        //private bool IsInScreenHorizontalBounds(float xValue, float spaceKeptFromBounds)
        //{
        //    return -_screenBoundsInWorldSpace.x + spaceKeptFromBounds < xValue && xValue < _screenBoundsInWorldSpace.x - spaceKeptFromBounds;
        //}

        private void DestroySpaceSip()
        {
            GameObject.Destroy(_playerSpaceshipView.gameObject);
        }

        public void SetMoveVelocity(float xVelocity)
        {
            _playerSpaceshipView.SetVelocity(xVelocity);
            var xDirection = xVelocity > 0 ? 1 : xVelocity.EqualsWithTolerance(0) ? 0 : -1;
            var rotationFactor = -xDirection;
            _spaceshipDestRotation = _spaceshipMaxRotationAngle * rotationFactor;
        }

        public void ManagedUpdate()
        {
            _playerSpaceshipView.LerpToRotation(_spaceshipDestRotation);
        }
    }
}
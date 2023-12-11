using System;
using CoreDomain.Scripts.Extensions;
using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipViewModule: IUpdatable
    {
        private const float SpaceShipYPosition = 3.8f;
        
        private PlayerSpaceshipView _playerSpaceshipView;
        private float _playerSpaceFromBounds;
        private float _spaceshipDestRotation = 0;
        private float _spaceshipMaxRotationAngle = 20;
        private readonly IUpdateSubscriptionService _updateSubscriptionService;
        private float _positiveXMovementBound;

        public Transform PlayerSpaceShipTransform => _playerSpaceshipView.transform;

        public PlayerSpaceshipViewModule(IUpdateSubscriptionService updateSubscriptionService)
        {
            _updateSubscriptionService = updateSubscriptionService;
        }
        
        public void Setup(PlayerSpaceshipView playerSpaceshipView)
        {
            _playerSpaceshipView = playerSpaceshipView;
            _playerSpaceshipView.transform.position = new Vector3(0, SpaceShipYPosition, 0);
        }
        
        public void Dispose()
        {
            UnregisterListeners();
            DestroySpaceSip();
        }

        public void RegisterListeners()
        {
            _updateSubscriptionService.RegisterUpdatable(this);
        }
        
        public void UnregisterListeners()
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

        public void TrySetMovementVelocity(float xVelocity)
        {
            var spaceShipPosition = _playerSpaceshipView.transform.position;
            var willMoveOutOfBounds = spaceShipPosition.x > _positiveXMovementBound && xVelocity > 0 ||
                                      spaceShipPosition.x < -_positiveXMovementBound && xVelocity < 0;
            
            if (willMoveOutOfBounds) return;
            
            SetMovementVelocity(xVelocity);
        }

        private void SetMovementVelocity(float xVelocity)
        {
            _playerSpaceshipView.SetVelocity(xVelocity);
            var xDirection = xVelocity > 0 ? 1 : xVelocity.EqualsWithTolerance(0) ? 0 : -1;
            var rotationFactor = -xDirection;
            _spaceshipDestRotation = _spaceshipMaxRotationAngle * rotationFactor;
        }

        public void EnableThrusterBoost(bool isEnabled)
        {
            _playerSpaceshipView.EnableThrusterBoost(isEnabled);
        }
        
        public void ManagedUpdate()
        {
            _playerSpaceshipView.LerpToRotation(_spaceshipDestRotation);
            ResetVelocityIfOutOfBounds();
        }

        private void ResetVelocityIfOutOfBounds()
        {
            var spaceShipPosition = _playerSpaceshipView.transform.position;
            var isSpaceShipOutOfXBounds =  spaceShipPosition.x > _positiveXMovementBound ||
                                        spaceShipPosition.x < -_positiveXMovementBound;
            
            if (isSpaceShipOutOfXBounds)
            {
                SetMovementVelocity(0);
            }
        }

        public void ResetSpaceShipTransform()
        {
            _playerSpaceshipView.transform.position = new Vector3(0, 2.5f, 0);
            _playerSpaceshipView.SetRotation(0);
        }

        public void SetXMovementBounds(float positiveXBound)
        {
            _positiveXMovementBound = positiveXBound;
        }

        public void EnableThruster(bool isEnabled)
        {
            _playerSpaceshipView.EnableThruster(isEnabled);
        }
    }
}
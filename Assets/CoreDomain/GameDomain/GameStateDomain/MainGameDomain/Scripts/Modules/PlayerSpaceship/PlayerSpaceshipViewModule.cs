using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipViewModule
    {
        private static readonly Vector2 RelativeToScreenCenterStartPosition = new (0f, -0.5f);
        
        private readonly IDeviceScreenService _deviceScreenService;
        private PlayerSpaceshipView _playerSpaceshipView;
        private readonly Vector3 _screenBoundsInWorldSpace;
        private float _playerSpaceFromBounds;

        public PlayerSpaceshipViewModule(IDeviceScreenService deviceScreenService)
        {
            _deviceScreenService = deviceScreenService;
            _screenBoundsInWorldSpace = deviceScreenService.ScreenBoundsInWorldSpace;
        }
        
        public void Setup(PlayerSpaceshipView playerSpaceshipView)
        {
            _playerSpaceshipView = playerSpaceshipView;

            var startPosition = _screenBoundsInWorldSpace * RelativeToScreenCenterStartPosition + _deviceScreenService.ScreenCenterPointInWorldSpace;
            _playerSpaceshipView.transform.position = startPosition;
        }

        private bool IsInScreenHorizontalBounds(float xValue, float spaceKeptFromBounds)
        {
            return -_screenBoundsInWorldSpace.x + spaceKeptFromBounds < xValue && xValue < _screenBoundsInWorldSpace.x - spaceKeptFromBounds;
        }

        public void DestroySpaceSip()
        {
            GameObject.Destroy(_playerSpaceshipView.gameObject);
        }

        public void SetMoveDirection(float xDirection)
        {
            _playerSpaceshipView.SetVelocity(xDirection);
        }
    }
}
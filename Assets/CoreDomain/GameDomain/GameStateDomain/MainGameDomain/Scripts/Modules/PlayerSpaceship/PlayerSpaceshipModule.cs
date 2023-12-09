using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipModule : IPlayerSpaceshipModule
    {
        public Transform PlayerSpaceShipTransform => _playerSpaceshipViewModule.PlayerSpaceShipTransform;
        
        private readonly PlayerSpaceshipCreator _playerSpaceshipCreator;
        private readonly PlayerSpaceshipViewModule _playerSpaceshipViewModule;
        private PlayerSpaceshipData _playerSpaceshipData;
        
        public PlayerSpaceshipModule(IAssetBundleLoaderService assetBundleLoaderService, IDeviceScreenService deviceScreenService, IUpdateSubscriptionService updateSubscriptionService)
        {
            _playerSpaceshipCreator = new PlayerSpaceshipCreator(assetBundleLoaderService);
            _playerSpaceshipViewModule = new PlayerSpaceshipViewModule(deviceScreenService, updateSubscriptionService);
        }

        public void CreatePlayerSpaceship()
        {
            _playerSpaceshipData = _playerSpaceshipCreator.LoadPlayerSpaceShipData();
            var playerSpaceshipView = _playerSpaceshipCreator.CreatePlayerSpaceship();
            _playerSpaceshipViewModule.Setup(playerSpaceshipView);
        }

        public void Dispose()
        {
            _playerSpaceshipViewModule.Dispose();
        }

        public void SetSpaceShipMoveDirection(float xDirection)
        {
            _playerSpaceshipViewModule.SetMoveVelocity(xDirection * _playerSpaceshipData.MovementSpeed);
        }
    }
}
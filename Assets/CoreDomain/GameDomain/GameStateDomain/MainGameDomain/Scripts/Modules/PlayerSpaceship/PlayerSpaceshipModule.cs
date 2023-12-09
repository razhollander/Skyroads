using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipModule : IPlayerSpaceshipModule
    {
        private readonly PlayerSpaceshipCreator _playerSpaceshipCreator;
        private readonly PlayerSpaceshipViewModule _playerSpaceshipViewModule;
        private PlayerSpaceshipData _playerSpaceshipData;
        
        public PlayerSpaceshipModule(IAssetBundleLoaderService assetBundleLoaderService, IDeviceScreenService deviceScreenService)
        {
            _playerSpaceshipCreator = new PlayerSpaceshipCreator(assetBundleLoaderService);
            _playerSpaceshipViewModule = new PlayerSpaceshipViewModule(deviceScreenService);
        }

        public void CreatePlayerSpaceship()
        {
            _playerSpaceshipData = _playerSpaceshipCreator.LoadPlayerSpaceShipData();
            var playerSpaceshipView = _playerSpaceshipCreator.CreatePlayerSpaceship();
            _playerSpaceshipViewModule.Setup(playerSpaceshipView);
        }

        public void Dispose()
        {
            _playerSpaceshipViewModule.DestroySpaceSip();
        }

        public void SetSpaceShipMoveDirection(float xDirection)
        {
            _playerSpaceshipViewModule.SetMoveDirection(xDirection * _playerSpaceshipData.MovementSpeed);
        }
    }
}
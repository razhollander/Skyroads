using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipModule : IPlayerSpaceshipModule
    {
        private readonly PlayerHitCommand.Factory _playerHitCommand;
        public Transform PlayerSpaceShipTransform => _playerSpaceshipViewModule.PlayerSpaceShipTransform;
        
        private readonly PlayerSpaceshipCreator _playerSpaceshipCreator;
        private readonly PlayerSpaceshipViewModule _playerSpaceshipViewModule;
        private PlayerSpaceshipData _playerSpaceshipData;
        
        public PlayerSpaceshipModule(IAssetBundleLoaderService assetBundleLoaderService, IDeviceScreenService deviceScreenService, IUpdateSubscriptionService updateSubscriptionService, PlayerHitCommand.Factory playerHitCommand)
        {
            _playerHitCommand = playerHitCommand;
            _playerSpaceshipCreator = new PlayerSpaceshipCreator(assetBundleLoaderService);
            _playerSpaceshipViewModule = new PlayerSpaceshipViewModule(deviceScreenService, updateSubscriptionService);
        }

        public void CreatePlayerSpaceship()
        {
            _playerSpaceshipData = _playerSpaceshipCreator.LoadPlayerSpaceShipData();
            var playerSpaceshipView = _playerSpaceshipCreator.CreatePlayerSpaceship();
            playerSpaceshipView.SetupCallbacks(OnSpaceshipCollisionEnter);
            _playerSpaceshipViewModule.Setup(playerSpaceshipView);
        }
        
        public void EnableSpaceShipMovement(bool isEnabled)
        {
            if (isEnabled)
            {
                _playerSpaceshipViewModule.RegisterListeners();
            }
            else
            {
                _playerSpaceshipViewModule.SetMoveVelocity(0);
                _playerSpaceshipViewModule.UnregisterListeners();
            }
        }
        
        private void OnSpaceshipCollisionEnter(Collider collision)
        {
            var didCollideWithAsteroid = collision.gameObject.GetComponent<AsteroidView>() != null;
            
            if (didCollideWithAsteroid)
            {
                _playerHitCommand.Create().Execute().Forget();
            }
        }
        public void Dispose()
        {
            _playerSpaceshipViewModule.Dispose();
        }

        public void SetSpaceShipMoveDirection(float xDirection)
        {
            _playerSpaceshipViewModule.SetMoveVelocity(xDirection * _playerSpaceshipData.MovementSpeed);
        }

        public void ResetSpaceShip()
        {
            _playerSpaceshipViewModule.ResetSpaceShip();
        }
    }
}
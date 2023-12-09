using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerBullet;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class SpaceButtonClickedCommand : CommandSync<SpaceButtonClickedCommand>
    {
        private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
        private readonly IPlayerBulletModule _playerBulletModule;

        private SpaceButtonClickedCommand(IPlayerSpaceshipModule playerSpaceshipModule, IPlayerBulletModule playerBulletModule)
        {
            _playerSpaceshipModule = playerSpaceshipModule;
            _playerBulletModule = playerBulletModule;
        }

        public override void Execute()
        {
        }
    }
}
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Enemies;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerBullet;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Services.GameStates;
using CoreDomain.Utils.Pools;
using Zenject;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class MainGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameKeyboardInputsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpeedService>().AsSingle().NonLazy();

            Container.BindInterfacesTo<MainGameUiModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerSpaceshipModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FloorModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemiesModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerBulletModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AsteroidsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimePlayingModule>().AsSingle().NonLazy();
            
            Container.BindFactory<float, JoystickDraggedCommand, JoystickDraggedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<GameBoostModeChangedCommandData, GameBoostModeChangedCommand, GameBoostModeChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<SpaceButtonClickedCommand, SpaceButtonClickedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<SpaceButtonReleasedCommand, SpaceButtonReleasedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<BackButtonClickedCommand, BackButtonClickedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PlayerBulletHitCommandData, PlayerBulletHitCommand, PlayerBulletHitCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, PlayerBulletPool, PlayerBulletPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, BeeEnemiesPool, BeeEnemiesPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, GuardEnemiesPool, GuardEnemiesPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, AsteroidsPool, AsteroidsPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<MainGameStateEnterData, EnterMainGameStateCommand, EnterMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ExitMainGameStateCommand, ExitMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<SpawnAsteroidCommand, SpawnAsteroidCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<float, ArrowKeysInputChangedCommand, ArrowKeysInputChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ScoreChangedCommandData, ScoreChangedCommand, ScoreChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<int, TimePlayingChangedCommand, TimePlayingChangedCommand.Factory>().AsSingle().NonLazy();

        }
    }
}
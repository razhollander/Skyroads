using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Enemies;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using CoreDomain.Services.GameStates;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class EnterMainGameStateCommand : CommandOneParameter<MainGameStateEnterData, EnterMainGameStateCommand>
    {
        private readonly MainGameStateEnterData _stateEnterData;
        private readonly IMainGameUiModule _mainGameUiModule;
        private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
        private readonly ILevelsService _levelsService;
        private readonly IEnemiesModule _enemiesModule;
        private readonly IAudioService _audioService;
        private readonly IFloorModule _floorModule;
        private readonly IGameSpeedService _gameSpeedService;
        private readonly ICameraService _cameraService;
        private readonly IAsteroidsModule _asteroidsModule;

        public EnterMainGameStateCommand(MainGameStateEnterData stateEnterData, IMainGameUiModule mainGameUiModule, IPlayerSpaceshipModule playerSpaceshipModule,
            ILevelsService levelsService, IEnemiesModule enemiesModule, IAudioService audioService, IFloorModule floorModule, IGameSpeedService gameSpeedService, ICameraService cameraService, IAsteroidsModule asteroidsModule)
        {
            _stateEnterData = stateEnterData;
            _mainGameUiModule = mainGameUiModule;
            _playerSpaceshipModule = playerSpaceshipModule;
            _floorModule = floorModule;
            _gameSpeedService = gameSpeedService;
            _cameraService = cameraService;
            _asteroidsModule = asteroidsModule;
            _levelsService = levelsService;
            _enemiesModule = enemiesModule;
            _audioService = audioService;
        }

        public override async UniTask Execute()
        {
            //var enterData = _stateEnterData;
            //_mainGameUiModule.CreateMainGameUi();
            _gameSpeedService.LoadGameSpeedData();
            _asteroidsModule.LoadData();

            _playerSpaceshipModule.CreatePlayerSpaceship();
            _floorModule.CreateFloor();
            _cameraService.SetCameraFollowTarget(GameCameraType.World, _playerSpaceshipModule.PlayerSpaceShipTransform);
            _cameraService.SetCameraZoom(GameCameraType.World, true);
            _floorModule.StartMovement();
            _asteroidsModule.StartSpawning();
            //var levelData = _levelsService.GetLevelData(enterData.Level);
            //_enemiesModule.StartEnemiesWavesSequence(levelData.EnemiesWaveSequenceData);
            _audioService.PlayAudio(AudioClipName.ThemeSongName, AudioChannelType.Master, AudioPlayType.Loop);
        }
    }
}
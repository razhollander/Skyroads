using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerBullet;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoostModeChangedCommand : CommandOneParameter<GameBoostModeChangedCommandData, GameBoostModeChangedCommand>
{
    private readonly GameBoostModeChangedCommandData _commandData;
    private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
    private readonly IPlayerBulletModule _playerBulletModule;
    private readonly ICameraService _cameraService;
    private readonly IGameSpeedService _gameSpeedService;
    private readonly IFloorModule _floorModule;
    public GameBoostModeChangedCommand(GameBoostModeChangedCommandData commandData, IPlayerSpaceshipModule playerSpaceshipModule, IPlayerBulletModule playerBulletModule, ICameraService cameraService, IGameSpeedService gameSpeedService, IFloorModule floorModule)
    {
        _commandData = commandData;
        _playerSpaceshipModule = playerSpaceshipModule;
        _playerBulletModule = playerBulletModule;
        _cameraService = cameraService;
        _gameSpeedService = gameSpeedService;
        _floorModule = floorModule;
    }
    
   
    public override async UniTask Execute()
    {
        _gameSpeedService.SetBoostMode(_commandData.IsBoostOn);
        _cameraService.SetCameraZoom(GameCameraType.World, !_commandData.IsBoostOn);
    }
}

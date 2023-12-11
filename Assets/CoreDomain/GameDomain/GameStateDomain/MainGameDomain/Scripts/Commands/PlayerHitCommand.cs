using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerHitCommand : Command<PlayerHitCommand>
{
    private readonly IAsteroidsModule _asteroidsModule;
    private readonly IFloorModule _floorModule;
    private readonly IGameKeyboardInputsModule _keyboardInputsModule;
    private readonly IHighScoreModule _highScoreModule;
    private readonly IScoreModule _scoreModule;
    private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
    private readonly ITimePlayingModule _timePlayingModule;
    private readonly IMainGameUiModule _mainGameUiModule;

    public PlayerHitCommand(
        IAsteroidsModule asteroidsModule,
        IFloorModule floorModule,
        IGameKeyboardInputsModule keyboardInputsModule,
        IHighScoreModule highScoreModule,
        IScoreModule scoreModule,
        IPlayerSpaceshipModule playerSpaceshipModule,
        ITimePlayingModule timePlayingModule,
        IMainGameUiModule mainGameUiModule)
    {
        _asteroidsModule = asteroidsModule;
        _floorModule = floorModule;
        _keyboardInputsModule = keyboardInputsModule;
        _highScoreModule = highScoreModule;
        _scoreModule = scoreModule;
        _playerSpaceshipModule = playerSpaceshipModule;
        _timePlayingModule = timePlayingModule;
        _mainGameUiModule = mainGameUiModule;
    }
    
    public override async UniTask Execute()
    {
        _keyboardInputsModule.DisableInputs();
        _asteroidsModule.StopSpawning();
        _floorModule.StopMovement();
        _scoreModule.StopCountingScore();
        _playerSpaceshipModule.EnableSpaceShipMovement(true);
        _timePlayingModule.StopTimer();

        bool isNewHighScore = _highScoreModule.LastHighScore < _scoreModule.PlayerScore;
        
        if (isNewHighScore)
        {
            _highScoreModule.SaveHighScore(_scoreModule.PlayerScore);
        }
        
        _mainGameUiModule.ShowGameOverPanel(_scoreModule.PlayerScore, _timePlayingModule.TimePlaying, _asteroidsModule.AsteroidsPassedPlayerCounter, isNewHighScore, _highScoreModule.LastHighScore);
    }
}

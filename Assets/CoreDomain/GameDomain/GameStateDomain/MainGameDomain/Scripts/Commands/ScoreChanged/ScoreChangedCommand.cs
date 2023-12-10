using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScoreChangedCommand : CommandSyncOneParameter<ScoreChangedCommandData, ScoreChangedCommand>
{
    private readonly ScoreChangedCommandData _commandData;
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;

    public ScoreChangedCommand(ScoreChangedCommandData commandData, IScoreModule scoreModule, IMainGameUiModule mainGameUiModule)
    {
        _commandData = commandData;
        _scoreModule = scoreModule;
        _mainGameUiModule = mainGameUiModule;
    }
    
    public override void Execute()
    {
        _scoreModule.AddScore(_commandData.ScoreAdded);
        _mainGameUiModule.UpdateScore(_scoreModule.PlayerScore);
    }
}
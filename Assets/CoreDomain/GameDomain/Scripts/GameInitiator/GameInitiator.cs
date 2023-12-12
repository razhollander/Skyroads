using CoreDomain.Services.GameStates;
using UnityEngine;
using Zenject;

namespace CoreDomain.GameDomain
{
    public class GameInitiator : MonoBehaviour
    {
        private IStateMachineService _stateMachine;
        private MainGameState.Factory _mainGameStateFactory;

        [Inject]
        private void Setup(IStateMachineService stateMachine, MainGameState.Factory mainGameStateFactory)
        {
            _stateMachine = stateMachine;
            _mainGameStateFactory = mainGameStateFactory;
        }

        private void Start()
        {
            UpdateApplicationSettings();
            InitializeServices();
            EnterToLobbyGameState();
        }

        private void InitializeServices()
        {
            
        }

        private void EnterToLobbyGameState()
        {
            _stateMachine.EnterInitialGameState(_mainGameStateFactory.Create(new MainGameStateEnterData()));
        }

        private void UpdateApplicationSettings()
        {
            Screen.SetResolution(1920, 1080, true);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }
    }
}
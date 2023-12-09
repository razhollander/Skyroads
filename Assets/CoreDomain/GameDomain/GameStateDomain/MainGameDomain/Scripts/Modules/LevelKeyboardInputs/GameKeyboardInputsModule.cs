using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.Scripts.Extensions;
using CoreDomain.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule
{
    public class GameKeyboardInputsModule : IGameKeyboardInputsModule, IUpdatable
    {
        private readonly IUpdateSubscriptionService _updateSubscriptionService;

        private readonly GameInputActions _gameInputActions;
        private readonly ArrowKeysInputChangedCommand.Factory _arrowKeysInputChangedCommandFactory;
        private readonly SpaceButtonClickedCommand.Factory _spaceButtonClickedCommandFactory;
        private readonly SpaceButtonReleasedCommand.Factory _spaceButtonReleasedCommandFactory;
        private float _arrowsDirectionValue;

        public GameKeyboardInputsModule(IUpdateSubscriptionService updateSubscriptionService, GameInputActions gameInputActions, ArrowKeysInputChangedCommand.Factory arrowKeysInputChangedCommandFactory, SpaceButtonClickedCommand.Factory spaceButtonClickedCommandFactory, SpaceButtonReleasedCommand.Factory spaceButtonReleasedCommandFactory)
        {
            _updateSubscriptionService = updateSubscriptionService;
            _gameInputActions = gameInputActions;
            _arrowKeysInputChangedCommandFactory = arrowKeysInputChangedCommandFactory;
            _spaceButtonClickedCommandFactory = spaceButtonClickedCommandFactory;
            _spaceButtonReleasedCommandFactory = spaceButtonReleasedCommandFactory;

            AddListeners();
        }

        public void RemoveInputsListeners()
        {
            RemoveListeners();
        }

        public void Dispose()
        {
            RemoveListeners();
        }

        public void ManagedUpdate()
        {
            CheckArrowKeysInput();
        }

        private void CheckArrowKeysInput()
        {
            var newArrowsDirectionValue = _gameInputActions.MainGame.Move.ReadValue<float>();
            
            if(!newArrowsDirectionValue.EqualsWithTolerance(_arrowsDirectionValue))
            {
                _arrowsDirectionValue = newArrowsDirectionValue;
                _arrowKeysInputChangedCommandFactory.Create(_arrowsDirectionValue).Execute();
            }
        }

        private void AddListeners()
        {
            _gameInputActions.MainGame.Boost.started += OnSpaceBarClicked;
            _gameInputActions.MainGame.Boost.canceled += OnSpaceBarReleased;
            _updateSubscriptionService.RegisterUpdatable(this);
        }

        private void OnSpaceBarReleased(InputAction.CallbackContext obj)
        {
            _spaceButtonReleasedCommandFactory.Create().Execute();
        }

        private void RemoveListeners()
        {
            _gameInputActions.MainGame.Boost.started -= OnSpaceBarClicked;
            _gameInputActions.MainGame.Boost.canceled -= OnSpaceBarReleased;
            _updateSubscriptionService.UnregisterUpdatable(this);
        }

        private void OnSpaceBarClicked(InputAction.CallbackContext context)
        {
            _spaceButtonClickedCommandFactory.Create().Execute();
        }
    }
}
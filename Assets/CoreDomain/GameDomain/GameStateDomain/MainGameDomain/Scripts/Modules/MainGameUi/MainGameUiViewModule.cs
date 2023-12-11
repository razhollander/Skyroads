using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiViewModule
    {
        private MainGameUiView _mainGameUiView;

        public void SetupMainGameUiView(MainGameUiView mainGameUiView)
        {
            _mainGameUiView = mainGameUiView;
        }

        public void DestroyMainGameUiView()
        {
            Object.Destroy(_mainGameUiView.gameObject);
        }

        public void UpdateScore(int newScore)
        {
            _mainGameUiView.UpdateScore(newScore);
        }

        public void UpdateTimePlaying(int timePlaying)
        {
            _mainGameUiView.UpdateTimePlaying(timePlaying);
        }  
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _mainGameUiView.UpdateAsteroidsPassedCountable(asteroidsPassed);
        }

        public void UpdateHighScore(int lastHighScore)
        {
            _mainGameUiView.UpdateHighScore(lastHighScore);
        }
    }
}

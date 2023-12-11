using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiModule : IMainGameUiModule
    {
        private readonly MainGameUiCreator _creator;
        private readonly MainGameUiViewModule _viewModule;

        public MainGameUiModule(IAssetBundleLoaderService assetBundleLoaderService)
        {
            _creator = new MainGameUiCreator(assetBundleLoaderService);
            _viewModule = new MainGameUiViewModule();
        }

        public void UpdateScore(int newScore)
        {
            _viewModule.UpdateScore(newScore);
        }

        public void CreateMainGameUi()
        {
            var mainGameUiView = _creator.CreateMainGameUi();
            _viewModule.SetupMainGameUiView(mainGameUiView);
        }
        
        public void Dispose()
        {
            _viewModule.DestroyMainGameUiView();
        }

        public void UpdateTimePlaying(int timePlaying)
        {
            _viewModule.UpdateTimePlaying(timePlaying);
        }
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _viewModule.UpdateAsteroidsPassedCountable(asteroidsPassed);
        }

        public void UpdateHighScore(int highScore)
        {
            _viewModule.UpdateHighScore(highScore);
        }
    }
}
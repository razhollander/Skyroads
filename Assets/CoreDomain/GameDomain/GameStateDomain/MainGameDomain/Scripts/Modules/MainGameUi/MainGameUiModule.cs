using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiModule : IMainGameUiModule
    {
        private readonly JoystickDraggedCommand.Factory _joystickDraggedCommandFactory;
        private readonly CommandSync<SpaceButtonClickedCommand>.Factory _shootButtonClickedCommandFactory;
        private readonly CommandSync<BackButtonClickedCommand>.Factory _backButtonClickedCommandFactory;
        private readonly MainGameUiCreator _creator;
        private readonly MainGameUiViewModule _viewModule;

        public MainGameUiModule(IAssetBundleLoaderService assetBundleLoaderService, JoystickDraggedCommand.Factory joystickDraggedCommandFactory, SpaceButtonClickedCommand.Factory shootButtonClickedCommandFactory, BackButtonClickedCommand.Factory backButtonClickedCommandFactory)
        {
            _joystickDraggedCommandFactory = joystickDraggedCommandFactory;
            _shootButtonClickedCommandFactory = shootButtonClickedCommandFactory;
            _backButtonClickedCommandFactory = backButtonClickedCommandFactory;
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
            mainGameUiView.Setup(OnShootButtonClicked, OnJoystickInputChanged, OnBackButtonClicked);
            _viewModule.SetupMainGameUiView(mainGameUiView);
        }

        private void OnBackButtonClicked()
        {
            _backButtonClickedCommandFactory.Create().Execute();
        }

        private void OnJoystickInputChanged(float dragValue)
        {
            _joystickDraggedCommandFactory.Create(dragValue).Execute();
        }

        private void OnShootButtonClicked()
        {
            _shootButtonClickedCommandFactory.Create().Execute();
        }

        public void Dispose()
        {
            _viewModule.DestroyMainGameUiView();
        }
    }
}
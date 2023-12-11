namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public interface IMainGameUiModule
    {
        void UpdateScore(int newScore);
        void CreateMainGameUi();
        void Dispose();
        void UpdateTimePlaying(int timePlaying);
        void UpdateAsteroidsPassedCountable(int asteroidsPassed);
        void UpdateHighScore(int highScore);
    }
}
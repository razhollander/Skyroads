public interface IGameSpeedService
{
    bool IsBoosting { get; }
    float CurrentGameSpeed { get; }
    void LoadLevels();
    void ChangeBoostMode(bool isOn);
}
using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    public class ScoreModule : IScoreModule, IUpdatable
    {
        private const string GameSpeedAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/gamescore";
        private const string GameSpeedSettingsAssetName = "GameScoreData";
        
        private readonly IUpdateSubscriptionService _updateSubscriptionService;
        private float _playerCurrentScore;
        private float _scoreGainedEverySecondMultiplier;
        public int PlayerScore => Mathf.FloorToInt(_playerCurrentScore);
        private ScoreData _scoreData;
        private readonly IAssetBundleLoaderService _assetBundleLoaderService;

        public ScoreModule(IUpdateSubscriptionService updateSubscriptionService, IAssetBundleLoaderService assetBundleLoaderService)
        {
            _updateSubscriptionService = updateSubscriptionService;
            _assetBundleLoaderService = assetBundleLoaderService;
        }

        public void LoadScoreConfig()
        {
            _scoreData = _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<ScoreData>(GameSpeedAssetBundlePath, GameSpeedSettingsAssetName);
            _scoreGainedEverySecondMultiplier = _scoreData.GainEverySecondScore;
        }

        public void StartCountingScore()
        {
            _updateSubscriptionService.RegisterUpdatable(this);
        }
        
        public void StopCountingScore()
        {
            _updateSubscriptionService.UnregisterUpdatable(this);
        }
        
        public void AddScore(float score)
        {
            _playerCurrentScore += score;
        }

        public void SetMultiplier(bool isOn)
        {
            _scoreGainedEverySecondMultiplier = isOn ? _scoreData.GainEverySecondScore * _scoreData.BoostScoreMultiplier : _scoreData.GainEverySecondScore;
        }
        
        public void ManagedUpdate()
        {
            AddScore(Time.deltaTime * _scoreGainedEverySecondMultiplier);
            Debug.Log(PlayerScore);
        }
    }
}
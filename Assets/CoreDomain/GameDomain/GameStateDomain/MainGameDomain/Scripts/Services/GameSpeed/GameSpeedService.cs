using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;

public class GameSpeedService : IGameSpeedService
{
    private const string GameSpeedAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/gamespeed";
    private const string GameSpeedSettingsAssetName = "GameSpeedData";

    public bool IsBoosting { get; private set; }
    public float CurrentGameSpeed { get; private set; }

    private GameSpeedData _gameSpeedConfigData;
    private readonly IAssetBundleLoaderService _assetBundleLoaderService;

    public GameSpeedService(IAssetBundleLoaderService assetBundleLoaderService)
    {
        _assetBundleLoaderService = assetBundleLoaderService;
    }

    public void LoadLevels()
    {
        _gameSpeedConfigData = _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<GameSpeedData>(GameSpeedAssetBundlePath, GameSpeedSettingsAssetName);
    } 
    public void ChangeBoostMode(bool isOn)
    {
        IsBoosting = isOn; 
        CurrentGameSpeed = IsBoosting ? _gameSpeedConfigData.BaseSpeed : _gameSpeedConfigData.BoostSpeedMultiplier * _gameSpeedConfigData.BaseSpeed;
    }
}
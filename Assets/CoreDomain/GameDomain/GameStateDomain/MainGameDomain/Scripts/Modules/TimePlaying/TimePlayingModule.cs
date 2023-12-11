using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class TimePlayingModule : ITimePlayingModule, IUpdatable
{
    private int TimePlaying => Mathf.FloorToInt(_timePlaying);

    private readonly TimePlayingChangedCommand.Factory _timePlayingChangedCommand;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private float _timePlaying;
    private int _prevTimePlaying;
    private IDisposable _timerCoroutine;
    private CancellationToken _timerToken;
    private CancellationTokenSource _cancellationToken;

    public TimePlayingModule(TimePlayingChangedCommand.Factory timePlayingChangedCommand, IUpdateSubscriptionService updateSubscriptionService)
    {
        _timePlayingChangedCommand = timePlayingChangedCommand;
        _updateSubscriptionService = updateSubscriptionService;
    }
    public void StartTimer()
    {
        _timePlaying = 0;
        _prevTimePlaying = 0;
        _updateSubscriptionService.RegisterUpdatable(this);
    }

    public void StopTimer()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }

    public void ManagedUpdate()
    {
        _timePlaying += Time.deltaTime;
        var currentTimePlaying = Mathf.FloorToInt(_timePlaying);
        
        if (currentTimePlaying != _prevTimePlaying)
        {
            _prevTimePlaying = currentTimePlaying;
            _timePlayingChangedCommand.Create(currentTimePlaying).Execute();
        }
    }
}

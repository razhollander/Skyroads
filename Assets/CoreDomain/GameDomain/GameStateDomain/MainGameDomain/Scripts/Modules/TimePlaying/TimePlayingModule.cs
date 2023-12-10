using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class TimePlayingModule : ITimePlayingModule
{
    private readonly TimePlayingChangedCommand.Factory _timePlayingChangedCommand;
    private int _timePlaying;
    private IDisposable _timerCoroutine;
    private CancellationToken _timerToken;
    private CancellationTokenSource _cancellationToken;

    public TimePlayingModule(TimePlayingChangedCommand.Factory timePlayingChangedCommand)
    {
        _timePlayingChangedCommand = timePlayingChangedCommand;
    }
    public void StartTimer()
    {
        _cancellationToken = new CancellationTokenSource();
        StartTimerTask(_cancellationToken).Forget();

        //_timer = Observable.Interval(TimeSpan.FromMilliseconds(1000)).Subscribe(x =>
        //{
        //    _timePlayingChangedCommand.Create((int) x).Execute();
        //});
    }

    private async UniTaskVoid StartTimerTask(CancellationTokenSource cancellationToken)
    {
        while (true)
        {
            await UniTask.Delay(1000, cancellationToken: cancellationToken.Token);
            _timePlaying++;
            _timePlayingChangedCommand.Create(_timePlaying).Execute();
        }
    }

    public void StopTimer()
    {
        _cancellationToken.Cancel();
        _timePlaying = 0;
    }
}

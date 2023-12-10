using System;
using CoreDomain.Scripts.Utils.Command;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiView : MonoBehaviour
    {
        [SerializeField] private Countable _scoreCountable;
        [SerializeField] private Countable _timePlayingText;

        public void Setup(Action shootButtonClicked, Action<float> onJoystickDragged, Action onBackButtonClicked)
        {
            
        }

        public void UpdateScore(int newScore)
        {
            _scoreCountable.SetNumber(newScore);
        }
        
        private void Awake()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            
        }

    

        private void RemoveListeners()
        {
            
        }
        
        public void UpdateTimePlaying(int timePlaying)
        {
            _timePlayingText.SetNumber(timePlaying);
        }
    }
}
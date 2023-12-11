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
        [SerializeField] private Countable _highScoreCountable;
        [SerializeField] private Countable _timePlayingCountable;
        [SerializeField] private Countable _asteroidsPassedCountable;
        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _beforeGamePanel;

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

        public void SwitchToInGameView()
        {
            _inGamePanel.SetActive(true);
            _beforeGamePanel.SetActive(false);
        }
        
        public void SwitchToBeforeGameView()
        {
            _inGamePanel.SetActive(false);
            _beforeGamePanel.SetActive(true);
        }

        private void RemoveListeners()
        {
            
        }
        
        public void UpdateTimePlaying(int timePlaying)
        {
            _timePlayingCountable.SetNumber(timePlaying);
        }  
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _asteroidsPassedCountable.SetNumber(asteroidsPassed);
        }

        public void UpdateHighScore(int highScore)
        {
            _highScoreCountable.SetNumber(highScore);
        }
    }
}
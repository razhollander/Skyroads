using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverUiView : MonoBehaviour
{
    private const string PassedHighScoreMessageFormat = "Congratulations, You Broke Your High Score! New HighScore: ” {0}";
    private const string DidntPassHighScoreMessageFormat = "You Didn't Pass Your HighScore of {0} :(";
    
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timePlayedText;
    [SerializeField] private TextMeshProUGUI _asteroidsPassedText;
    [SerializeField] private TextMeshProUGUI _highScoreMessageText;

    public void SetAllTexts(
        int score,
        int timePlayed,
        int asteroidsPassed,
        bool isNewHighScore,
        int highScore)
    {
        _scoreText.text = $"Score: {score}";
        _timePlayedText.text = $"Time Played: {timePlayed}";
        _asteroidsPassedText.text = $"Asteroids Passed: {asteroidsPassed}";
        _highScoreMessageText.text = isNewHighScore ? string.Format(PassedHighScoreMessageFormat, highScore) : string.Format(DidntPassHighScoreMessageFormat, highScore);
    }
}

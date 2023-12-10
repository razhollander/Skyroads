using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    [CreateAssetMenu(fileName = "GameScoreData", menuName = "Game/GameScore")]
    public class ScoreData : ScriptableObject
    {
        public float GainEverySecondScore;
        public float BoostScoreMultiplier;
    }
}
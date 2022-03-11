using UnityEngine;
using PewPew.Utilities;

namespace PewPew.Services
{
    public class ScoreService
    {
        private static int score = 0;

        internal static void IncrementScore(int scoreToAdd)
        {
            score += scoreToAdd;
            UIService.Instance.UpdateScoreText(score);
        }
    }
}
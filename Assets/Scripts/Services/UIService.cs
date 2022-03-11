using UnityEngine;
using TMPro;
using PewPew.Utilities;

namespace PewPew.Services
{
    public class UIService : SingletonGeneric<UIService>
    {
        // References:
        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private TextMeshProUGUI Scoretext;
        [SerializeField] private TextMeshProUGUI LivesText;

        internal void UpdateLivesText(int lives)
        {
            LivesText.text = lives.ToString();
        }

        internal void UpdateScoreText(int score)
        {
            Scoretext.text = score.ToString();
        }

        internal void ToggleGameOverPanel(bool isActive)
        {
            GameOverPanel.SetActive(isActive);
        }
    }
}

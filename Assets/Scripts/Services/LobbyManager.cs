using PewPew.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PewPew.Services
{
    /// <summary>
    /// This class is used for implementing UI for Game Lobby.
    /// </summary>
    public class LobbyManager : MonoBehaviour
    {

        public void Play()
        {
            SceneManager.LoadScene(1);
            SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick1);
        }

        public void Quit()
        {
            SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick2);
            Application.Quit();
        }
    }
}
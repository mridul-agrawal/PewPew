using PewPew.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

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

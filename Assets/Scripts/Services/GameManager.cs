using PewPew.Audio;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CanvasRenderer GameOverPanel;
    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI LivesText;
    public ParticleSystem explosion;
    public PlayerController player;
    public int lives = 3;
    public float respawnTime = 3.0f;

    public int Score = 0;

    private void Start()
    {
        Asteroid.OnAsteroidDestroy += AsteroidDestroyed;
    }

    public void PlayerDied()
    {
        lives--;
        LivesText.text = lives.ToString();
        explosion.transform.position = player.transform.position;
        explosion.Play();

        if(lives<=0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    public void AsteroidDestroyed(Vector3 position)
    {
        explosion.transform.position = position;
        explosion.Play();

        Score += 10;
        Scoretext.text = Score.ToString();
    }

    public void GameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick1);
        SceneManager.LoadScene(0);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public PlayerController player;
    public int lives = 3;
    public float respawnTime = 3.0f;

    public int Score;

    public void PlayerDied()
    {
        lives--;
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
    }

    public void GameOver()
    {
        //TODO
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

}

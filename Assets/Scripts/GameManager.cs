using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public int lives = 3;
    public float respawnTime = 3.0f;

    public void PlayerDied()
    {
        lives--;

        if(lives<=0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
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

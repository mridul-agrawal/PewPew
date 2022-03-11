using PewPew.Audio;
using PewPew.Asteroids;
using PewPew.Player;
using PewPew.VFX;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace PewPew.Services
{
    /// <summary>
    /// Handles all the in game events and communicates between various services.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // References:
        [SerializeField] private PlayerController player;

        // Variables:
        [SerializeField] private int lives = 3;


        private void Start()
        {
            AddListenersToEvents();
        }

        // Adds listeners to invocation list of events.
        private void AddListenersToEvents()
        {
            Asteroid.OnAsteroidDestroy += AsteroidDestroyed;
            PlayerController.OnPlayerDeath += PlayerDied;
        }

        // Handles the player death logic when the event is invoked.
        public void PlayerDied()
        {
            UIService.Instance.UpdateLivesText(--lives);
            ParticleEffects.Instance.PlayExplosionAt(player.transform.position);

            if (lives <= 0)
            {
                UIService.Instance.ToggleGameOverPanel(true);
            }
            else
            {
                StartCoroutine(player.Respawn());
            }
        }

        // Handles the asteroid destroyed logic when the event is invoked.
        public void AsteroidDestroyed(Vector3 position)
        {
            ParticleEffects.Instance.PlayExplosionAt(position);
            ScoreService.IncrementScore(10);
        }

        // Restarts the game.
        public void Restart()
        {
            SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Takes back to the Lobby Screen
        public void Quit()
        {
            SoundManager.Instance.PlaySoundEffects2(SoundType.ButtonClick1);
            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            RemoveListenersFromEvents();
        }

        // Removes listeners from invocation list of events.
        private void RemoveListenersFromEvents()
        {
            Asteroid.OnAsteroidDestroy -= AsteroidDestroyed;
            PlayerController.OnPlayerDeath -= PlayerDied;
        }
    }

}
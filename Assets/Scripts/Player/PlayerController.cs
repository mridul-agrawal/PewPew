using System;
using System.Collections;
using PewPew.Audio;
using UnityEngine;

namespace PewPew.Player
{
    /// <summary>
    /// This class is used to Control Player Ship.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // Variables:
        [SerializeField] private float speed = 0.01f;
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private float respawnTime = 3.0f;
        [SerializeField] private float respawnProtectionTime = 3.0f;
        private Coroutine firingRoutine = null;

        // References:
        [SerializeField] private GameObject Bullet;
        [SerializeField] private Transform weaponPoint1;
        [SerializeField] private Transform weaponPoint2;
        private Rigidbody2D playerRigidBody;

        // Events:
        public static event Action OnPlayerDeath;


        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            StartCoroutine(Initialize());
        }

        // This method is used to ignore collisions for some time after being enabled.
        IEnumerator Initialize()
        {
            gameObject.layer = 7;
            yield return new WaitForSeconds(respawnProtectionTime);
            gameObject.layer = 0;
        }

        private void FixedUpdate()
        {
            HandlePlayerMovement();
            HandlePlayerRotation();
        }

        private void Update()
        {
            HandleShooting();
        }

        // Handles the player movement according to the input.
        private void HandlePlayerMovement()
        {

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * speed);
                PlayMoveSound();
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * speed);
                PlayMoveSound();
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * speed);
                PlayMoveSound();
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * speed);
                PlayMoveSound();
            }
            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                SoundManager.Instance.StopSoundEffect();
            }
        }

        // Used to play movement sound when needed.
        private static void PlayMoveSound()
        {
            if (!SoundManager.Instance.audioEffects.isPlaying)
            {
                SoundManager.Instance.PlaySoundEffects(SoundType.PlayerMove);
            }
        }

        // Used to rotate player ship.
        private void HandlePlayerRotation()
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        // Used to shoot bullets according to the input recieved.
        private void HandleShooting()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                firingRoutine = StartCoroutine(Fire());
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(firingRoutine);
            }
        }

        // Used to Instantiate bullets with the specified time interval.
        IEnumerator Fire()
        {
            while (true)
            {
                Instantiate(Bullet, weaponPoint1.position, weaponPoint1.rotation);
                Instantiate(Bullet, weaponPoint2.position, weaponPoint2.rotation);
                SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerShoot);
                yield return new WaitForSeconds(fireRate);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Asteroid")
            {
                playerRigidBody.velocity = Vector3.zero;
                playerRigidBody.angularVelocity = 0.0f;
                gameObject.SetActive(false);
                SoundManager.Instance.StopSoundEffect();
                SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerDeath);
                OnPlayerDeath.Invoke();
            }
        }

        internal IEnumerator Respawn()
        {
            yield return new WaitForSeconds(respawnTime);
            transform.position = Vector3.zero;
            gameObject.SetActive(true);
        }

    }
}
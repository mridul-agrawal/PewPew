using System.Collections;
using PewPew.Audio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 0.01f;
    public float fireRate = 0.2f;

    public GameManager gameManager;
    public GameObject Bullet;
    public Transform weaponPoint1;
    public Transform weaponPoint2;
    public float respawnProtectionTime = 3.0f;

    private Rigidbody2D playerRigidBody;
    private Coroutine firingRoutine = null;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("ignoreCollisions");
        Invoke(nameof(TurnOnCollisions), respawnProtectionTime);
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

    private void HandlePlayerMovement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed);
            if(!SoundManager.Instance.audioEffects.isPlaying)
            {
                SoundManager.Instance.PlaySoundEffects(SoundType.PlayerMove);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed);
            if (!SoundManager.Instance.audioEffects.isPlaying)
            {
                SoundManager.Instance.PlaySoundEffects(SoundType.PlayerMove);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed);
            if (!SoundManager.Instance.audioEffects.isPlaying)
            {
                SoundManager.Instance.PlaySoundEffects(SoundType.PlayerMove);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed);
            if (!SoundManager.Instance.audioEffects.isPlaying)
            {
                SoundManager.Instance.PlaySoundEffects(SoundType.PlayerMove);
            }
        } 
        if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            SoundManager.Instance.StopSoundEffect();
        }
    }

    private void HandlePlayerRotation()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firingRoutine = StartCoroutine(Fire());
        } else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingRoutine);
        }
    }

    IEnumerator Fire()
    {
        while(true)
        {
            Instantiate(Bullet, weaponPoint1.position, weaponPoint1.rotation);
            Instantiate(Bullet, weaponPoint2.position, weaponPoint2.rotation);
            SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerShoot);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            SoundManager.Instance.StopSoundEffect();
            SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerDeath);
            gameManager.PlayerDied();
        }
    }

    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}

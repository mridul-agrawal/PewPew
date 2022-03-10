using PewPew.Audio;
using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // References:
    public Sprite[] BigSprites;
    public Sprite[] MediumSprites;
    public Sprite[] SmallSprites;
    private SpriteRenderer AsteroidSprite;
    private Rigidbody2D AsteroidRigidBody;

    private enum Size { Big, Medium, Small }
    
    // Variables:
    private Size size;
    [SerializeField] private float speed = 10;
    [SerializeField] private int health = 100;

    // Events:
    public static event Action<Vector3> OnAsteroidDestroy;


    private void Awake()
    {
        SetReferences();
        InitializeAsteroid();
    }

    // Sets references to components.
    private void SetReferences()
    {
        AsteroidSprite = GetComponent<SpriteRenderer>();
        AsteroidRigidBody = GetComponent<Rigidbody2D>();
    }

    // Used to Initialise some properties of the asteroids.
    private void InitializeAsteroid()
    {
        AsteroidSprite.sprite = BigSprites[UnityEngine.Random.Range(0, BigSprites.Length)];
        if (gameObject.GetComponent<PolygonCollider2D>() == null)
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
        size = Size.Big;
        transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.value * 360);
    }

    private void Update()
    {
        if(health <= 0)
        {
            BreakAsteroid();
        }
    }

    // Adds force to the asteroid in given direction.
    public void SetTrajectory(Vector2 direction)
    {
        AsteroidRigidBody.AddForce(direction * speed);
    }

    // Takes damage on collision with a bullet.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    // decreases health according to the given value.
    private void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void BreakAsteroid()
    {
        OnAsteroidDestroy.Invoke(transform.position);

        if (size == Size.Big)
        {
            Split(Size.Medium);
            Split(Size.Medium);
        } else if(size == Size.Medium)
        {
            Split(Size.Small);
            Split(Size.Small);
        }
        SoundManager.Instance.PlaySoundEffects2(SoundType.AsteroidExplosion);
        Destroy(gameObject);
    }

    private void Split(Size sizeToCreate)
    {
        Vector2 position = transform.position;
        position += UnityEngine.Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate<Asteroid>(this, position, transform.rotation);

        if(sizeToCreate == Size.Medium)
        {
            half.AsteroidSprite.sprite = MediumSprites[UnityEngine.Random.Range(0, MediumSprites.Length)];
            half.size = Size.Medium;
            Destroy(half.GetComponent<PolygonCollider2D>());
            half.gameObject.AddComponent<PolygonCollider2D>();
            half.health = 60;
        } else if(sizeToCreate == Size.Small)
        {
            half.AsteroidSprite.sprite = SmallSprites[UnityEngine.Random.Range(0, SmallSprites.Length)];
            half.size = Size.Small;
            Destroy(half.GetComponent<PolygonCollider2D>());
            half.gameObject.AddComponent<PolygonCollider2D>();
            half.health = 20;
        }

        half.SetTrajectory(UnityEngine.Random.insideUnitSphere.normalized * speed);
    }
}

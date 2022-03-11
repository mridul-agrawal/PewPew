using PewPew.Audio;
using PewPew.Player;
using System;
using UnityEngine;

namespace PewPew.Asteroids
{
    /// <summary>
    /// This class is responisble for managing the lifecycle of an Asteroid gameObject. 
    /// </summary>
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
        public static event Action<Bullet> OnBulletDestroy;


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
            if (health <= 0)
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
                TakeDamage(collision.gameObject.GetComponent<Bullet>().Damage);
                OnBulletDestroy?.Invoke(collision.GetComponent<Bullet>());
            }
        }

        // decreases health according to the given value.
        private void TakeDamage(int damage)
        {
            health -= damage;
        }

        // Breaks this Asteroid and Splits it according to its size.
        private void BreakAsteroid()
        {
            OnAsteroidDestroy.Invoke(transform.position);

            if (size == Size.Big)
            {
                Split(Size.Medium);
                Split(Size.Medium);
            }
            else if (size == Size.Medium)
            {
                Split(Size.Small);
                Split(Size.Small);
            }
            SoundManager.Instance.PlaySoundEffects2(SoundType.AsteroidExplosion);
            Destroy(gameObject);
        }

        // Used to instantiate and initialilse an asteroid of the given size when splitting from a bigger Asteroid.
        private void Split(Size sizeToCreate)
        {
            Vector2 position = transform.position;
            position += UnityEngine.Random.insideUnitCircle * 0.5f;

            Asteroid half = Instantiate<Asteroid>(this, position, transform.rotation);

            if (sizeToCreate == Size.Medium)
            {
                half.AsteroidSprite.sprite = MediumSprites[UnityEngine.Random.Range(0, MediumSprites.Length)];
                half.size = Size.Medium;
                half.health = 60;
            }
            else if (sizeToCreate == Size.Small)
            {
                half.AsteroidSprite.sprite = SmallSprites[UnityEngine.Random.Range(0, SmallSprites.Length)];
                half.size = Size.Small;
                half.health = 20;
            }
            Destroy(half.GetComponent<PolygonCollider2D>());
            half.gameObject.AddComponent<PolygonCollider2D>();
            half.SetTrajectory(UnityEngine.Random.insideUnitSphere.normalized * speed);
        }
    }
}
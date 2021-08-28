using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] BigSprites;
    public Sprite[] MediumSprites;
    public Sprite[] SmallSprites;

    private enum Size
    {
        Big,
        Medium,
        Small
    }
    private Size size;
    public float speed = 10;
    public int Strength = 100;

    private SpriteRenderer AsteroidSprite;
    private Rigidbody2D AsteroidRigidBody;

    private void Awake()
    {
        AsteroidSprite = GetComponent<SpriteRenderer>();
        AsteroidRigidBody = GetComponent<Rigidbody2D>();

        AsteroidSprite.sprite = BigSprites[Random.Range(0, BigSprites.Length)];
        if(gameObject.GetComponent<PolygonCollider2D>() == null)
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
        size = Size.Big;
        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
    }

    private void Update()
    {
        if(Strength <= 0)
        {
            BreakAsteroid();
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        AsteroidRigidBody.AddForce(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        Strength -= damage;
    }

    private void BreakAsteroid()
    {
        if(size == Size.Big)
        {
            Split(Size.Medium);
            Split(Size.Medium);
        } else if(size == Size.Medium)
        {
            Split(Size.Small);
            Split(Size.Small);
        }
        Destroy(gameObject);
    }

    private void Split(Size sizeToCreate)
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate<Asteroid>(this, position, transform.rotation);

        if(sizeToCreate == Size.Medium)
        {
            Debug.Log("After Instantiating");
            half.AsteroidSprite.sprite = MediumSprites[Random.Range(0, MediumSprites.Length)];
            half.size = Size.Medium;
            Destroy(half.GetComponent<PolygonCollider2D>());
            half.gameObject.AddComponent<PolygonCollider2D>();
            half.Strength = 60;
        } else if(sizeToCreate == Size.Small)
        {
            half.AsteroidSprite.sprite = SmallSprites[Random.Range(0, SmallSprites.Length)];
            half.size = Size.Small;
            Destroy(half.GetComponent<PolygonCollider2D>());
            half.gameObject.AddComponent<PolygonCollider2D>();
            half.Strength = 20;
        }

        half.SetTrajectory(Random.insideUnitSphere.normalized * speed);
    }

}

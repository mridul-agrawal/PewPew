using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed = 10;

    private SpriteRenderer AsteroidSprite;
    private Rigidbody2D AsteroidRigidBody;

    private void Awake()
    {
        AsteroidSprite = GetComponent<SpriteRenderer>();
        AsteroidRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        AsteroidSprite.sprite = sprites[Random.Range(0,sprites.Length)];
        transform.eulerAngles = new Vector3(0,0,Random.value * 360);
    }

    public void SetTrajectory(Vector2 direction)
    {
        AsteroidRigidBody.AddForce(direction * speed);
    }
}

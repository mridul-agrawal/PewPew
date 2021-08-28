using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage = 10;
    private void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed);
    }

}

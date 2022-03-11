using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for a player bullet gameObject.
/// </summary>
namespace PewPew.Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed;
        [SerializeField]
        private int damage = 10;

        public int Damage { get => damage; }

        private void Update()
        {
            transform.Translate(Vector2.up * Time.deltaTime * bulletSpeed);
        }
    }
}
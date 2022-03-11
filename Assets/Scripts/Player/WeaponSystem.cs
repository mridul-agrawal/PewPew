using PewPew.Asteroids;
using PewPew.Audio;
using PewPew.Pooling;
using System.Collections;
using UnityEngine;


namespace PewPew.Player
{
    /// <summary>
    /// This Class is Responsible for handling the weapon system of player ship.
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        // Variables:
        [SerializeField] private float fireRate = 0.2f;
        private Coroutine firingRoutine = null;

        // References:
        [SerializeField] private GameObject Bullet;
        [SerializeField] private Transform weaponPoint1;
        [SerializeField] private Transform weaponPoint2;
        private ObjectPoolBullet objectPoolBullet;

        private void Start()
        {
            objectPoolBullet = GetComponent<ObjectPoolBullet>();
            Asteroid.OnBulletDestroy += ReturnBulletToPool;
        }

        private void Update()
        {
            HandleShooting();
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
                FireBulletAtPosition(weaponPoint1);
                FireBulletAtPosition(weaponPoint2);
                SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerShoot);
                yield return new WaitForSeconds(fireRate);
            }
        }

        // Fires a bullet from the given position.
        private void FireBulletAtPosition(Transform weaponPoint)
        {
            Bullet bullet1 = objectPoolBullet.GetBullet(Bullet);
            bullet1.transform.position = weaponPoint.position;
            bullet1.transform.rotation = weaponPoint.rotation;
            bullet1.gameObject.SetActive(true);
        }

        // Disables and returns back given bullet to the pool.
        private void ReturnBulletToPool(Bullet bulletToDestroy)
        {
            bulletToDestroy.gameObject.SetActive(false);
            objectPoolBullet.ReturnItem(bulletToDestroy);
        }

    }
}

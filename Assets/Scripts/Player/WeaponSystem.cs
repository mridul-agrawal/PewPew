using PewPew.Audio;
using System.Collections;
using System.Collections.Generic;
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
                Instantiate(Bullet, weaponPoint1.position, weaponPoint1.rotation);
                Instantiate(Bullet, weaponPoint2.position, weaponPoint2.rotation);
                SoundManager.Instance.PlaySoundEffects2(SoundType.PlayerShoot);
                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}

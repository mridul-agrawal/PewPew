using UnityEngine;
using PewPew.Player;

namespace PewPew.Pooling
{
    /// <summary>
    /// This class implements an object pool for Bullets using GenericObjectPoolService.
    /// </summary>
    public class ObjectPoolBullet : GenericObjectPoolService<Bullet>
    {
        GameObject bulletPrefab;

        public Bullet GetBullet(GameObject bulletPrefab)
        {
            this.bulletPrefab = bulletPrefab;
            return GetItem();
        }

        protected override Bullet CreateItem()
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            return newBullet.GetComponent<Bullet>();
        }
    }
}
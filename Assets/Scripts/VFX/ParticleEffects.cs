using UnityEngine;
using PewPew.Utilities;

namespace PewPew.VFX
{
    public class ParticleEffects : SingletonGeneric<ParticleEffects>
    {
        // References:
        [SerializeField] private ParticleSystem explosion;

        internal void PlayExplosionAt(Vector3 position)
        {
            explosion.transform.position = position;
            explosion.Play();
        }
    }
}
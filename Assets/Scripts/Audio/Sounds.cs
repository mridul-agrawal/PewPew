using UnityEngine;
using System;

/// <summary>
/// This class is used to bind a soundType with an audio clip.
/// </summary>

namespace PewPew.Audio
{
    [Serializable]
    public class Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }
}
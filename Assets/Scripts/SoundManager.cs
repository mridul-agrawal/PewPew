using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    public AudioSource audioEffects;
    public AudioSource audioEffects2;
    public AudioSource audioMusic;

    public Sounds[] AudioList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffects(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioEffects.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public void PlaySoundEffects2(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioEffects2.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public void PlayBackgroundMusic(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioMusic.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public AudioClip GetSoundClip(SoundType soundType)
    {
        Sounds st = Array.Find(AudioList, item => item.soundType == soundType);
        if (st != null)
        {
            return st.audio;
        }
        return null;
    }

    public void StopSoundEffect()             // Sets the audio clip to null.
    {
        audioEffects.Stop();
        audioEffects.clip = null;
    }

    public void StopSoundEffect2()             // Sets the audio clip to null.
    {
        audioEffects2.Stop();
        audioEffects2.clip = null;
    }

}


[Serializable]
public class Sounds
{
    public SoundType soundType;
    public AudioClip audio;
}

public enum SoundType
{
    BackgroundMusic1,
    ButtonClick1,
    ButtonClick2,
    PlayerMove,
    PlayerShoot,
    PlayerDeath,
    AsteroidExplosion,
    GameOver,
}
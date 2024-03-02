using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Sounds
{
    public AudioClip[] audioClips;
    public float soundScale = 1f;
    public Sound sound;
}

public enum Sound
{
    EnemyHit,
    Coin,
    Fruit,
    PlayerHit,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource _audioSource;

    [SerializeField] private List<Sounds> sounds;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(Sound sound)
    {
        _audioSource.PlayOneShot(GetSound(sound), GetVolumeScale(sound));
    }

    private AudioClip GetSound(Sound sound)
    {
        if (sounds.FirstOrDefault(x => x.sound == sound)?.audioClips is { } clips)
            return clips[Random.Range(0, clips.Length - 1)];

        return null;
    }

    private float GetVolumeScale(Sound sound)
    {
        return sounds.FirstOrDefault(x => x.sound == sound)!.soundScale;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioPair
{
    public AudioClip audio;
    public string name;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioPair[] sounds;

    private AudioSource backgroundSource;

    [SerializeField]
    private Dictionary<string, AudioSource> channels;

    [Range(0, 1)]
    public float SFXvolume = 1f;
    [Range(0, 1)]
    public float musicVolume = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        backgroundSource = GetComponent<AudioSource>();

        channels = new Dictionary<string, AudioSource>();
        channels.Add("Weapon", transform.GetChild(0).GetComponent<AudioSource>());

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        backgroundSource.volume = musicVolume;
        backgroundSource.Play();
    }

    public void PlaySound(string name, string source)
    {
        foreach (var item in sounds)
        {
            if (item.name == name)
            {
                channels[source].volume = SFXvolume;
                channels[source].clip = item.audio;
                channels[source].Play();
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        backgroundSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var item in channels)
        {
            item.Value.volume = Mathf.Clamp01(volume);
        }
    }
}

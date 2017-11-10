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
        backgroundSource.Play();
    }

    public void PlaySound(string name, string source)
    {
        foreach (var item in sounds)
        {
            if (item.name == name)
            {
                channels[source].clip = item.audio;
                channels[source].Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioPair
{
    public AudioClip audio;
    public string name;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    [SerializeField]
    private AudioPair[] sounds;

    //private AudioSource backgroundSource;

    [SerializeField]
    private Dictionary<string, AudioSource> channels;

    [Range(0, 1)]
    public float SFXvolume = 1f;
    [Range(0, 1)]
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float voiceVolume = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
            Destroy(gameObject);
        if (Instance == null)
            Instance = gameObject.GetComponent<AudioManager>();

        //backgroundSource = GetComponent<AudioSource>();

        channels = new Dictionary<string, AudioSource>();
        channels.Add("SFX", transform.GetChild(0).GetComponent<AudioSource>());
        channels.Add("Music", transform.GetChild(1).GetComponent<AudioSource>());
        channels.Add("Voice", transform.GetChild(2).GetComponent<AudioSource>());
    }

    public void PlayBackgroundMusic()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
            PlaySound("MainMenu", "Music");
        else if (sceneName == "WickedForest")
            PlaySound("WickedForest", "Music");
        else if (sceneName == "OrcArena")
            PlaySound("OrcArena", "Music");
    }

    public static void PlaySound(string name, string source)
    {
        foreach (var item in Instance.sounds)
        {
            if (item.name == name)
            {
                if (source == "SFX")
                    Instance.channels[source].volume = Instance.SFXvolume;
                else if (source == "Music")
                    Instance.channels[source].volume = Instance.musicVolume;
                else if (source == "Voice")
                    Instance.channels[source].volume = Instance.voiceVolume;

                Instance.channels[source].clip = item.audio;
                Instance.channels[source].Play();
            }
        }
    }

    public static void SetVolume(float volume, string channel)
    {
        if (channel == "SFX")
        {
            Instance.SFXvolume = volume;
        }
        else if (channel == "Music")
        {
            Instance.musicVolume = volume;
        }
        else if (channel == "Voice")
        {
            Instance.voiceVolume = volume;
        }

        Instance.channels[channel].volume = volume;
    }

    [System.Obsolete]
    private void SetMusicVolume(float volume)
    {
        //backgroundSource.volume = Mathf.Clamp01(volume);
        musicVolume = volume;
    }

    [System.Obsolete]
    private void SetSFXVolume(float volume)
    {
        SFXvolume = volume;
        foreach (var item in channels)
        {
            item.Value.volume = Mathf.Clamp01(volume);
        }
    }
}

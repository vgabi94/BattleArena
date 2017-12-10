using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Text labelValue;
    public string whatSlider;

    Slider slider;
    AudioManager am;

    // Use this for initialization
    void Start()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>().GetComponent<AudioManager>();
        slider = GetComponent<Slider>();

        if (whatSlider == "SFX")
        {
            labelValue.text = ((int)(am.SFXvolume * 100)).ToString();
            slider.value = am.SFXvolume;
        }
        else if (whatSlider == "Music")
        {
            labelValue.text = ((int)(am.musicVolume * 100)).ToString();
            slider.value = am.musicVolume;
        }
    }

    public void OnValueChange()
    {
        labelValue.text = ((int)(slider.value * 100)).ToString();

        if (whatSlider == "SFX")
            am.SetSFXVolume(slider.value);
        else if (whatSlider == "Music")
            am.SetMusicVolume(slider.value);
    }
}

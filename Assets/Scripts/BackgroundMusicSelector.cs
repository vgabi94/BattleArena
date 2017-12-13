using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicSelector : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBackgroundMusic();
    }
}

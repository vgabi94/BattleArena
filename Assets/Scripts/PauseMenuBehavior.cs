using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehavior : MonoBehaviour
{
    private GameObject audioSettings;
    private GameObject pauseMenu;

    private void Start()
    {
        audioSettings = transform.Find("SoundSettings").gameObject;
        pauseMenu = transform.Find("Panel").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf && !audioSettings.activeSelf)
                pauseMenu.SetActive(false);
            else if (!pauseMenu.activeSelf)
                pauseMenu.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
    }

    public void AudioButton()
    {
        audioSettings.SetActive(true);
    }

    public void SaveButton()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

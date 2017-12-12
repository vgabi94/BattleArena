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
            {
                ResumeButton();
            }
            else if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AudioButton()
    {
        audioSettings.SetActive(true);
    }

    public void SaveButton()
    {
        GameManager.SaveGame();
    }

    public void QuitButton()
    {
        ModalDialog.Show("Exit? Any unsaved progress will be lost!", () => Application.Quit());
    }

    public void QuitToMenuButton()
    {
        ModalDialog.Show("Quit to Main Menu? Any unsaved progress will be lost!", () => { GameManager.LoadLevel("MainMenu"); Time.timeScale = 1f; });
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    public GameObject buttonPrefab;
    private GameObject audioSettings;
    private GameObject listOfGames;
    private GameObject pauseMenu;
    

    private void Start()
    {
        audioSettings = transform.Find("SoundSettings").gameObject;
        pauseMenu = transform.Find("Panel").gameObject;
        listOfGames = transform.Find("ListOfGames").gameObject;
    }

    public void NewGameButton()
    {
        GameManager.LoadLevel(1);
    }

    public void AudioButton()
    {
        audioSettings.SetActive(true);
    }

    public void LoadButton()
    {
        LoadListOfGames();
    }

    private void LoadListOfGames()
    {
        listOfGames.SetActive(true);
        string path = Application.dataPath + "/SavedGames";
        string[] files = System.IO.Directory.GetFiles(path);
        foreach (var item in files)
        {
            if (item.Contains(".meta")) continue;

            string name = GetSaveFileGameName(item);
            var btn = Instantiate(buttonPrefab, listOfGames.transform);
            var btnMngr = btn.GetComponent<ButtonGameLoader>();
            btnMngr.GamePath = item;
            var btnComp = btn.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
            btnComp.text = name;
        }
    }

    public void QuitButton()
    {
        ModalDialog.Show("Are you sure you want to exit?", () => Application.Quit());
    }

    private string GetSaveFileGameName(string path)
    {
        int startIndex = path.IndexOf('_');
        int endIndex = path.LastIndexOf('.');
        string AO = path.Substring(startIndex + 1, endIndex - startIndex - 1);
        double ao;
        double.TryParse(AO, out ao);
        string saveGameName = "SavedGame " + DateTime.FromOADate(ao).ToString();
        return saveGameName;
    }
}

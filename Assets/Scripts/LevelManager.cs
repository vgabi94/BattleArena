using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private HashSet<GameObject> orcs;

    private UIManager ui;

    private void Awake()
    {
        orcs = new HashSet<GameObject>();
        // Find all orcs
        orcs.UnionWith(GameObject.FindGameObjectsWithTag("Enemy"));

        EventObserver.Instance.KillsUpdateEvent += OnKillUpdate;

        ui = GameObject.Find("UI").GetComponent<UIManager>();
    }

    private void OnKillUpdate(GameObject sender, object message)
    {
        orcs.Remove(sender);
        if (orcs.Count == 0)
        {
            AudioManager.PlaySound("YouWin", "Voice");
            ui.ShowWinScreen();
            StartCoroutine(GoToNextLevel());
        }
    }

    private IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName == "WickedForest")
            GameManager.LoadLevel("OrcArena");
        else if (sceneName == "OrcArena")
            GameManager.LoadLevel("MainMenu");
    }
}

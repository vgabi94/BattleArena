using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
    private HashSet<GameObject> orcs;

    private UIManager ui;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        if (Instance == null)
            Instance = gameObject.GetComponent<LevelManager>();

        orcs = new HashSet<GameObject>();
        Reset();

        EventObserver.Instance.KillsUpdateEvent += OnKillUpdate;

        ui = GameObject.Find("UI").GetComponent<UIManager>();
    }

    public void Reset()
    {
        orcs.Clear();
        orcs.UnionWith(GameObject.FindGameObjectsWithTag("Enemy"));
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

    private void OnDestroy()
    {
        EventObserver.Instance.KillsUpdateEvent -= OnKillUpdate;
    }
}

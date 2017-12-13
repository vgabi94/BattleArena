using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text goldIndicator;
    Text ammoIndicator;
    Text killsIndicator;

    GameObject gameOver;
    GameObject win, lose;

    public GameObject floatingTextPrefab;

    private void Awake()
    {
        var obj = transform.Find("GoldIndicator");
        goldIndicator = obj.GetComponent<Text>();

        obj = transform.Find("AmmoIndicator");
        ammoIndicator = obj.GetComponent<Text>();

        obj = transform.Find("KillsIndicator");
        killsIndicator = obj.GetComponent<Text>();

        gameOver = transform.Find("GameOverImage").gameObject;
        win = gameOver.transform.GetChild(1).gameObject;
        lose = gameOver.transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        EventObserver.Instance.GoldUpdateEvent += OnGoldUpdate;
        EventObserver.Instance.AmmoUpdateEvent += OnAmmoUpdate;
        EventObserver.Instance.KillsUpdateEvent += OnKillsUpdate;
        EventObserver.Instance.PlayerDeadEvent += OnPlayerDead;
    }

    private void OnPlayerDead(GameObject sender, object message)
    {
        gameOver.SetActive(true);
        lose.SetActive(true);
    }

    private void OnAmmoUpdate(GameObject sender, object message)
    {
        int ammo, rounds;
        Pistol.UnpackAmmo((int)message, out ammo, out rounds);

        ammoIndicator.text = "Ammo    " + ammo + "/" + rounds;
    }

    private void OnKillsUpdate(GameObject sender, object message)
    {
        killsIndicator.text = "Kills    " + ((int)message).ToString();
        CreateFloatingText("+1 Kill", Color.magenta);
    }

    private void OnGoldUpdate(GameObject sender, object message)
    {
        goldIndicator.text = "Gold    " + ((int)message).ToString();
        CreateFloatingText("+1 Gold", Color.yellow);
    }

    private void OnDestroy()
    {
        EventObserver.Instance.GoldUpdateEvent -= OnGoldUpdate;
        EventObserver.Instance.AmmoUpdateEvent -= OnAmmoUpdate;
        EventObserver.Instance.KillsUpdateEvent -= OnKillsUpdate;
        EventObserver.Instance.PlayerDeadEvent -= OnPlayerDead;
    }

    private void CreateFloatingText(string text, Color color)
    {
        var obj = Instantiate(floatingTextPrefab, transform);
        var ft = obj.GetComponent<Text>();
        ft.text = text;
        ft.color = color;
    }

    public void ShowWinScreen()
    {
        gameOver.SetActive(true);
        win.SetActive(true);
    }
}

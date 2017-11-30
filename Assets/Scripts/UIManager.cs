using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text goldIndicator;
    Text ammoIndicator;
    Text killsIndicator;

    private void Awake()
    {
        var obj = transform.Find("GoldIndicator");
        goldIndicator = obj.GetComponent<Text>();

        obj = transform.Find("AmmoIndicator");
        ammoIndicator = obj.GetComponent<Text>();

        obj = transform.Find("KillsIndicator");
        killsIndicator = obj.GetComponent<Text>();
    }

    private void Start()
    {
        EventObserver.Instance.GoldUpdateEvent += OnGoldUpdate;
        EventObserver.Instance.AmmoUpdateEvent += OnAmmoUpdate;
        EventObserver.Instance.KillsUpdateEvent += OnKillsUpdate;
    }

    private void OnAmmoUpdate(GameObject sender, object message)
    {
        int ammo, rounds;
        Pistol.UnpackAmmo((int)message, out ammo, out rounds);

        ammoIndicator.text = "Ammo    " + ammo + "/" + rounds;
    }

    private void OnKillsUpdate(GameObject sender, object message)
    {
        goldIndicator.text = "Kills    " + ((int)message).ToString();
    }

    private void OnGoldUpdate(GameObject sender, object message)
    {
        goldIndicator.text = "Gold    " + ((int)message).ToString();
    }

    private void OnDestroy()
    {
        EventObserver.Instance.GoldUpdateEvent -= OnGoldUpdate;
        EventObserver.Instance.AmmoUpdateEvent -= OnAmmoUpdate;
        EventObserver.Instance.KillsUpdateEvent -= OnKillsUpdate;
    }
}

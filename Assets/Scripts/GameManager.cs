using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerKills = 0;

    public int PlayerKills
    {
        get { return playerKills; }
        set
        {
            playerKills = value;
            EventObserver.Instance.Notify(ObservableEvents.KillsUpdate, gameObject, value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObservableEvents
{
    GoldUpdate, AmmoUpdate, KillsUpdate
}

public class EventObserver
{
    private EventObserver() { }

    private static EventObserver instance;

    public static EventObserver Instance
    {
        get
        {
            if (instance == null)
                instance = new EventObserver();
            return instance;
        } 
    }

    public delegate void SenderTarget(GameObject sender, GameObject target);
    public delegate void SenderMessage(GameObject sender, object message);

    public event SenderMessage GoldUpdateEvent;
    public event SenderMessage AmmoUpdateEvent;
    public event SenderMessage KillsUpdateEvent;

    public void Notify(ObservableEvents ev, params object[] args)
    {
        switch (ev)
        {
            case ObservableEvents.GoldUpdate:
                if (GoldUpdateEvent != null)
                    GoldUpdateEvent((GameObject)args[0], args[1]);
                break;
            case ObservableEvents.AmmoUpdate:
                if (AmmoUpdateEvent != null)
                    AmmoUpdateEvent((GameObject)args[0], args[1]);
                break;
            case ObservableEvents.KillsUpdate:
                if (KillsUpdateEvent != null)
                    KillsUpdateEvent((GameObject)args[0], args[1]);
                break;
            default:
                break;
        }
    }
}

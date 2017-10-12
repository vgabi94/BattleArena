using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObservableEvents
{
    
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

    public void Notify(ObservableEvents ev, params object[] args)
    {
        
    }
}

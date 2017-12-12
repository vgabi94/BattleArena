using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnDeath()
    {
        EventObserver.Instance.Notify(ObservableEvents.PlayerDead, gameObject, null);
    }
}

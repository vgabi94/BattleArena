using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text goldIndicator;

    private void Awake()
    {
        var obj = transform.Find("GoldIndicator");
        Debug.Assert(obj != null, "GoldIndicator is missing");
        goldIndicator = obj.GetComponent<Text>();
    }

    private void Start()
    {
        EventObserver.Instance.GoldUpdateEvent += OnGoldUpdate;
	}

    private void OnGoldUpdate(GameObject sender, object message)
    {
        goldIndicator.text = "Gold    " + ((int)message).ToString();
    }

    private void OnDestroy()
    {
        EventObserver.Instance.GoldUpdateEvent -= OnGoldUpdate;
    }
}

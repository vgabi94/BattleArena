using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    Image image;
    GameObject target;
    Health hc;

    private void Awake()
    {
        image = GetComponent<Image>();
        target = transform.parent.parent.gameObject;
        Debug.Assert(target != null, "This component must have at least two parents");
        hc = target.GetComponent<Health>();
    }

    void HPChanged(object value)
    {
        float hp = (float)value;
        image.fillAmount = hp / hc.maxHP;
    }
}

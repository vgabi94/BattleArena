using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP = 100f;
    public float minHP = 0f;

    private float hpValue;

    public float HP
    {
        get { return hpValue; }
        set
        {
            hpValue = Mathf.Clamp(value, minHP, maxHP);
            BroadcastMessage("HPChanged", hpValue, SendMessageOptions.DontRequireReceiver);
        }
    }

    void ApplyDamage(float damage, GameObject sender)
    {
        HP -= damage;
        BroadcastMessage("HitBy", sender, SendMessageOptions.DontRequireReceiver);
    }
}

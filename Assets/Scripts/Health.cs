using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    public float maxHP = 100f;
    public float minHP = 0f;

    private float hpValue;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        HP = maxHP;
    }

    public float HP
    {
        get { return hpValue; }
        set
        {
            hpValue = Mathf.Clamp(value, minHP, maxHP);
            BroadcastMessage("HPChanged", hpValue, SendMessageOptions.DontRequireReceiver);

            if (hpValue == minHP)
            {
                anim.SetBool("die", true);
            }
        }
    }

    private void DeathAnimationFinished()
    {
        BroadcastMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }

    public void ApplyDamage(float damage, Vector3 hitLocation, GameObject sender)
    {
        HP -= damage;
        BroadcastMessage("HitBy", sender, SendMessageOptions.DontRequireReceiver);
        BroadcastMessage("HitLocation", hitLocation, SendMessageOptions.DontRequireReceiver);
    }
}

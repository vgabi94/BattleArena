using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon MainWeapon { get; private set; }
    public Transform PistolOffset { get; private set; }

    private void Start()
    {
        PistolOffset = transform.Find("PistollOffset");
    }

    public void SwitchWeapon(Weapon wep)
    {

    }
}

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

    private void Update()
    {
        if (MainWeapon)
        {
            MainWeapon.UpdateWeapon();
        }
    }

    public void SwitchWeapon(Weapon wep)
    {
        MainWeapon = wep;
    }
    
    public void UpdateAmmo(int packed)
    {
        if (MainWeapon != null)
        {
            // We know that we have only one weapon type
            Pistol p = (Pistol)MainWeapon;
            p.AddAmmo(packed);
        }
    }
}

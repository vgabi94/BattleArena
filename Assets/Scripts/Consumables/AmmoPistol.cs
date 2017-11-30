using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPistol : Consumable
{
    public int ammo = 12;
    public int rounds = 6;

    public override void Use(GameObject target)
    {
        WeaponController wc = target.GetComponent<WeaponController>();
        int packed = Pistol.PackAmmo(ammo, rounds);
        wc.UpdateAmmo(packed);
        Destroy(gameObject);
    }
}

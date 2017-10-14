using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class Inventory : MonoBehaviour
{
    private List<Weapon> weapons;
    private WeaponController wc;

    private void Awake()
    {
        wc = GetComponent<WeaponController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.gameObject.GetComponent<Item>();

        if (item.TypeOfItem == ItemType.Weapon)
        {
            var wep = (Weapon)item;
            if (!HasWeaponOfType(wep.TypeOfWeapon))
            {
                wc.SwitchWeapon(wep);
                weapons.Add(wep);
            }
        }
        else
        {
            if (item.UseOnPickUp)
            {
                item.Use();
            }
        }
    }

    private bool HasWeaponOfType(WeaponType type)
    {
        foreach (var item in weapons)
        {
            if (item.TypeOfWeapon == type)
                return true;
        }
        return false;
    }
}

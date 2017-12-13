using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class Inventory : MonoBehaviour
{
    private List<Weapon> weapons;
    private WeaponController wc;

    private int goldAmount;
    public int Gold
    {
        get { return goldAmount; }
        set
        {
            if (value > goldAmount)
                goldAmount = value;

            EventObserver.Instance.Notify(ObservableEvents.GoldUpdate, gameObject, goldAmount);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        wc = GetComponent<WeaponController>();
        weapons = new List<Weapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        
        if (item != null)
        {
            if (item.TypeOfItem == ItemType.Weapon)
            {
                var wep = (Weapon)item;
                if (!HasWeaponOfType(wep.TypeOfWeapon))
                {
                    wc.SwitchWeapon(wep);
                    weapons.Add(wep);
                    wep.PickUp(gameObject);
                }
            }
            else
            {
                if (item.UseOnPickUp)
                {
                    item.Use(gameObject);
                }
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

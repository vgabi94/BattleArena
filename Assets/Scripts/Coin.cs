using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int coinValue = 1;

    protected virtual void Awake()
    {
        TypeOfItem = ItemType.Coin;
        UseOnPickUp = true;
    }

    public override void Use(GameObject target)
    {
        var inv = target.GetComponent<Inventory>();
        inv.Gold += coinValue;
        Destroy(gameObject);
    }
}

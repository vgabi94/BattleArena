using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon, Consumable, Coin
}

public abstract class Item : MonoBehaviour
{
    public bool UseOnPickUp { get; protected set; }

    public ItemType TypeOfItem { get; protected set; }

    public abstract void Use();
}

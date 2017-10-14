using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    protected virtual void Awake()
    {
        UseOnPickUp = true;
    }
}

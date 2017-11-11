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
        if (inv != null)
        {
            inv.Gold += coinValue;
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        if (StateOfItem == ItemState.Dropped)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Use(GameObject target)
    {
        
    }

    public override void PickUp(GameObject target)
    {
        StateOfItem = ItemState.Picked;
        var wc = target.GetComponent<WeaponController>();
        gameObject.transform.position = wc.PistolOffset.position;
        gameObject.transform.rotation = wc.PistolOffset.rotation;
        gameObject.transform.SetParent(target.transform, true);
    }
}

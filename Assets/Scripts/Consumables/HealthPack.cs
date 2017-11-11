using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Consumable
{
    public float HPValue = 10f;

    public override void Use(GameObject target)
    {
        var h = target.GetComponent<Health>();
        h.HP += HPValue;
        Destroy(gameObject);
    }

    protected override void Update()
    {
        if (StateOfItem == ItemState.Dropped)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}

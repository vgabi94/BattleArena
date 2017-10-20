using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public float damagePoints = 10f;

    protected override void Awake()
    {
        base.Awake();
        TypeOfWeapon = WeaponType.Pistol;
    }

    public override void Use(GameObject target)
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var hp = hit.collider.GetComponent<Health>();
            if (hp)
            {
                hp.ApplyDamage(damagePoints, target);
            }
        }
    }

    public override void UpdateWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Use(transform.parent.gameObject);
        }
    }

    public override void PickUp(GameObject target)
    {
        StateOfItem = ItemState.Picked;

        var wc = target.GetComponent<WeaponController>();
        gameObject.transform.position = wc.PistolOffset.position;
        gameObject.transform.rotation = wc.PistolOffset.rotation;
        gameObject.transform.SetParent(target.transform, true);

        var anim = target.GetComponent<Animator>();
        anim.SetInteger("weapon", (int)TypeOfWeapon);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
    }
}

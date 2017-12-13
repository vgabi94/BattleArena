using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public float damagePoints = 10f;
    public int MaxAmmoPerRound = 45;
    public int MaxRounds = 60;

    private ParticleSystem[] ps;
    private int ammo;
    private int rounds;

    protected override void Awake()
    {
        base.Awake();
        TypeOfWeapon = WeaponType.Pistol;
        ps = GetComponentsInChildren<ParticleSystem>();
        ammo = MaxAmmoPerRound;
        rounds = MaxRounds;
    }

    public override void Use(GameObject target)
    {
        if (ammo == 0 && rounds == 0)
        {
            AudioManager.PlaySound("NoAmmo", "SFX");
            return;
        }

        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        AudioManager.PlaySound("Pistol", "SFX");

        ammo -= 1;
        if (ammo == 0 && rounds > 0)
        {
            ammo = MaxAmmoPerRound;
            rounds -= 1;
        }
        EventObserver.Instance.Notify(ObservableEvents.AmmoUpdate, gameObject, PackAmmo(ammo, rounds));

        foreach (var item in ps)
        {
            item.Play();
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var hp = hit.collider.GetComponent<Health>();
            
            if (hp)
            {
                hp.ApplyDamage(damagePoints, hit.point, target);
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
        EventObserver.Instance.Notify(ObservableEvents.AmmoUpdate, gameObject, PackAmmo(ammo, rounds));
    }

    public override void SetAmmo(int packed)
    {
        int am, ro;
        UnpackAmmo(packed, out am, out ro);
        ammo = MaxAmmoPerRound = am;
        rounds = MaxRounds = ro;
        EventObserver.Instance.Notify(ObservableEvents.AmmoUpdate, gameObject, packed);
    }

    public override void AddAmmo(int packed)
    {
        int am, ro;
        UnpackAmmo(packed, out am, out ro);
        ammo = MaxAmmoPerRound += am;
        rounds = MaxRounds += ro;
        packed = PackAmmo(ammo, rounds);
        EventObserver.Instance.Notify(ObservableEvents.AmmoUpdate, gameObject, packed);
    }

    /// <summary>
    /// Helper method for packing two shorts into an int
    /// </summary>
    public static int PackAmmo(int ammo, int rounds)
    {
        int ammoMask = 0x0000FFFF;
        ammo = ammoMask & ammo;
        rounds = ammoMask & rounds;
        rounds = (rounds << 16);
        int packed = ammo | rounds;
        return packed;
    }

    public static void UnpackAmmo(int packed, out int ammo, out int rounds)
    {
        int ammoMask = 0x0000FFFF;
        ammo = packed & ammoMask;
        rounds = (packed >> 16) & ammoMask;
    }

    public override int GetAmmo()
    {
        return PackAmmo(ammo, rounds);
    }
}

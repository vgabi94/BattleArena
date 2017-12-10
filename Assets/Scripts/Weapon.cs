using UnityEngine;

public enum WeaponType
{
    Pistol, Machinegun, RocketLauncher, None
}

public class Weapon : Item
{
    public Weapon()
    {
        TypeOfWeapon = WeaponType.None;
    }

    public int Level { get; protected set; }

    public WeaponType TypeOfWeapon { get; protected set; }

    public override void Use(GameObject target) { }

    protected virtual void Awake()
    {
        TypeOfItem = ItemType.Weapon;
        UseOnPickUp = false;
    }

    public virtual void UpdateWeapon() { }

    public virtual int GetAmmo() { return 0; }

    public virtual void SetAmmo(int packed) { }
}

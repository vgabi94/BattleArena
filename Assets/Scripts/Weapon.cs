using UnityEngine;

public enum WeaponType
{
    Pistol, Machinegun, RocketLauncher
}

public abstract class Weapon : Item
{
    public int Level { get; protected set; }

    public WeaponType TypeOfWeapon { get; protected set; }

    protected virtual void Awake()
    {
        TypeOfItem = ItemType.Weapon;
        UseOnPickUp = false;
    }
}

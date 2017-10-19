using UnityEngine;

public enum ItemType
{
    Weapon, Consumable, Coin
}

public enum ItemState
{
    Dropped, Picked
}

public abstract class Item : MonoBehaviour
{
    public bool UseOnPickUp { get; protected set; }

    public float rotationSpeed = 50f;

    public ItemType TypeOfItem { get; protected set; }

    public ItemState StateOfItem { get; protected set; }

    public abstract void Use(GameObject target);

    public virtual void PickUp(GameObject target) { }

    protected virtual void Update()
    {
        if (StateOfItem == ItemState.Dropped)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}

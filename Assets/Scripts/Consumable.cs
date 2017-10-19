public abstract class Consumable : Item
{
    protected virtual void Awake()
    {
        TypeOfItem = ItemType.Consumable;
        UseOnPickUp = true;
    }
}

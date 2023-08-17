using UniRx;
namespace Game.Scripts.Model
{
    public class DatabusInventory
    {
        public InventoryItem EquippedItem
        {
            get => equippedItemReactive.Value;
            set => equippedItemReactive.Value = value;
        }

        public int Cash
        {
            get => cashReactive.Value;
            set => cashReactive.Value = value;
        }

        public readonly ReactiveProperty<InventoryItem> equippedItemReactive = new();
        public readonly ReactiveProperty<int> cashReactive = new(0);
        public readonly ReactiveCollection<InventoryItem> inventoryItems = new();
    }

    public class InventoryItem
    {
        public string itemName;
    }
}
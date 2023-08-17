using Game.Scripts.Model;
using System;
namespace Game.Scripts.Controller
{
    public class PurchaseController
    {
        public void Purchase(Action success = null, Action fail = null)
        {
            DatabusInventory inventory = Service<DatabusInventory>.Get();
            DatabusStore store = Service<DatabusStore>.Get();
            StoreItem itemToPurchase = store.SelectedItem;

            if (inventory.cash < itemToPurchase.price)
            {
                fail?.Invoke();
                return;
            }

            inventory.cash -= itemToPurchase.price;
            inventory.inventoryItems.Add(new InventoryItem()
            {
                itemName = itemToPurchase.itemName
            });

            success?.Invoke();
        }
    }
}
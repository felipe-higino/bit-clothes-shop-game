using Game.Scripts.Controller;
using Game.Scripts.Model;
using UnityEngine;
namespace Game.Scripts.Tools
{
    public class InventoryShowcase : MonoBehaviour
    {
        DatabusInventory _Inventory =>
            Service<DatabusInventory>.Get();

        void Awake()
        {
            _Inventory.cash = 5000;
        }
    }
}
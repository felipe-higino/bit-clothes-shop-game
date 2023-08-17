using Game.Scripts.Controller;
using Game.Scripts.Model;
using UnityEngine;
namespace Game.Scripts.Tools
{
    public class ShopShowcase : MonoBehaviour
    {
        DatabusInventory _Inventory =>
            Service<DatabusInventory>.Get();

        void Awake()
        {
            _Inventory.Cash = 5000;
        }
    }
}
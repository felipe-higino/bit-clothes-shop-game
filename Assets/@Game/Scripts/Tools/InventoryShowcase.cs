using Game.Scripts.Controller;
using Game.Scripts.Model;
using TMPro;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Tools
{
    public class InventoryShowcase : MonoBehaviour
    {
        [SerializeField] TMP_Text _cashText;

        DatabusInventory _Inventory =>
            Service<DatabusInventory>.Get();

        void Awake()
        {
            _Inventory.cash = 5;

            _Inventory.cashReactive
                .ObserveOnMainThread()
                .Subscribe(cash => _cashText.text = $"${cash}")
                .AddTo(this);
        }

        [ContextMenu("give cash")]
        public void GiveCash()
        {
            _Inventory.cash += 200;
        }
    }
}
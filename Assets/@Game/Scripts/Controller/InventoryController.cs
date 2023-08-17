using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] InventoryItemWidget _inventoryWidgetPrefab;
        [SerializeField] Transform _inventoryItemsParent;

        readonly Dictionary<InventoryItem, InventoryItemWidget> _itemWidget = new();

        void Awake()
        {
            DatabusInventory inventory = Service<DatabusInventory>.Get();

            inventory.inventoryItems
                .ObserveAdd()
                .ObserveOnMainThread()
                .Subscribe(x =>
                {
                    InventoryItem item = x.Value;

                    //TODO: change to a pool approach
                    InventoryItemWidget widgetInstance = Instantiate(_inventoryWidgetPrefab, _inventoryItemsParent);

                    Sprite sprite = item.itemName.SpriteFromItemName();
                    widgetInstance.SetIcon(sprite);
                    widgetInstance.SetState(InventoryItemWidget.State.AVAILABLE);

                    _itemWidget.Add(item, widgetInstance);
                })
                .AddTo(this);

            inventory.inventoryItems
                .ObserveRemove()
                .ObserveOnMainThread()
                .Subscribe(x =>
                {
                    InventoryItem item = x.Value;
                    _itemWidget.TryGetValue(item, out InventoryItemWidget value);
                    if (null != value)
                    {
                        //TODO: change to a pool approach
                        Destroy(value.gameObject);
                    }
                })
                .AddTo(this);
        }

    }
}
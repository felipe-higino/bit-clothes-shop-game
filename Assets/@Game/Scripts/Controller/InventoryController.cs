using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Controller
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] InventoryItemWidget _inventoryWidgetPrefab;
        [SerializeField] Transform _inventoryItemsParent;
        [SerializeField] Image _img_equippedSet;
        [SerializeField] GroupSelector _inventoryGroupSelector;

        readonly Dictionary<InventoryItem, InventoryItemWidget> _dictItemWidget = new();
        readonly Dictionary<InventoryItemWidget, InventoryItem> _dictWidgetItem = new();

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

                    _dictItemWidget.Add(item, widgetInstance);
                    _dictWidgetItem.Add(widgetInstance, item);
                    _inventoryGroupSelector.AddSelectable(widgetInstance);
                })
                .AddTo(this);

            inventory.inventoryItems
                .ObserveRemove()
                .ObserveOnMainThread()
                .Subscribe(x =>
                {
                    InventoryItem item = x.Value;
                    _dictItemWidget.TryGetValue(item, out InventoryItemWidget widgetInstance);
                    if (null != widgetInstance)
                    {
                        //TODO: change to a pool approach
                        Destroy(widgetInstance.gameObject);
                        _inventoryGroupSelector.RemoveSelectable(widgetInstance);
                        _dictItemWidget.Remove(item);
                        _dictWidgetItem.Remove(widgetInstance);
                    }
                })
                .AddTo(this);

            inventory.equippedItemReactive
                .ObserveOnMainThread()
                .Subscribe(equippedItem =>
                {
                    Sprite sprite;
                    if (null == equippedItem)
                        sprite = "default".SpriteFromItemName();
                    else
                        sprite = equippedItem.itemName.SpriteFromItemName();

                    _img_equippedSet.sprite = sprite;
                })
                .AddTo(this);

            OnChangeSelected(_inventoryGroupSelector.CurrentSelected);
            _inventoryGroupSelector.OnChangeSelected += OnChangeSelected;
        }

        void OnDestroy()
        {
            _inventoryGroupSelector.OnChangeSelected -= OnChangeSelected;
        }

        void OnChangeSelected(GroupSelector.ISelectable selected)
        {
            if (selected is InventoryItemWidget widget)
            {
                _dictWidgetItem.TryGetValue(widget, out InventoryItem inventoryItem);
                if (null != inventoryItem)
                {
                    Service<DatabusInventory>.Get().EquippedItem = inventoryItem;
                }
            }
        }

    }
}
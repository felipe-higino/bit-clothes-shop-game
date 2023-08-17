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
        [SerializeField] InventoryItemWidget _defaultSet;
        [SerializeField] Transform _inventoryItemsParent;
        [SerializeField] Image _img_equippedSet;
        [SerializeField] GroupSelector _inventoryGroupSelector;

        readonly Dictionary<InventoryItem, InventoryItemWidget> _dictItemWidget = new();
        readonly Dictionary<InventoryItemWidget, InventoryItem> _dictWidgetItem = new();

        void Awake()
        {
            _inventoryGroupSelector.OnChangeSelected += OnChangeSelected;
            _defaultSet.OnSelectThis += OnSelectDefaultSet;
            _inventoryGroupSelector.AddSelectable(_defaultSet);

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
                    if (null == equippedItem)
                    {
                        Sprite sprite = "default".SpriteFromItemName();
                        _defaultSet.SelectThis();
                        _img_equippedSet.sprite = sprite;
                    }
                    else
                    {
                        Sprite sprite = equippedItem.itemName.SpriteFromItemName();
                        _img_equippedSet.sprite = sprite;
                    }
                })
                .AddTo(this);
        }

        void OnDestroy()
        {
            _inventoryGroupSelector.OnChangeSelected -= OnChangeSelected;
            _defaultSet.OnSelectThis -= OnSelectDefaultSet;
        }

        void OnSelectDefaultSet(GroupSelector.ISelectable _)
        {
            Service<DatabusInventory>.Get().EquippedItem = null;
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
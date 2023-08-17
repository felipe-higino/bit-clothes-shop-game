using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] SO_Store _storeSettings;
        [SerializeField] GroupSelector _storeItemsContainer;
        [SerializeField] PurchaseWidget _purchaseWidgetPrefab;

        Dictionary<GroupSelector.ISelectable, Store> _items;

        void Awake()
        {
            _items = new Dictionary<GroupSelector.ISelectable, Store>();
            foreach (Store storeItem in _storeSettings.storeItems)
            {
                // weapons not supported yet
                if (storeItem.itemType != ItemType.SET)
                    continue;

                PurchaseWidget widgetInstance = Instantiate(_purchaseWidgetPrefab, _storeItemsContainer.transform);
                widgetInstance.SetItemName(storeItem.itemName);
                // widgetInstance.SetItemIcon();

                _items.Add(widgetInstance, storeItem);
            }

            _storeItemsContainer.OnChangeSelected += OnChangeSelectedItem;
            _storeItemsContainer.Init();
        }

        void OnDestroy()
        {
            _storeItemsContainer.OnChangeSelected -= OnChangeSelectedItem;
        }

        void OnChangeSelectedItem(GroupSelector.ISelectable selected)
        {
            Store storeItem = _items[selected];
            Debug.Log($"Selected: {storeItem.itemName}");
        }
    }
}
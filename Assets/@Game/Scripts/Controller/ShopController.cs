using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] SO_Store _storeSettings;
        [SerializeField] GroupSelector _storeItemsContainer;
        [SerializeField] PurchaseWidget _purchaseWidgetPrefab;
        [SerializeField] Image _img_itemPreview;

        readonly Dictionary<GroupSelector.ISelectable, Store> _selectableStores = new();
        readonly Dictionary<Store, Sprite> _storeSprites = new();

        const string resources = "Store/Items/{0}";

        void Awake()
        {
            foreach (Store storeItem in _storeSettings.storeItems)
            {
                // weapons not supported yet
                if (storeItem.itemType != ItemType.SET)
                    continue;

                PurchaseWidget widgetInstance = Instantiate(_purchaseWidgetPrefab, _storeItemsContainer.transform);
                widgetInstance.SetItemName(storeItem.itemName);

                Sprite sprite = Resources.Load<Sprite>(string.Format(resources, storeItem.itemName));
                widgetInstance.SetItemIcon(sprite);

                _selectableStores.Add(widgetInstance, storeItem);
                _storeSprites.Add(storeItem, sprite);
            }

            _storeItemsContainer.OnChangeSelected += OnChangeSelectedItem;
            _storeItemsContainer.Init();
            _selectableStores.ElementAt(0).Key.SelectThis();
        }

        void OnDestroy()
        {
            _storeItemsContainer.OnChangeSelected -= OnChangeSelectedItem;
        }

        void OnChangeSelectedItem(GroupSelector.ISelectable selected)
        {
            Store storeItem = _selectableStores[selected];
            Sprite sprite = _storeSprites[storeItem];
            _img_itemPreview.sprite = sprite;
        }
    }
}
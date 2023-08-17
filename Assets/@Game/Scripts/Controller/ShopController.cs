﻿using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
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
        [SerializeField] TMP_Text _txt_price;
        [SerializeField] Button _btn_purchase;
        [SerializeField] TMP_Text _txt_cash;

        readonly Dictionary<GroupSelector.ISelectable, StoreItem> _selectableStores = new();
        readonly Dictionary<StoreItem, Sprite> _storeSprites = new();

        const string resources = "Store/Items/{0}";

        void Awake()
        {
            Service<DatabusInventory>.Get().cashReactive
                .ObserveOnMainThread()
                .Subscribe(value => _txt_cash.text = value.ToString())
                .AddTo(this);

            foreach (StoreItem storeItem in _storeSettings.storeItems)
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

            _btn_purchase.onClick.AddListener(OnClickPurchase);
        }

        void OnDestroy()
        {
            _storeItemsContainer.OnChangeSelected -= OnChangeSelectedItem;
            _btn_purchase.onClick.RemoveListener(OnClickPurchase);
        }

        void OnChangeSelectedItem(GroupSelector.ISelectable selected)
        {
            StoreItem storeItem = _selectableStores[selected];
            Sprite sprite = _storeSprites[storeItem];

            Service<DatabusStore>.Get().SelectedItem = storeItem;

            _img_itemPreview.sprite = sprite;
            _txt_price.text = storeItem.price.ToString();
        }

        void OnClickPurchase()
        {
            Service<PurchaseController>.Get().Purchase();
        }
    }
}
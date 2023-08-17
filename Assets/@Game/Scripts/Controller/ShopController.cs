using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Controller
{
    public class ShopController : MonoBehaviour
    {
        [Header("--- model ---")]
        [SerializeField] SO_Store _storeSettings;

        [Header("--- view ---")]
        [SerializeField] GroupSelector _storeItemsContainer;
        [SerializeField] PurchaseWidget _purchaseWidgetPrefab;
        [Space]
        [SerializeField] Image _img_itemPreview;
        [SerializeField] TMP_Text _txt_price;
        [SerializeField] Button _btn_purchase;
        [Space]
        [SerializeField] AudioClip _sfx_successPurchase;
        [SerializeField] AudioClip _sfx_failPurchase;

        readonly Dictionary<GroupSelector.ISelectable, StoreItem> _selectableStores = new();
        readonly Dictionary<StoreItem, Sprite> _storeSprites = new();

        void Awake()
        {
            foreach (StoreItem storeItem in _storeSettings.storeItems)
            {
                // weapons not supported yet
                if (storeItem.itemType != ItemType.SET)
                    continue;

                PurchaseWidget widgetInstance = Instantiate(_purchaseWidgetPrefab, _storeItemsContainer.transform);
                widgetInstance.SetItemName(storeItem.itemName);

                Sprite sprite = storeItem.itemName.SpriteFromItemName();
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
            Service<PurchaseController>.Get().Purchase(
                success: OnPurchaseSuccess,
                fail: OnPurchaseFail);
        }

        void OnPurchaseSuccess()
        {
            AudioController.Instance.PlaySFX(_sfx_successPurchase);
        }

        void OnPurchaseFail()
        {
            AudioController.Instance.PlaySFX(_sfx_failPurchase);
        }
    }
}
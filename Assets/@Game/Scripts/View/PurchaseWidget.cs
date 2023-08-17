using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game.Scripts.View
{
    public class PurchaseWidget : MonoBehaviour, GroupSelector.ISelectable, IPointerClickHandler
    {
        public event Action<GroupSelector.ISelectable> OnClick;

        [SerializeField] Image _arrow;
        [SerializeField] Image _img_icon;
        [SerializeField] TMP_Text _txt_itemName;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        void GroupSelector.ISelectable.NotifyIsSelected(bool isSelected)
        {
            _arrow.enabled = isSelected;
        }

        public void SetItemName(string itemName)
        {
            _txt_itemName.text = itemName;
        }

        public void SetItemIcon(Sprite icon)
        {
            _img_icon.sprite = icon;
        }
    }
}
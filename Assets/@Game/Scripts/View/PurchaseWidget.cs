using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game.Scripts.View
{
    public class PurchaseWidget : MonoBehaviour, GroupSelector.ISelectable, IPointerClickHandler
    {
        public event Action<GroupSelector.ISelectable> OnClick;

        [SerializeField] Image _arrow;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        void GroupSelector.ISelectable.NotifyIsSelected(bool isSelected)
        {
            _arrow.enabled = isSelected;
        }
    }
}
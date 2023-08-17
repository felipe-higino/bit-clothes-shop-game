using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game.Scripts.View
{
    public class InventoryItemWidget : MonoBehaviour, GroupSelector.ISelectable, IPointerClickHandler
    {
        [SerializeField] ButtonMultiImage _buttonMultiImage;
        [SerializeField] CursorHover _cursorHover;
        [SerializeField] Image _img_background;
        [SerializeField] Image _img_icon;
        [SerializeField] Image _img_locker;

        [SerializeField] State _currentState = State.UNKNOWN;

        public Action<GroupSelector.ISelectable> OnSelectThis { get; set; }

        public void SetState(State state)
        {
            switch (state)
            {
                case State.UNKNOWN:
                    break;
                case State.EMPTY:
                    StateEmpty();
                    break;
                case State.AVAILABLE:
                    StateAvailable();
                    break;
                case State.UNAVAILABLE:
                    StateUnavailable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public void SetIcon(Sprite icon)
        {
            _img_icon.sprite = icon;
        }

        void GroupSelector.ISelectable.OnSelectedChange(bool isSelected)
        {
            if (_currentState == State.EMPTY)
                return;

            if (isSelected)
                StateUnavailable();
            else
                StateAvailable();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnSelectThis?.Invoke(this);
        }

        void StateEmpty()
        {
            _img_locker.enabled = false;
            _buttonMultiImage.enabled = false;
            _cursorHover.enabled = false;
            _img_icon.enabled = false;
            _img_background.enabled = false;
        }

        void StateAvailable()
        {
            _img_locker.enabled = false;
            _buttonMultiImage.enabled = true;
            _cursorHover.enabled = true;
            _img_icon.enabled = true;
            _img_background.enabled = true;
        }

        void StateUnavailable()
        {
            _img_locker.enabled = true;
            _buttonMultiImage.enabled = false;
            _cursorHover.enabled = false;
            _img_icon.enabled = true;
            _img_background.enabled = true;
        }

        void OnValidate()
        {
            SetState(_currentState);
        }

        [Serializable]
        public enum State
        {
            UNKNOWN,
            EMPTY,
            AVAILABLE,
            UNAVAILABLE
        }
    }
}
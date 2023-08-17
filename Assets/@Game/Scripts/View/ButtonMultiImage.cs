using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game.Scripts.View
{
    public class ButtonMultiImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("--- Styles ---")]
        [SerializeField] Color _normal;
        [SerializeField] Color _hover;
        [SerializeField] Color _click;

        [Space]
        [Header("--- References ---")]
        [SerializeField] Image[] _targets;

        bool _isClick;
        bool _isHover;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _isClick = true;
            UpdateVisual();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _isHover = true;
            UpdateVisual();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _isHover = false;
            UpdateVisual();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _isClick = false;
            UpdateVisual();
        }

        void UpdateVisual()
        {
            if (_isClick)
            {
                SetColors(_click);
                return;
            }

            if (_isHover)
            {
                SetColors(_hover);
            }
            else
            {
                SetColors(_normal);
            }
        }

        void SetColors(Color newColor)
        {
            foreach (Image target in _targets)
            {
                target.color = newColor;
            }
        }
    }
}
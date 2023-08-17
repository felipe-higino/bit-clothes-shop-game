using UnityEngine;
using UnityEngine.EventSystems;
namespace Game.Scripts.View
{
    public class CursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        void OnDisable()
        {
            CursorManager.Instance.NotifyFinishHover(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorManager.Instance.NotifyHover(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorManager.Instance.NotifyFinishHover(this);
        }
    }
}
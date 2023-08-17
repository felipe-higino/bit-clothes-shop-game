using UnityEngine;
using UnityEngine.EventSystems;
namespace Game.Scripts.View
{
    public class CursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enabled)
                return;
            CursorManager.Instance.NotifyHover(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!enabled)
                return;
            CursorManager.Instance.NotifyFinishHover(this);
        }
    }
}
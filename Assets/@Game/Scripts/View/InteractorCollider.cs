using UnityEngine;
namespace Game.Scripts.View
{
    public class InteractorCollider : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            IInteractableCollider interactableCollider = other.GetComponent<IInteractableCollider>();
            if (null != interactableCollider)
            {
                interactableCollider.SetIsInteractable(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            IInteractableCollider interactableCollider = other.GetComponent<IInteractableCollider>();
            if (null != interactableCollider)
            {
                interactableCollider.SetIsInteractable(false);
            }
        }
    }

    interface IInteractableCollider
    {
        public void SetIsInteractable(bool isInteractable);
    }
}
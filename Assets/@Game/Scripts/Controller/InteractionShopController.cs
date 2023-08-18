using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class InteractionShopController : MonoBehaviour, IInteractableCollider
    {
        [SerializeField] Canvas _interactionFeedback;

        void Awake()
        {
            _interactionFeedback.enabled = false;
        }

        public void SetIsInteractable(bool isInteractable)
        {
            _interactionFeedback.enabled = isInteractable;
        }
    }
}
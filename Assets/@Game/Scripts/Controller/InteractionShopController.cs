using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class InteractionShopController : MonoBehaviour, IInteractableCollider
    {
        [SerializeField] Canvas _interactionFeedback;
        [SerializeField] Canvas _shopUI;

        bool _isInteractable = false;

        void Awake()
        {
            // Service<InputController>.Get().actions.Gameplay.
            _interactionFeedback.enabled = false;
        }

        public void SetIsInteractable(bool isInteractable)
        {
            _isInteractable = isInteractable;
            _interactionFeedback.enabled = isInteractable;
        }
    }
}
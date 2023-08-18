using Game.Scripts.Model;
using Game.Scripts.View;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Scripts.Controller
{
    public class InteractionShopController : MonoBehaviour, IInteractableCollider
    {
        [SerializeField] Canvas _interactionFeedback;

        bool _isInteractable = false;

        void Awake()
        {
            Service<InputController>.Get().actions.Gameplay.Interact.performed += OnInteract;
            _interactionFeedback.enabled = false;
        }

        void OnDestroy()
        {
            Service<InputController>.Get().actions.Gameplay.Interact.performed -= OnInteract;
        }

        void OnInteract(InputAction.CallbackContext ctx)
        {
            if (!_isInteractable)
                return;
            Service<DatabusCoreloop>.Get().gameState.Value = DatabusCoreloop.GameState.PURCHASING;
        }

        public void SetIsInteractable(bool isInteractable)
        {
            _isInteractable = isInteractable;
            _interactionFeedback.enabled = isInteractable;
        }
    }
}
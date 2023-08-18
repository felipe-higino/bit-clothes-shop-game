using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class InteractionShopController : MonoBehaviour, IInteractableCollider
    {
        [SerializeField] GameObject _interactionFeedback;

        public void SetIsInteractable(bool isInteractable)
        {
            _interactionFeedback.SetActive(isInteractable);
        }
    }
}
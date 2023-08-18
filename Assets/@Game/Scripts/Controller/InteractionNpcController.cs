using Game.Scripts.View;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Scripts.Controller
{
    public class InteractionNpcController : MonoBehaviour, IInteractableCollider
    {
        [SerializeField] NpcInteractionWidget _npcInteractionWidget;

        bool _isInteractable;
        IDisposable _timer;

        void Awake()
        {
            Service<InputController>.Get().actions.Gameplay.Interact.performed += OnInteract;
            _npcInteractionWidget.StateInvisible();
        }

        void OnDestroy()
        {
            _timer?.Dispose();
            _timer = null;
        }

        void OnInteract(InputAction.CallbackContext obj)
        {
            if (!_isInteractable)
                return;

            _npcInteractionWidget.StateResponse();
            _timer?.Dispose();
            _timer = null;
            _timer = Observable
                .Timer(TimeSpan.FromSeconds(3))
                .ObserveOnMainThread()
                .Subscribe(x => { _npcInteractionWidget.StateInteraction(); });
        }

        void IInteractableCollider.SetIsInteractable(bool isInteractable)
        {
            _isInteractable = isInteractable;

            if (isInteractable)
            {
                _npcInteractionWidget.StateInteraction();
            }
            else
            {
                _npcInteractionWidget.StateInvisible();
                _timer?.Dispose();
                _timer = null;
            }
        }
    }
}
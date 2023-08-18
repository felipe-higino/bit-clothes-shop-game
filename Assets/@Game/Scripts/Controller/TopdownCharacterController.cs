using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Scripts.Controller
{
    public class TopdownCharacterController : MonoBehaviour, BitClothesInputActions.IGameplayActions
    {
        [SerializeField] float _speed;
        [SerializeField] Rigidbody2D _rigidbody2D;

        Vector2 _direction;

        //TODO: change to Kinematic Character Controller (KCC)
        void Awake()
        {
            BitClothesInputActions actions = new();
            actions.Gameplay.SetCallbacks(this);
            actions.Enable();
        }

        void FixedUpdate()
        {
            _rigidbody2D.AddForce(_speed * Time.fixedDeltaTime * _direction);
        }

        void BitClothesInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }
    }
}
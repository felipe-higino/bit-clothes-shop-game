using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Scripts.Controller
{
    public class TopdownCharacterController : MonoBehaviour, BitClothesInputActions.IGameplayActions
    {
        public readonly ReactiveProperty<Vector2> walkDirection = new();
        public readonly ReactiveProperty<float> walkSpeed = new();

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

            if (_rigidbody2D.velocity.magnitude <= 0.01f)
            {
                _rigidbody2D.velocity = Vector2.zero;
            }

            walkDirection.Value = _rigidbody2D.velocity.normalized;
            walkSpeed.Value = _rigidbody2D.velocity.magnitude;
        }

        void BitClothesInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }
    }
}
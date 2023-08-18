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

            Vector2 velocity = _rigidbody2D.velocity;
            if (Mathf.Abs(_rigidbody2D.velocity.x) <= 0.01f)
            {
                velocity.x = 0;
            }
            if (Mathf.Abs(_rigidbody2D.velocity.y) <= 0.01f)
            {
                velocity.y = 0;
            }
            _rigidbody2D.velocity = velocity;

            walkDirection.Value = _rigidbody2D.velocity.normalized;
            walkSpeed.Value = _rigidbody2D.velocity.magnitude;
        }

        void BitClothesInputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }
    }
}
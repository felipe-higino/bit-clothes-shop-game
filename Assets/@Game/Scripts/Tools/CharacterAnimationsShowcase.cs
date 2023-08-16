using Game.Scripts.Model;
using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Tools
{
    public class CharacterAnimationsShowcase : MonoBehaviour
    {
        CharacterAnimation[] _animations;

        void Start()
        {
            _animations = FindObjectsOfType<CharacterAnimation>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                SetDirection(WalkDirection.LEFT);
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                SetDirection(WalkDirection.FRONT);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                SetDirection(WalkDirection.RIGHT);
                return;
            }
            if (Input.GetKey(KeyCode.W))
            {
                SetDirection(WalkDirection.BACK);
                return;
            }

            SetIsWalking(false);
        }

        void SetIsWalking(bool isWalking)
        {
            foreach (CharacterAnimation characterAnimation in _animations)
            {
                characterAnimation.isWalking.Value = isWalking;
            }
        }

        void SetDirection(WalkDirection direction)
        {
            foreach (CharacterAnimation characterAnimation in _animations)
            {
                characterAnimation.isWalking.Value = true;
                characterAnimation.direction.Value = direction;
            }
        }
    }
}
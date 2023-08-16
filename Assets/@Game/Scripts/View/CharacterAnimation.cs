using Game.Scripts.Model;
using System;
using UniRx;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Scripts.View
{
    public class CharacterAnimation : MonoBehaviour
    {
        public readonly ReactiveProperty<WalkDirection> direction = new();
        public readonly ReactiveProperty<bool> isWalking = new(false);
        
        [SerializeField] Animator _animator;

        const string animIdleFront = "idle-front";
        const string animIdleBack = "idle-back";
        const string animIdleLeft = "idle-left";
        const string animIdleRight = "idle-right";
        const string animWalkFront = "walk-front";
        const string animWalkBack = "walk-back";
        const string animWalkLeft = "walk-left";
        const string animWalkRight = "walk-right";

        void Awake()
        {
            Observable
                .Merge(direction.AsUnitObservable())
                .Merge(isWalking.AsUnitObservable())
                .ObserveOnMainThread()
                .Subscribe(UpdateStateMachine)
                .AddTo(this);
        }

        void UpdateStateMachine(Unit _)
        {
            WalkDirection currentDirection = direction.Value;
            bool isCurrentlyWalking = isWalking.Value;
            
            switch (currentDirection)
            {
                case WalkDirection.FRONT:
                    Play(isCurrentlyWalking ? animWalkFront : animIdleFront);
                    break;
                case WalkDirection.BACK:
                    Play(isCurrentlyWalking ? animWalkBack : animIdleBack);
                    break;
                case WalkDirection.LEFT:
                    Play(isCurrentlyWalking ? animWalkLeft : animIdleLeft);
                    break;
                case WalkDirection.RIGHT:
                    Play(isCurrentlyWalking ? animWalkRight : animIdleRight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Play(string animationName)
        {
            _animator.Play(animationName);
        }
        
        #region ------------------------- tool

#if UNITY_EDITOR
        [CustomEditor(typeof(CharacterAnimation))]
        public class CharacterAnimationEditor : Editor
        {
            CharacterAnimation script;

            void OnEnable()
            {
                script = (CharacterAnimation)target;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (!Application.isPlaying)
                    return;

                script.direction.Value = (WalkDirection)EditorGUILayout.EnumPopup("Direction", script.direction.Value);
                script.isWalking.Value = EditorGUILayout.Toggle("Is Walking?", script.isWalking.Value);
            }
        }
#endif

        #endregion ------------------------- tool
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Scripts.View
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] ButtonMultiImage _buttonMultiImage;
        [SerializeField] CursorHover _cursorHover;
        [SerializeField] Image _img_background;
        [SerializeField] Image _img_icon;
        [SerializeField] Image _img_locker;

        public void SetState(State state)
        {
            switch (state)
            {
                case State.EMPTY:
                    StateEmpty();
                    break;
                case State.AVAILABLE:
                    StateAvailable();
                    break;
                case State.UNAVAILABLE:
                    StateUnavailable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public void SetIcon(Sprite icon)
        {
            _img_icon.sprite = icon;
        }

        void StateEmpty()
        {
            _img_locker.enabled = false;
            _buttonMultiImage.enabled = false;
            _cursorHover.enabled = false;
            _img_icon.enabled = false;
            _img_background.enabled = false;
        }

        void StateAvailable()
        {
            _img_locker.enabled = false;
            _buttonMultiImage.enabled = true;
            _cursorHover.enabled = true;
            _img_icon.enabled = true;
            _img_background.enabled = true;
        }

        void StateUnavailable()
        {
            _img_locker.enabled = true;
            _buttonMultiImage.enabled = false;
            _cursorHover.enabled = false;
            _img_icon.enabled = true;
            _img_background.enabled = true;
        }

        public enum State
        {
            UNKNOWN,
            EMPTY,
            AVAILABLE,
            UNAVAILABLE
        }

        #region ------------------------- tool
#if UNITY_EDITOR
        State EDITOR_state = State.UNKNOWN;

        [CustomEditor(typeof(InventoryItemWidget))]
        public class InventoryItemWidgetEditor : Editor
        {
            InventoryItemWidget script;

            void OnEnable()
            {
                script = (InventoryItemWidget)target;
            }

            public override void OnInspectorGUI()
            {
                if (Application.isPlaying)
                    DrawTool();

                base.OnInspectorGUI();
            }

            void DrawTool()
            {
                EditorGUILayout.LabelField("Tool:");

                State state = (State)EditorGUILayout.EnumPopup("State", script.EDITOR_state);
                if (script.EDITOR_state != state)
                {
                    script.EDITOR_state = state;
                    script.SetState(state);
                }
            }
        }
#endif
        #endregion ------------------------- tool
    }
}
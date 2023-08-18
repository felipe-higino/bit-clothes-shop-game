using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.View
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance { get; private set; }

        static readonly Vector2 _point = new(0f, 1f);

        [SerializeField] Texture2D _cursorIdle;
        [SerializeField] Texture2D _cursorIdleClick;
        [SerializeField] Texture2D _cursorHover;
        [SerializeField] Texture2D _cursorHoverClick;

        readonly HashSet<CursorHover> _hovereds = new();

        bool _isHover;
        bool _isCursorDown;
        CursorState _currentState;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            _isCursorDown = Input.GetMouseButton(0);
            UpdateCursorState();
        }

        void UpdateCursorState()
        {
            if (_isHover)
            {
                if (_isCursorDown)
                {
                    SetCursorState(CursorState.HOVER_CLICK);
                }
                else
                {
                    SetCursorState(CursorState.HOVER);
                }
            }
            else
            {
                if (_isCursorDown)
                {
                    SetCursorState(CursorState.IDLE_CLICK);
                }
                else
                {
                    SetCursorState(CursorState.IDLE);
                }
            }
        }

        void SetCursorState(CursorState state)
        {
            if (_currentState == state)
                return;

            switch (state)
            {
                case CursorState.IDLE:
                    Cursor.SetCursor(_cursorIdle, _point, CursorMode.Auto);
                    break;
                case CursorState.IDLE_CLICK:
                    Cursor.SetCursor(_cursorIdleClick, _point, CursorMode.Auto);
                    break;
                case CursorState.HOVER:
                    Cursor.SetCursor(_cursorHover, _point, CursorMode.Auto);
                    break;
                case CursorState.HOVER_CLICK:
                    Cursor.SetCursor(_cursorHoverClick, _point, CursorMode.Auto);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            _currentState = state;
        }

        void UpdateHoverState()
        {
            _isHover = _hovereds.Count > 0;
        }

        public void NotifyHover(CursorHover hover)
        {
            if (null == hover)
                return;

            _hovereds.Add(hover);
            UpdateHoverState();
        }

        public void NotifyFinishHover(CursorHover hover)
        {
            if (null == hover)
                return;

            _hovereds.Remove(hover);
            UpdateHoverState();
        }

        enum CursorState
        {
            IDLE,
            IDLE_CLICK,
            HOVER,
            HOVER_CLICK
        }
    }
}
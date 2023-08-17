using System;
using UnityEngine;
namespace Game.Scripts.View
{
    public class GroupSelector : MonoBehaviour
    {
        public event Action<ISelectable> OnChangeSelected;

        [SerializeField] bool _isInitOnStart;

        ISelectable[] _selectables;

        public ISelectable CurrentSelected { get; private set; }

        void Start()
        {
            if (_isInitOnStart)
                Init();
        }

        public void Init()
        {
            _selectables = GetComponentsInChildren<ISelectable>();
            foreach (ISelectable selectable in _selectables)
            {
                selectable.OnClick += OnClickSelectable;
                selectable.NotifyIsSelected(false);
            }
        }

        void OnClickSelectable(ISelectable clicked)
        {
            foreach (ISelectable selectable in _selectables)
            {
                if (selectable == clicked)
                {
                    selectable.NotifyIsSelected(true);
                    CurrentSelected = clicked;
                    OnChangeSelected?.Invoke(clicked);
                }
                else
                {
                    selectable.NotifyIsSelected(false);
                }
            }
        }

        public interface ISelectable
        {
            public event Action<ISelectable> OnClick;
            public void NotifyIsSelected(bool isSelected);
        }
    }

}
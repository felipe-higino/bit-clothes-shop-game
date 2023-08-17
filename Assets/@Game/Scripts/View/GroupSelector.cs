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
                selectable.OnSelectThis += SelectThisSelectable;
                selectable.OnSelectedChange(false);
            }
        }

        void SelectThisSelectable(ISelectable clicked)
        {
            foreach (ISelectable selectable in _selectables)
            {
                if (selectable == clicked)
                {
                    selectable.OnSelectedChange(true);
                    CurrentSelected = clicked;
                    OnChangeSelected?.Invoke(clicked);
                }
                else
                {
                    selectable.OnSelectedChange(false);
                }
            }
        }

        public interface ISelectable
        {
            public Action<ISelectable> OnSelectThis { get; set; }
            public void OnSelectedChange(bool isSelected);
        }

    }

    public static class ISelectableExtensions
    {
        public static void SelectThis(this GroupSelector.ISelectable selectable)
        {
            selectable.OnSelectThis?.Invoke(selectable);
        }
    }

}
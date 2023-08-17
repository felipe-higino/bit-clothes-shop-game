using System;
using UnityEngine;
namespace Game.Scripts.View
{
    public class GroupSelector : MonoBehaviour
    {
        ISelectable[] _selectables;
        public interface ISelectable
        {
            public event Action<ISelectable> OnClick;
            public void NotifyIsSelected(bool isSelected);
        }

        void Start()
        {
            _selectables = GetComponentsInChildren<ISelectable>();
            for (int i = 0; i < _selectables.Length; i++)
            {
                ISelectable selectable = _selectables[i];
                selectable.OnClick += OnClick;
                selectable.NotifyIsSelected(false);
            }
        }

        void OnClick(ISelectable clicked)
        {
            foreach (ISelectable selectable in _selectables)
            {
                if (selectable == clicked)
                {
                    selectable.NotifyIsSelected(true);
                }
                else
                {
                    selectable.NotifyIsSelected(false);
                }
            }
        }
    }

}
using Game.Scripts.Model;
using Game.Scripts.View;
using System.Linq;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class CharacterVisualController : MonoBehaviour
    {
        [SerializeField] SO_Animations _animationsDB;
        CharacterAnimation _visualInstance;

        string _currentAnimationName;

        void Awake()
        {
            Service<DatabusInventory>.Get().equippedItemReactive
                .ObserveOnMainThread()
                .Subscribe(equippedItem =>
                {
                    if (null == equippedItem)
                    {
                        InstantiateAnimation(DomainConversion.defaultName);
                    }
                    else
                    {
                        Animations item = _animationsDB.animations.First(x => x.setItemName == equippedItem.itemName);
                        if (null == item)
                            return;

                        string animationName = item.animationControllerName;
                        InstantiateAnimation(animationName);
                    }
                })
                .AddTo(this);
        }

        void InstantiateAnimation(string animationName)
        {
            if (_currentAnimationName != animationName)
            {
                if (null != _visualInstance)
                {
                    //TODO: use a pool/reuse approach for performance improovement
                    Destroy(_visualInstance.gameObject);
                }

                CharacterAnimation characterAnimationPrefab = animationName.AnimationFromName();
                _visualInstance = Instantiate(characterAnimationPrefab, this.transform);
            }
        }

    }
}
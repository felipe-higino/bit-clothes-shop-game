using Game.Scripts.Model;
using Game.Scripts.View;
using System.Linq;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class CharacterVisualController : MonoBehaviour
    {
        [SerializeField] TopdownCharacterController _characterController;
        [SerializeField] SO_Animations _animationsDB;
        CharacterAnimation _visualInstance;

        string _currentAnimationName;

        void Awake()
        {
            DisposableExtensions.AddTo(ObservableExtensions.Subscribe(Observable.ObserveOnMainThread(Service<DatabusInventory>.Get().equippedItemReactive), OnEquipedItemChange), this);
            Observable
                .Merge(_characterController.walkDirection.AsUnitObservable())
                .Merge(_characterController.walkSpeed.AsUnitObservable())
                .ObserveOnMainThread()
                .Subscribe(OnMovementUpdate)
                .AddTo(this);
        }

        void OnMovementUpdate(Unit _)
        {
            if (null != _visualInstance)
            {
                Vector2 direction = _characterController.walkDirection.Value;
                _visualInstance.direction.Value = CalculateDirection(direction);

                bool isWalking = _characterController.walkSpeed.Value > 0;
                _visualInstance.isWalking.Value = isWalking;
            }
        }

        void OnEquipedItemChange(InventoryItem equippedItem)
        {
            if (null == equippedItem)
            {
                InstantiateAnimation(DomainConversion.defaultName);
            }
            else
            {
                Animations item = _animationsDB.animations.First(x => x.setItemName == equippedItem.itemName);
                if (null == item) return;

                string animationName = item.animationControllerName;
                InstantiateAnimation(animationName);
            }
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

        WalkDirection CalculateDirection(Vector2 movementDirection)
        {
            if (movementDirection.x != 0)
            {
                if (movementDirection.x > 0)
                {
                    return WalkDirection.RIGHT;
                }
                else
                {
                    return WalkDirection.LEFT;
                }
            }
            else
            {
                if (movementDirection.y > 0)
                {
                    return WalkDirection.BACK;
                }
                else
                {
                    return WalkDirection.FRONT;
                }
            }
        }

    }
}
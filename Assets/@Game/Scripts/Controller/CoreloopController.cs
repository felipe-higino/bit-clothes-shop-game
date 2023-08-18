using Game.Scripts.Model;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class CoreloopController : MonoBehaviour
    {
        [SerializeField] GameObject _shopUIObject;

        void Awake()
        {
            Service<DatabusInventory>.Get().Cash = 2000;

            DatabusCoreloop coreloopDatabus = Service<DatabusCoreloop>.Get();

            coreloopDatabus.gameState.Value = DatabusCoreloop.GameState.EXPLORING;

            coreloopDatabus.gameState
                .ObserveOnMainThread()
                .Subscribe(OnGamestateUpdate)
                .AddTo(this);
        }

        void OnGamestateUpdate(DatabusCoreloop.GameState gameState)
        {
            InputController inputController = Service<InputController>.Get();

            bool isPurchasing = gameState == DatabusCoreloop.GameState.PURCHASING;
            _shopUIObject.SetActive(isPurchasing);

            bool isExploring = gameState == DatabusCoreloop.GameState.EXPLORING;
            if (isExploring)
            {
                inputController.actions.Gameplay.Enable();
            }
            else
            {
                inputController.actions.Gameplay.Disable();
            }
        }
    }
}
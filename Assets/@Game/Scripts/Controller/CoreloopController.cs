using Game.Scripts.Model;
using Game.Scripts.View;
using System.Collections;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class CoreloopController : MonoBehaviour
    {
        [SerializeField] GameObject _shopUIObject;
        [SerializeField] FadeView _initialFade;

        void Awake()
        {
            _initialFade.SetAlpha(1);

            Service<DatabusInventory>.Get().Cash = 2000;

            DatabusCoreloop coreloopDatabus = Service<DatabusCoreloop>.Get();

            coreloopDatabus.gameState
                .ObserveOnMainThread()
                .Subscribe(OnGamestateUpdate)
                .AddTo(this);
        }

        IEnumerator Start()
        {
            yield return null;
            yield return null;

            _initialFade.FadeIn();

            Service<DatabusCoreloop>.Get().gameState.Value = DatabusCoreloop.GameState.EXPLORING;
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
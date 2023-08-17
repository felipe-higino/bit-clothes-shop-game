using Game.Scripts.Model;
using TMPro;
using UniRx;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public class CashController : MonoBehaviour
    {
        [SerializeField] TMP_Text _txt_cash;

        void Awake()
        {
            Service<DatabusInventory>.Get().cashReactive
                .ObserveOnMainThread()
                .Subscribe(value => _txt_cash.text = value.ToString())
                .AddTo(this);
        }
    }
}
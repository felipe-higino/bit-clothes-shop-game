using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.View
{
    public class FadeView : MonoBehaviour
    {
        [SerializeField] float _duration;
        [SerializeField] Image _fade;

        public void SetAlpha(float alpha)
        {
            Color color = _fade.color;
            color.a = 1;
            _fade.color = color;
        }

        public void FadeIn()
        {
            SetAlpha(1);
            _fade
                .DOFade(0, _duration)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
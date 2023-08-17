using UnityEngine;
namespace Game.Scripts.Controller
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip _audioClip;

        public void Play()
        {
            AudioController.Instance.PlaySFX(_audioClip);
        }
    }
}
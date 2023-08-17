using UnityEngine;
namespace Game.Scripts.Controller
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] AudioSource _sfx;
        public static AudioController Instance { get; private set; }

        void Awake()
        {
            if (null != Instance)
            {
                Debug.LogError("Only one audio controller singleton allowed");
                Destroy(this);
            }

            Instance = this;
        }

        public void PlaySFX(AudioClip clip)
        {
            _sfx.PlayOneShot(clip);
        }
    }
}
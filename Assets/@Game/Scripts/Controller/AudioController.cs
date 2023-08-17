using UnityEngine;
namespace Game.Scripts.Controller
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance { get; private set; }

        [SerializeField] AudioSource _sfxSource;

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
            _sfxSource.PlayOneShot(clip);
        }
    }
}
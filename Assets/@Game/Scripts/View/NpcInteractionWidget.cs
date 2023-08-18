using UnityEngine;
namespace Game.Scripts.View
{
    public class NpcInteractionWidget : MonoBehaviour
    {
        [SerializeField] Canvas _interactionView;
        [SerializeField] Canvas _responseView;

        public void StateInvisible()
        {
            _interactionView.enabled = false;
            _responseView.enabled = false;
        }

        public void StateInteraction()
        {
            _interactionView.enabled = true;
            _responseView.enabled = false;
        }

        public void StateResponse()
        {
            _interactionView.enabled = false;
            _responseView.enabled = true;
        }
    }
}
namespace Game.Scripts.Controller
{
    public class InputController
    {
        public readonly BitClothesInputActions actions;

        public InputController()
        {
            actions = new BitClothesInputActions();
            actions.Enable();
        }
    }
}
using UniRx;
namespace Game.Scripts.Model
{
    public class DatabusInventory
    {
        public int cash
        {
            get => cashReactive.Value;
            set => cashReactive.Value = value;
        }

        public readonly ReactiveProperty<int> cashReactive = new(0);
    }
}
using UniRx;
namespace Game.Scripts.Model
{
    public class DatabusCoreloop
    {
        public readonly ReactiveProperty<GameState> gameState = new();
        public enum GameState
        {
            NONE,
            EXPLORING,
            PURCHASING
        }
    }
}
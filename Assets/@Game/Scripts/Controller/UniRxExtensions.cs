using UniRx;
namespace Game.Scripts.Controller
{
    public static class UniRxExtensions
    {
        public static void ClearCollection<T>(this ReactiveCollection<T> collection)
        {
            int count = collection.Count;
            for (int i = 0; i < count; i++)
            {
                collection.RemoveAt(0);
            }
        }
    }
}
namespace ReverseDungeonSparta.Manager
{
    public abstract class Manager<T> where T : Manager<T>, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance => _instance.Value;

        protected Manager()
        {
            Console.WriteLine($"{typeof(T).Name} 생성됨!");
        }
    }
}
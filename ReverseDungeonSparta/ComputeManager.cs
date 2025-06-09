namespace ReverseDungeonSparta
{
    public static class ComputeManager
    {
        public static bool TryChance(int probability)
        {

            Random random = new Random();
            return random.Next(100) < probability;
        }
    }
}

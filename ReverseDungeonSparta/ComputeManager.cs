using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

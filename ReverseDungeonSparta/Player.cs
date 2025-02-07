using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public enum JobType
    {
        Warrior
    }
    internal class Player
    {
        public JobType Job { get; set; } 
      public int Level { get; set; }
        public string Name { get; set; }  
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public int AdditionalAttack { get; set; }
        public int AdditionalDefence { get; set; }
        public Player ()
        {
            int lv = Level;
            string name = Name;
            Attack = 10;
            Defence = 5;
            Health = 100;
            Gold = 1500;
            int additionalAttack = AdditionalAttack;
            int additionalDefence = AdditionalDefence;

            switch (Job)
            {
                case JobType.Warrior:
                    break;
            }

        }
    }
}

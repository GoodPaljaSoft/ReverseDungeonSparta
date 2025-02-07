using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public enum JobType // 캐릭터 enum을 통해 직업 선택
    {
        Warrior
    }
    public class Player
    {
        public JobType Job { get; set; } 
        public int Level { get; set; }
        public string Name { get; set; }  
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHealth { get; set; }
        public int NowHealth { get; set; }
        public int Gold { get; set; }
        public int AdditionalAttack { get; set; }
        public int AdditionalDefence { get; set; }
        public Player () //Player 생성자 
        {
            int lv = Level;
            string name = Name; 
            Attack = 10; //공격력, 방어력, 체력, 골드는 초기값으로 초기화
            Defence = 5;
            MaxHealth = 100;
            NowHealth = MaxHealth;
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

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
    public class Player : Character
    {
        public JobType Job { get; set; } 
        public int Level { get; set; }

        public override string Name { get; set; } = string.Empty;
        public override int Attack { get; set; }
        public override int Defence { get; set; }
        public override int MaxHP { get; set; }
        public override int HP { get; set; }
        public override int MaxMP { get; set; }
        public override int MP { get; set; }
        public int Gold { get; set; }

        public int AdditionalAttack { get; set; }   // 장비 공격력
        public int AdditionalDefence { get; set; }  // 장비 방어력

        public Player () //Player 생성자 
        {
            Name = "플레이어";
            Level = 1;

            SkillList = Skill.AddPlayerSkill(this, 3);

            Luck = 5;
            Defence = 5;
            Attack = 100;
            Intelligence = 5;
            
            MaxHP = 100;
            HP = MaxHP;
            MaxMP = 100;
            MP = MaxMP;

            Speed = 8;

            Critical = 5;
            Evasion = 5;

            int additionalAttack = AdditionalAttack;
            int additionalDefence = AdditionalDefence;

            switch (Job)
            {
                case JobType.Warrior:
                    break;
            }
        }


        public void SelectSkill(List<Monster> monsters, int selectSkillNum)
        {
            selectSkillNum--;


        }


        public bool CheckPlayerCanSkill(int selectSkillNum)
        {
            bool result = false;
            selectSkillNum--;

            //플레이어 마나가 요구 마나보다 이상일 경우
            if (SkillList[selectSkillNum].ConsumptionMP <= this.MP)
            {
                result = true;
            }

            return result;
        }
    }
}

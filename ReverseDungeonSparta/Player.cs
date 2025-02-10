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
        List<EquipItem> equipItemList = new List<EquipItem>(); // 아이템목록 객체 만들기
        public JobType Job { get; set; } 
        public int Level { get; set; }
        public int Gold { get; set; }
        public int AdditionalAttack { get; set; }   // 장비 공격력
        public int AdditionalDefence { get; set; }  // 장비 방어력
        public int MaxEXP { get; set; }
        public int NowEXP { get; set; }


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
        #region 아이템 능력치 저장
        public void PlusItemStat() 
        {
            foreach (var equipItem in equipItemList)
            {
                var itemInfo = equipItem.ItemInfo;

                Attack += equipItem.ItemInfo.addAttack;
                Defence += equipItem.ItemInfo.addDefence;
                Luck += equipItem.ItemInfo.addLuck;
                Intelligence += equipItem.ItemInfo.addIntelligence;
                MaxHP += equipItem.ItemInfo.addMaxHp;
                MaxMP += equipItem.ItemInfo.addMaxMp;
            }
        }
        #endregion
        public void IsEquipItem(EquipItem item) //아이템 장착 로직 구현
        {
            if(!equipItemList.Contains(item))
            {
                equipItemList.Add(item); // item이 장착된 장착아이템리스트에 없다면
                item.isEquiped = true;
                PlusItemStat();

            }
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

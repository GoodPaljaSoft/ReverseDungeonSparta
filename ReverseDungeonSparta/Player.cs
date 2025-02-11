using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReverseDungeonSparta.EquipItem;

namespace ReverseDungeonSparta
{
    public enum JobType // 캐릭터 enum을 통해 직업 선택
    {
        Warrior
    }
    public class Player : Character
    {
        public List<EquipItem> equipItemList = new List<EquipItem>(); // 아이템목록 객체 만들기
        public JobType Job { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int AdditionalAttack { get; set; }   // 장비 공격력
        public int AdditionalDefence { get; set; }  // 장비 방어력
        public int MaxEXP { get; set; }
        public int NowEXP { get; set; }


        public Player() //Player 생성자 
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

            Speed = 3;

            Critical = 5;
            Evasion = 5;

            //int additionalAttack = AdditionalAttack; //필요없다면 지우기
            //int additionalDefence = AdditionalDefence;

            switch (Job)
            {
                case JobType.Warrior:
                    break;
            }
        }
        #region 아이템 능력치 저장
        public void ApplyItemStat()
        {
            Attack = 100;  // 일단 기본값으로 초기화
            Defence = 5;
            Luck = 5;
            Intelligence = 5;
            MaxHP = 100;
            HP = MaxHP;
            MP = 100;
            MP = MaxMP;

            foreach (var equipItem in equipItemList)
            {
                Attack += equipItem.AddAttack;
                Defence += equipItem.AddDefence;
                Luck += equipItem.AddAttack;
                Intelligence += equipItem.AddAttack;
                MaxHP += equipItem.AddMaxHp;
                MaxMP += equipItem.AddMaxMp;
            }
        }
        #endregion

        public void LoadEquipItems()
        {
            // EquipItem.allEquipItem 배열을 반복하여 equipItemList에 추가
            foreach (var EquipitemInfo in EquipItem.allEquipItem)
            {
                EquipItem equipItem = new EquipItem(EquipitemInfo); // EquipItem 객체 생성
                equipItemList.Add(equipItem); // 생성된 아이템을 리스트에 추가
            }
        }

        public void IsEquipItem(EquipItem item) //아이템 장착 로직 구현
        {
            if (!equipItemList.Contains(item))
            {
                equipItemList.Add(item); // item이 장착된 장비아이템리스트에 없다면
                item.IsEquiped = true;
                ApplyItemStat();
            }
            else
            {
                Console.WriteLine("장착된 아이템이 아닙니다.");
            }
        }
        public void UnEquippedItem(EquipItem item) //아이템 해제 로직 구현
        {
            if (equipItemList.Contains(item))
            {
                equipItemList.Remove(item); // 장비리스트에 장착된 아이템을 제거하고
                item.IsEquiped = false;
                ApplyItemStat(); //다시 스텟을 초기화
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
        //public static List<EquipItem> GetEquipItemList(EquipItemInfo equipItemInfo)
        // {
            //        // 새로운 장비아이템 리스트 만들고 
            //        List<EquipItem> rewardItemList = new List<EquipItem>();

            //        // 랜덤값 생성하여
            //        Random random = new Random();

            //        // 기본적인 반환 아이템은 초기값이 Normal 등급이 되도록
            //        // Grade itemgrade = Grade.Normal;

            //        // 생성 확률에 따라 랜덤아이템이 리스트 안에 포함되도록 생성
            //        double randomValue = random.NextDouble();

            //        // 30% 확률로 Uncommon 아이템이 나오도록
            //        if (randomValue <= 0.3f)
            //        {
            //            itemgrade = Grade.Uncommon;
            //        }

            //        // 50%를 통해서 30%의 아이템인 Uncommon이 나오고 그다음으로 20%인 Rare인 아이템이 나오도록
            //        else if (randomValue <= 0.5f)
            //        {
            //            itemgrade = Grade.Rare;
            //        }
            //        else 
            //        {
            //           EquipItem equipitem = new EquipItem();  
            //        }
            //        //enum값에 따른 Type 랜덤으로 정하기
            //        Type type = (Type)random.Next(0, 6);


            //        // 아이템 정보를 생성하는 

            //        //EquipItem.EquipItemInfo itemInfo = new EquipItem.EquipItemInfo
            //        //(
            //        //    itemName,
            //        //    _addAttack,
            //        //    _addDefence,
            //        //    _addIntelligence,
            //        //    _addMaxHp,
            //        //    _addMaxMp,
            //        //    item,


            //        //);
            //        EquipItem rewardItem = new EquipItem(equipItemInfo);

            //        string itemName = $"{type} Item";
            //        string description = $"{itemName} 설명";



            //        // 반환된 아이템을 리워드리스트에 추가
            //        rewardItemList.Add(rewardItem);

            //        return new List<EquipItem> { rewardItem };
            //    }
            //}
        
    }
}

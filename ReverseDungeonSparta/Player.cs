using System;
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

            SkillList = Skill.AddPlayerSkill(this, 7);

            Luck = 100;
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
            InitEquipItems();

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

        public void InitEquipItems()
        {
            // EquipItem.allEquipItem 배열을 반복하여 equipItemList에 추가
            foreach (var info in EquipItem.allEquipItem)
            {
                EquipItem equipItem = new EquipItem(info); // EquipItem 객체 생성
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
        public void TryEquipItemUpgrade(List<EquipItem> equipItemList)
        {
            Console.WriteLine("");
            Console.WriteLine("[아이템 조합]");
            Console.WriteLine("조합을 원하시는 아이템을 입력해주세요.");
            int number1 = Util.GetUserIntInput(1, equipItemList.Count) - 1;//매개변수 숫자로 넣지 말아주세요
            EquipItem main = equipItemList[number1];
            Console.WriteLine("조합을 원하시는 두번째 아이템을 입력해주세요.");
            int number2 = Util.GetUserIntInput(1, equipItemList.Count) - 1;//매개변수 숫자로 넣지 말아주세요
            EquipItem offering = equipItemList[number2];
            EquipItem? newitem = ItemUpgrade(main, offering, equipItemList);
            if (newitem != null)
            {
                equipItemList.Add(newitem);
            }
        }
        public static EquipItem ItemUpgrade(EquipItem main, EquipItem offering, List<EquipItem> equipItemList)
        {
            //조합하고자 선택한 두 아이템의 타입이 동일한가, 등급이 동일한가?
            if (main.Type == offering.Type && main.Grade == offering.Grade)
            {
                float upgradePercent = 0.0f; //업그레이드 퍼센트 변수 생성
                switch (main.Grade) //item1의 매개변수를 받아서 타입별 아이템 강화확률을 설정
                {
                    case EquipItemGrade.Normal:
                        upgradePercent = 0.5f;
                        break;
                    case EquipItemGrade.Uncommon:
                        upgradePercent = 0.3f;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("더 이상 강화할 등급이 없습니다.");
                        return null;
                }
                //업그레이드 확률을 랜덤으로 설정
                Random random = new Random();
                // 업그레이드퍼센트를 0 ~ 100퍼센트로 만들기 위한 NextDoulbe메서드 사용
                double randomValue = random.NextDouble();

                // 업그레이드 등급확률이 랜덤확률값보다 높을 떄 조합이 성공되도록
                if (randomValue <= upgradePercent)
                {
                    //아이템 등급의 enum값을 이용하여 nextgrade로 만들기
                    EquipItemGrade nextgrade = (EquipItemGrade)((int)main.Grade + 1);

                    EquipItem upgradeItem = new EquipItem();
                    Console.Clear();
                    Console.WriteLine("[조합 결과]");
                    Console.WriteLine($"조합 성공! 새로운 아이템 : {upgradeItem.Name}, {upgradeItem.Type}, {upgradeItem.Grade}");
                    Thread.Sleep(1000);
                    ReturnToInventory();

                    List<EquipItem> tempEquipList = new List<EquipItem>();
                    for (int i = 0; i < equipItemList.Count; i++)
                    {

                        //장착 가능한 아이템 리스트를 모두 검사를 돌린다.
                        //검사를 돌려서 받은 아이템 등급보다 한 등급 높은 아이템들을 모두 임시 리스트에 받아온다.
                        //받아온 리스트에서 랜덤으로 하나를 고른다.
                        if (equipItemList[i].Grade == offering.Grade + 1)
                        {
                            tempEquipList.Add(equipItemList[i]);
                        }
                    }
                    int rand = random.Next(0, tempEquipList.Count);
                    upgradeItem = tempEquipList[rand];

                    return upgradeItem;
                    equipItemList.Remove(main);
                    equipItemList.Remove(offering);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("[조합 결과]");
                    Console.WriteLine("조합 실패! 조합한 아이템이 소멸됩니다...");
                    Thread.Sleep(1000);
                    ReturnToInventory();
                    equipItemList.Remove(main);
                    equipItemList.Remove(offering);
                    return null;
                }
            }
            else // 아이템타입이나 등급이 다르다면 나올 수 있는 출력
            {
                Console.WriteLine("같은 타입과 같은 등급의 아이템만 조합할 수 있습니다.");
                Thread.Sleep(1000);
                return null;
            }
        }
        public static List<EquipItem> RandomRewardList()
        {
            // 새로운 보상아이템정보를 리스트화 하고
            List<EquipItemInfo> rewardItemListInfo = new List<EquipItemInfo>();

            Random random = new Random();

            // 리스트안에 장비아이템정보를 입력
            foreach (EquipItemInfo rewardItemInfo in allEquipItem)
            {
                rewardItemListInfo.Add(rewardItemInfo);
            }
            int rand = random.Next(0, rewardItemListInfo.Count);

            // 랜덤으로 equipItemInfo가 리스트에 들어가고,
            EquipItemInfo equipItemInfo = rewardItemListInfo[rand];

            // equipItemInfo가 있는 rewardItem 객체 생성
            EquipItem rewardItem = new EquipItem(equipItemInfo);

            return new List<EquipItem> { rewardItem };
        }

    }

}

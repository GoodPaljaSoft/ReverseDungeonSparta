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
        public List<EquipItem> isOwnedItemList = new List<EquipItem>();
        public List<EquipItem> isEquippedList = new List<EquipItem>();
        public List<UsableItem> UsableItemInventory = new List<UsableItem>(); // 소비 아이템 리스트
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

            // 기본으로 주어지는 소비 아이템 추가
            AddInitialItems();

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


        //테스트 코드
        public void InitEquipItems()
        {
            // EquipItem.allEquipItem 배열을 반복하여 equipItemList에 추가
            foreach (var info in EquipItem.allEquipItem)
            {
                EquipItem equipItem = new EquipItem(info); // EquipItem 객체 생성
                equipItemList.Add(equipItem); // 생성된 아이템을 리스트에 추가
            }
        }

        public void IsEquipItem(ref int itemIndex) //아이템 장착 로직 구현
        {
            if (itemIndex >= 0 && itemIndex < equipItemList.Count)
            {
                EquipItem item = equipItemList[itemIndex]; //테스트를 위한 equipItemList //실제isOwnedItemList
                bool isEquipped = item.IsEquiped; 
                if (isEquipped == false) //아이템 장착되지 않았다면
                {
                    bool isTypeEquipped = false; //같은 타입의 아이템을 장착했는지 확인하기 위함
                    foreach(var equippedItem in isEquippedList)
                    {
                        if(equippedItem.Type == item.Type)
                        {
                            isTypeEquipped = true;  //이미 장착되어있으므로 패스
                            break;
                        }
                    }
                    if(!isTypeEquipped) //같은 타입의 아이템이 없으므로
                    {
                        item.IsEquiped = true; 
                        isEquippedList.Add(item);
                        // ApplyItemStat();
                    }
                    else
                    {
                        Console.WriteLine($"이미 {item.Type} 타입의 아이템이 장착되어 있습니다.");
                    }
                }
                else
                {
                    foreach (var equippedItem in isEquippedList) //장착해제
                    {
                        if (equippedItem == item) // 장착된 아이템을 찾으면
                        {
                            isEquippedList.Remove(equippedItem); // 장착아이템리스트에서 제거
                            item.IsEquiped = false; // 장착 상태를 false로 변경

                            // ApplyItemStat();
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("소유한 아이템이 아닙니다.");
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

        /*
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
            EquipItem? newitem = ItemUpgrade(main, offering, equipItemList); //equipItemList는 테스트용
                                                                             //실제 isOwnedItemList
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
                    // 아이템 총 갯수 저장
                    int itemCount = allEquipItem.Length;             // 18 

                    // 총 갯수를 통해 랜덤으로 뽑하낼 숫자의 최대값을 정함
                    int randomMax = itemCount / 3;                   // 6 

                    int ramdomIndex = random.Next(1, randomMax + 1); // 1~6 랜덤으로 뽑힘

                    //랜덤으로 1~6을 받아온다.
                    // 1~6 * 3 == 3의 배수 (0)
                    // -2: -1//

                    EquipItem upgradeItem;

                    if (main.Grade == EquipItemGrade.Normal)
                    {
                        //1,4,7,10,13,16

                        upgradeItem = new EquipItem(allEquipItem[ramdomIndex - 2]);

                    }

                    else if (main.Grade == EquipItemGrade.Uncommon)
                    {
                        //2,5,8,11,14,17


                    }

                    //이 부분을 고쳐야 함
                    //List<EquipItem> tempEquipList = new List<EquipItem>();
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

                    Console.Clear();
                    Console.WriteLine("[조합 결과]");
                    Console.WriteLine($"조합 성공! 새로운 아이템 : {upgradeItem.Name}, {upgradeItem.Type}, {upgradeItem.Grade}");
                    Thread.Sleep(1000);
                    ReturnToInventory();

                    int rand = random.Next(0, tempEquipList.Count);
                    upgradeItem = tempEquipList[rand];
                    equipItemList.Remove(main);
                    equipItemList.Remove(offering);
                    return upgradeItem;// return 다음 애들은 호출이 안됩니다.
                    //equipItemList.Remove(main);
                    //equipItemList.Remove(offering);
                }
                else
                {
                    //실패
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
        */
        public static EquipItem RandomRewardList()
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

            return rewardItem;
        }
        // 소비 아이템 추가
        public void AddItemToInventory(UsableItem item, int count)
        {
            // 이미 해당 아이템이 존재하는 경우, 수량을 증가시킴
            var existingItem = UsableItemInventory.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Count += count;
            }
            else
            {
                // 새로운 아이템인 경우, 인벤토리에 추가
                item.Count = count;
                UsableItemInventory.Add(item);
            }
        }
        /*
               예시: 플레이어에게 체력 회복 포션 3개 추가
               UsableItem hpPotion = new UsableItem(UsableItem.allUsableItem[0]); // 체력 회복 포션 생성
               GameManager.Instance.Player.AddItemToInventory(hpPotion, 3); // 플레이어 인벤토리에 3개 추가
        */

        // 기본으로 주어지는 소비 아이템 추가
        private void AddInitialItems()
        {
            UsableItem LowHpPotion = new UsableItem(UsableItem.allUsableItem[0]); // 하급 체력 회복 포션
            UsableItem MidHpPotion = new UsableItem(UsableItem.allUsableItem[1]); // 중급 체력 회복 포션
            UsableItem HighHpPotion = new UsableItem(UsableItem.allUsableItem[2]); // 상급 체력 회복 포션
            UsableItem LowMpPotion = new UsableItem(UsableItem.allUsableItem[3]); // 하급 마나 회복 포션
            UsableItem MidMpPotion = new UsableItem(UsableItem.allUsableItem[4]); // 중급 마나 회복 포션
            UsableItem HighMpPotion = new UsableItem(UsableItem.allUsableItem[5]); // 상급 마나 회복 포션

            // 인벤토리에 추가 
            AddItemToInventory(LowHpPotion, 5);
            AddItemToInventory(MidHpPotion, 5);
            AddItemToInventory(HighHpPotion, 5);
            AddItemToInventory(LowMpPotion, 5);
            AddItemToInventory(MidMpPotion, 5);
            AddItemToInventory(HighMpPotion, 5);
        }


        //플레이어의 장비 아이템 리스트를 정렬해주는 메소드
        public void SortEquippedItemList()
        {
            equipItemList = equipItemList
                                    .OrderByDescending(x => x.IsEquiped)
                                    .ThenBy(x => (int)x.Type)
                                    .ToList();
        }


        //플레이어의 소비 아이템 리스트를 정렬해주는 메소드
        public void SortUseItemList()
        {
            UsableItemInventory = UsableItemInventory
                                    .OrderBy(x => x.Name)
                                    .ToList();
        }
    }

}

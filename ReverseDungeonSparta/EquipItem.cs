

namespace ReverseDungeonSparta
{
    public enum EquipItemGrade
    {
        Normal,
        Uncommon,
        Rare,
    }
    public enum EquipItemType
    {
        Armor,
        Weapon,
        Helmet,
        Shoes,
        Ring,
        Necklace
    }
    public class EquipItem : Item
    {
        public EquipItemType Type { get; set; }
        public EquipItemGrade Grade { get; set; }
        public int AddLuck { get; set; }
        public int AddDefence { get; set; }
        public int AddAttack { get; set; }
        public int AddIntelligence { get; set; }
        public int AddMaxHp { get; set; }
        public int AddMaxMp { get; set; }

        public bool IsEquiped { get; set; }

        public EquipItem(EquipItemInfo e)
        {
            Name = e.name;
            Information = e.info;
            Type = e.type;
            Grade = e.grade;
            AddLuck = e.addLuck;
            AddDefence = e.addDefence;
            AddAttack = e.addAttack;
            AddIntelligence = e.addIntelligence;
            AddMaxHp = e.addMaxHp;
            AddMaxMp = e.addMaxMp; 
            IsEquiped = false;
        }
        public EquipItem()
        {

        }
        // 장착한 아이템 구조체
        public struct EquipItemInfo
        {
            public string name;
            public string info;
            public EquipItemType type;
            public EquipItemGrade grade;
            public int addLuck;
            public int addDefence;
            public int addAttack;
            public int addIntelligence;
            public int addMaxHp;
            public int addMaxMp;

            public EquipItemInfo(string _name, int _addLuck, int _addDefence, int _addAttack, int _addIntelligence, int _addMaxHp, int _addMaxMp, EquipItemType _type, EquipItemGrade _grade, string _info)
            {
                name = _name;
                info = _info;
                type = _type;
                grade = _grade;                
                addLuck = _addLuck;
                addDefence = _addDefence;
                addAttack = _addAttack;
                addIntelligence = _addIntelligence;
                addMaxHp = _addMaxHp;
                addMaxMp = _addMaxMp;  
            }
        }
        public static void TestItemUpgrade()
        {
            //ItemUpgrade();
        }
        //public static void PrintItemList() //장착 가능한 아이템 출력 //추후에 유틸부분으로 넘기는게 깔끔할 수 있음.
        //{
        //    for (int i = 0; i < allEquipItem.Length; i++)
        //    {
                
        //        var item = allEquipItem[i];
        //        string statInfo = "";
        //        if (item.type == Type.Helmet)  // 작성된 스텟은 예시로 저장 회의를 통한 수정 필요
        //        {
        //            statInfo = $"체력 +{item.addMaxHp}";
        //        }
        //        else if (item.type == Type.Armor | item.type == Type.Shoes)
        //        {
        //            statInfo = $"방어력 +{item.addDefence}";
        //        }
        //        else if (item.type == Type.Weapon)
        //        {
        //            statInfo = $"공격력 +{item.addAttack}";
        //        }
        //        else if (item.type == Type.Ring)
        //        {
        //            statInfo = $"행운 +{item.addLuck}";
        //        }
        //        else if (item.type != Type.Necklace)
        //        {
        //            statInfo = $"지력 +{item.addIntelligence}";
        //        }
        //        Console.WriteLine($" [-]  {item.itemName}        | {item.type}         | {statInfo}         |{item.description}");
        //    }
        //}
        // 생성된 배열에 만들어진 아이템 목록
        public static EquipItemInfo[] allEquipItem =
        {
            new EquipItemInfo("item이름1",5,5,5,5,5,5,EquipItemType.Armor,EquipItemGrade.Normal,"아이템1"), // 아이템 1 설명작성필요
            new EquipItemInfo("item이름2",5,5,5,5,5,5,EquipItemType.Weapon, EquipItemGrade.Normal,"아이템2"), // 아이템 2 노말 무기
            new EquipItemInfo("item이름3",5,5,5,5,5,5,EquipItemType.Helmet, EquipItemGrade.Normal,"아이템3"), // 아이템 3 
            new EquipItemInfo("item이름4",5,5,5,5,5,5,EquipItemType.Shoes, EquipItemGrade.Normal,"아이템4"), // 아이템 4
            new EquipItemInfo("item이름5",5,5,5,5,5,5,EquipItemType.Ring, EquipItemGrade.Normal,"아이템5"), // 아이템 5
            new EquipItemInfo("item이름6",5,5,5,5,5,5,EquipItemType.Necklace, EquipItemGrade.Normal,"아이템6"), // 아이템 6
            new EquipItemInfo("item이름7",5,5,5,5,5,5,EquipItemType.Armor,EquipItemGrade.Uncommon,"아이템7"), // 아이템 7
            new EquipItemInfo("item이름8",5,5,5,5,5,5,EquipItemType.Weapon, EquipItemGrade.Uncommon,"아이템8"), // 아이템 8
            new EquipItemInfo("item이름9",5,5,5,5,5,5,EquipItemType.Helmet, EquipItemGrade.Uncommon,"아이템9"), // 아이템 9
            new EquipItemInfo("item이름10",5,5,5,5,5,5,EquipItemType.Shoes, EquipItemGrade.Uncommon,"아이템10"), // 아이템 10
            new EquipItemInfo("item이름11",5,5,5,5,5,5,EquipItemType.Ring, EquipItemGrade.Uncommon,"아이템11"), // 아이템 11
            new EquipItemInfo("item이름12",5,5,5,5,5,5,EquipItemType.Necklace, EquipItemGrade.Uncommon,"아이템12"), // 아이템 12
            new EquipItemInfo("item이름13",5,5,5,5,5,5,EquipItemType.Armor,EquipItemGrade.Rare,"아이템13"), // 아이템 13
            new EquipItemInfo("item이름14",5,5,5,5,5,5,EquipItemType.Weapon, EquipItemGrade.Rare,"아이템14"), // 아이템 14
            new EquipItemInfo("item이름15",5,5,5,5,5,5,EquipItemType.Helmet, EquipItemGrade.Rare,"아이템15"), // 아이템 15
            new EquipItemInfo("item이름16",5,5,5,5,5,5,EquipItemType.Shoes, EquipItemGrade.Rare,"아이템16"), // 아이템 16
            new EquipItemInfo("item이름17",5,5,5,5,5,5,EquipItemType.Ring, EquipItemGrade.Rare,"아이템17"), // 아이템 17
            new EquipItemInfo("item이름18",5,5,5,5,5,5,EquipItemType.Necklace, EquipItemGrade.Rare,"아이템18"), // 아이템 18
        };
        public static EquipItem InstanceEquipItem(int index) // 배열 index를 통해서 아이템 정보를
                                                             // 매개변수로 가진 아이템 객체 만들고 반환 
        {
            if (index >= 0 && index <= allEquipItem.Length)
            {
                EquipItemInfo equipItemInfo = allEquipItem[index];

                return new EquipItem(equipItemInfo);
            }
            else
            {
                return new EquipItem(); //아이템 index를 초과할 시 기본 아이템 정보를 가진 아이템으로 반환
            }
        }
        #region 아이템 조합 로직
        public static void ItemUpgrade()
        {
            Console.WriteLine("");
            Console.WriteLine("[아이템 조합]");
            Console.WriteLine("조합을 원하시는 아이템을 입력해주세요.");
            int number1 = Util.GetUserIntInput(1, 18) - 1; //인덱스는 0부터 시작이므로
            Console.WriteLine("조합을 원하시는 두번째 아이템을 입력해주세요.");
            int number2 = Util.GetUserIntInput(1, 18) - 1;

            //입력한 숫자가 장비아이템의 index범위내에 있는지부터 파악
            if (number1 >= 0 && number1 < allEquipItem.Length && number2 >= 0 && number2 < allEquipItem.Length)
            {
                EquipItem item1 = new EquipItem(allEquipItem[number1]); // 입력한 아이템의 숫자와 index가 일치하도록 
                EquipItem item2 = new EquipItem(allEquipItem[number2]); // 로직을 구성하여 입력한 아이템을 선택하도록

                //조합하고자 선택한 두 아이템의 타입이 동일한가, 등급이 동일한가?
                if (item1.Type == item2.Type && item1.Grade == item2.Grade)
                {
                    float upgradePercent = 0.0f; //업그레이드 퍼센트 변수 생성
                    switch (item1.Grade) //item1의 매개변수를 받아서 타입별 아이템 강화확률을 설정
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
                            return;
                    }
                    //업그레이드 확률을 랜덤으로 설정
                    Random random = new Random();
                    // 업그레이드퍼센트를 0 ~ 100퍼센트로 만들기 위한 NextDoulbe메서드 사용
                    double randomValue = random.NextDouble();

                    // 업그레이드 등급확률이 랜덤확률값보다 높을 떄 조합이 성공되도록
                    if (randomValue <= upgradePercent)
                    {
                        //업그레이드 된 아이템은 선택된 아이템 1번이 되도록
                        EquipItem upgradeItem = item1;

                        // 선택된 item의 등급에 따라서 더 높은 등급이 되도록 업그레이드
                        if (item1.Grade == EquipItemGrade.Normal)
                        {
                            upgradeItem.Grade = EquipItemGrade.Uncommon;
                        }
                        else if (item1.Grade == EquipItemGrade.Uncommon)
                        {
                            upgradeItem.Grade = EquipItemGrade.Rare;
                        }

                        int targetIndex = number1 + 6; // index를 통해서 업그레이드 된 아이템에 index에 할당되도록
                                                       // 아이템의 Type에 따라 갯수가 늘어난다면 index가 늘어나므로
                                                       // index +를 6이 아닌 다른 숫자를 입력해야함.

                        upgradeItem = new EquipItem(allEquipItem[targetIndex]);

                        Console.Clear();
                        Console.WriteLine("[조합 결과]");
                        Console.WriteLine($"조합 성공! 새로운 아이템 : {upgradeItem.Name}, {upgradeItem.Type}, {upgradeItem.Grade}");
                        Thread.Sleep(1000);
                        ReturnToInventory();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("[조합 결과]");
                        Console.WriteLine("조합 실패! 조합한 아이템이 소멸됩니다...");
                        // 조합실패시 조합된 아이템은 소멸되도록 하기 위해서 기본 아이템으로 초기화
                        new EquipItem();
                        Thread.Sleep(1000);
                        ReturnToInventory();
                    }
                }
                else // 아이템타입이나 등급이 다르다면 나올 수 있는 출력
                {
                    Console.WriteLine("같은 타입과 같은 등급의 아이템만 조합할 수 있습니다.");
                    Thread.Sleep(1000);
                    return;

                }
            }
            else //내가 선택한 아이템의 index가 유효한 범위에 없을 때 나올 수 있는 출력
            {
                Console.WriteLine("잘못된 아이템 번호가 입력되었습니다. 번호 확인 후 다시 입력해주세요.");
                Thread.Sleep(1000);
                return;
            }
        }
        #endregion

        #region 다시 인벤토리 돌아가는 메서드
        public static void ReturnToInventory() //다시 인벤토리로 돌아가는 메서드
        {
            Console.WriteLine("인벤토리로 돌아갑니다...");
            // 인벤토리 메뉴로 돌아가는 동작
            List<(String, Action, Action)> inventoryItems = new List<(string, Action, Action)>
            {
                ("나가기", GameManager.Instance.InventoryMenu, () => AudioManager.PlayMoveMenuSE(0)) // 나가기 버튼을 눌렀을 때 인벤토리 메뉴를 호출
            };
            Util.GetUserInput(inventoryItems, GameManager.Instance.InventoryMenu, ref GameManager.Instance.selectedIndex);
        }
        #endregion
    }

}
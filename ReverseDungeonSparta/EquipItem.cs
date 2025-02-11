

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
        {                                   //행운, 방어력, 공격력, 지능,  최대 체력, 최대 마력
            new EquipItemInfo("찢어진 도적의 망토",0,5,0,0,20,0,EquipItemType.Armor,EquipItemGrade.Normal,"    도적이 버리고 간 찢어진 망토"), // 아이템 1 설명작성필요
            new EquipItemInfo("치유의 지팡이",0,0,1,7,0,0,EquipItemType.Weapon, EquipItemGrade.Normal,"    마법이 부족한 지팡이"), // 아이템 2 노말 무기
            new EquipItemInfo("허름한 궁수의 모자",2,4,0,0,0,0,EquipItemType.Helmet, EquipItemGrade.Normal,"    초보자가 사용한 모자"), // 아이템 3 
            new EquipItemInfo("",0,7,0,0,10,0,EquipItemType.Shoes, EquipItemGrade.Normal,"    "), // 아이템 4
            new EquipItemInfo("item이름5",5,5,5,5,5,5,EquipItemType.Ring, EquipItemGrade.Normal,"아이템5"), // 아이템 5
            new EquipItemInfo("item이름6",5,5,5,5,5,5,EquipItemType.Necklace, EquipItemGrade.Normal,"아이템6"), // 아이템 6
            new EquipItemInfo("item이름7",5,5,5,5,5,5,EquipItemType.Armor,EquipItemGrade.Uncommon,"아이템7"), // 아이템 7
            new EquipItemInfo("item이름8",5,5,5,5,5,5,EquipItemType.Weapon, EquipItemGrade.Uncommon,"아이템8"), // 아이템 8
            new EquipItemInfo("item이름9",5,5,5,5,5,5,EquipItemType.Helmet, EquipItemGrade.Uncommon,"아이템9"), // 아이템 9
            new EquipItemInfo("item이름10",5,5,5,5,5,5,EquipItemType.Shoes, EquipItemGrade.Uncommon,"아이템10"), // 아이템 10
            new EquipItemInfo("item이름11",5,5,5,5,5,5,EquipItemType.Ring, EquipItemGrade.Uncommon,"아이템11"), // 아이템 11
            new EquipItemInfo("item이름12",5,5,5,5,5,5,EquipItemType.Necklace, EquipItemGrade.Uncommon,"아이템12"), // 아이템 12
            new EquipItemInfo("item이름13",5,5,5,5,5,5,EquipItemType.Armor,EquipItemGrade.Rare,"아이템13"), // 아이템 13
            new EquipItemInfo("태양의 활",10,0,15,0,0,0,EquipItemType.Weapon, EquipItemGrade.Rare,"아이템14"), // 아이템 14
            new EquipItemInfo("그림자의 두건",0,15,0,0,25,0,EquipItemType.Helmet, EquipItemGrade.Rare,"아이템15"), // 아이템 15
            new EquipItemInfo("용맹의 전투신발",0,15,0,0,25,0,EquipItemType.Shoes, EquipItemGrade.Rare,"아이템16"), // 아이템 16
            new EquipItemInfo("날렵한 명사수의 반지",5,0,15,0,0,0,EquipItemType.Ring, EquipItemGrade.Rare,"아이템17"), // 아이템 17
            new EquipItemInfo("힐러의 목걸이",7,0,0,10,0,0,EquipItemType.Necklace, EquipItemGrade.Rare,"아이템18"), // 아이템 18
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
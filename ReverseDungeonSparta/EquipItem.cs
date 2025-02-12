

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
        public bool IsSelected { get; set; }

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
            IsSelected = false;
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


        // 생성된 배열에 만들어진 아이템 목록
        public static EquipItemInfo[] allEquipItem =
        {
            new EquipItemInfo("찢어진 도적의 망토",0,5,0,0,25,0,EquipItemType.Armor,EquipItemGrade.Normal,"    도적이 버리고 간 찢어진 망토"),
            new EquipItemInfo("전사의 강철 갑옷",0,10,0,0,35,0,EquipItemType.Armor,EquipItemGrade.Uncommon,"    가문 대대로 내려온 강철 갑옷"),
            new EquipItemInfo("마법사의 고대 로브",5,9,0,0,0,0,EquipItemType.Armor,EquipItemGrade.Rare,"    해리포터가 사용한 고대 로브"),
            new EquipItemInfo("치유의 지팡이",0,0,2,7,0,0,EquipItemType.Weapon, EquipItemGrade.Normal,"    마법이 부족한 지팡이"),
            new EquipItemInfo("빛 바랜 단검",5,0,10,0,0,0,EquipItemType.Weapon, EquipItemGrade.Uncommon,"    옛날부터 수련에 사용된 단검"),
            new EquipItemInfo("태양의 활",10,0,15,0,0,0,EquipItemType.Weapon, EquipItemGrade.Rare,"    태양의 힘을 서린 활"),
            new EquipItemInfo("허름한 궁수의 모자",2,4,0,0,0,0,EquipItemType.Helmet, EquipItemGrade.Normal,"    초보자가 사용한 모자"),
            new EquipItemInfo("마법사의 마나의 왕관",0,10,0,0,35,0,EquipItemType.Helmet, EquipItemGrade.Uncommon,"    숙련된 마법사의 왕관"),
            new EquipItemInfo("그림자의 두건",0,15,0,0,50,0,EquipItemType.Helmet, EquipItemGrade.Rare,"    도적 군집이 사용했던 그림자 두건"),
            new EquipItemInfo("마법사의 천 신발",0,7,0,0,25,0,EquipItemType.Shoes, EquipItemGrade.Normal,"    견습생이 신는 신발"),
            new EquipItemInfo("도적의 철의 발걸음",10,10,0,0,0,0,EquipItemType.Shoes, EquipItemGrade.Uncommon,"    대장장이가 만든 도적의 철 신발"),
            new EquipItemInfo("용맹의 전투신발",0,15,0,0,50,0,EquipItemType.Shoes, EquipItemGrade.Rare,"    용맹함이 가득한 전사의 신발"),
            new EquipItemInfo("금이 간 전사의 반지",0,0,0,10,0,25,EquipItemType.Ring, EquipItemGrade.Normal,"    전투하다 금이 간 전사의 반지"),
            new EquipItemInfo("힐러의 생명의 반지",0,0,0,15,0,35,EquipItemType.Ring, EquipItemGrade.Uncommon,"    지능을 상당히 증가시키는 반지"),
            new EquipItemInfo("날렵한 명사수의 반지",5,0,15,0,0,0,EquipItemType.Ring, EquipItemGrade.Rare,"    일등사수가 사용했던 명사수의 반지"),
            new EquipItemInfo("생명의 구슬 목걸이",0,0,0,10,0,25,EquipItemType.Necklace, EquipItemGrade.Normal,"    여러 사람 구한 힐러의 목걸이"),
            new EquipItemInfo("명사의 목걸이",0,0,15,0,0,35,EquipItemType.Necklace, EquipItemGrade.Uncommon,"    백발백중 명사가 사용했던 목걸이"),
            new EquipItemInfo("힐러의 목걸이",0,0,0,10,0,50,EquipItemType.Necklace, EquipItemGrade.Rare,"    신성력이 가득한 힐러의 목걸이"),
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
            List<(String, Action, Action?)> inventoryItems = new List<(string, Action, Action?)>
            {
                ("나가기", GameManager.Instance.InventoryMenu, () => AudioManager.PlayMoveMenuSE(0)) // 나가기 버튼을 눌렀을 때 인벤토리 메뉴를 호출
            };
            Util.GetUserInput(inventoryItems, GameManager.Instance.InventoryMenu, ref GameManager.Instance.selectedIndex);
        }
        #endregion
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReverseDungeonSparta.UsableItem;

namespace ReverseDungeonSparta
{
    public class EquipItem : Item
    {
        public enum Grade
        {
            Normal,
            Uncommon,
            Rare,
        }
        public enum Type
        {
            Armor,
            Weapon,
            Helmet,
            Shoes,
            Ring,
            Necklace
        }
        int AddLuck {  get; set; }
        int AddDefence {  get; set; }
        int AddAttack { get; set; }
        int AddIntelligence {  get; set; }
        int AddMaxHp {  get; set; }
        int AddMaxMp {  get; set; }
        public EquipItem(EquipItemInfo equipItemInfo)
        {

        }
        public EquipItem()
        {

        }

        // 장착한 아이템 구조체
        public struct EquipItemInfo
        {
            public int addLuck;
            public int addDefence;
            public int addAttack;
            public int addIntelligence;
            public int addMaxHp;
            public int addMaxMp;

            public EquipItemInfo(int _addLuck, int _addDefence, int _addAttack, int _addIntelligence, int _addMaxHp, int _addMaxMp, Type type, Grade grade)
            {
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
            new EquipItemInfo(5,5,5,5,5,5,Type.Armor,Grade.Normal), // 아이템 1
            new EquipItemInfo(5,5,5,5,5,5,Type.Weapon, Grade.Normal), // 아이템 2
            new EquipItemInfo(5,5,5,5,5,5,Type.Helmet, Grade.Normal), // 아이템 3 
            new EquipItemInfo(5,5,5,5,5,5,Type.Shoes, Grade.Normal), // 아이템 4
            new EquipItemInfo(5,5,5,5,5,5,Type.Ring, Grade.Normal), // 아이템 5
            new EquipItemInfo(5,5,5,5,5,5,Type.Necklace, Grade.Normal), // 아이템 6
            new EquipItemInfo(5,5,5,5,5,5,Type.Armor,Grade.Uncommon), // 아이템 7
            new EquipItemInfo(5,5,5,5,5,5,Type.Weapon, Grade.Uncommon), // 아이템 8
            new EquipItemInfo(5,5,5,5,5,5,Type.Helmet, Grade.Uncommon), // 아이템 9
            new EquipItemInfo(5,5,5,5,5,5,Type.Shoes, Grade.Uncommon), // 아이템 10
            new EquipItemInfo(5,5,5,5,5,5,Type.Ring, Grade.Uncommon), // 아이템 11
            new EquipItemInfo(5,5,5,5,5,5,Type.Necklace, Grade.Uncommon), // 아이템 12
            new EquipItemInfo(5,5,5,5,5,5,Type.Armor,Grade.Rare), // 아이템 13
            new EquipItemInfo(5,5,5,5,5,5,Type.Weapon, Grade.Rare), // 아이템 14
            new EquipItemInfo(5,5,5,5,5,5,Type.Helmet, Grade.Rare), // 아이템 15
            new EquipItemInfo(5,5,5,5,5,5,Type.Shoes, Grade.Rare), // 아이템 16
            new EquipItemInfo(5,5,5,5,5,5,Type.Ring, Grade.Rare), // 아이템 17
            new EquipItemInfo(5,5,5,5,5,5,Type.Necklace, Grade.Rare), // 아이템 6
        };

        public static List<EquipItem> GetEquipItemList(int num)  // 만들어진 장착장비리스트에 아이템 객체 넣기 
        {
            List<EquipItem> equipItemList = new List<EquipItem>();
            for (int i = 0; i < num; i++)
            {
                equipItemList.Add(InstanceEquipItem(i));
            }
            return equipItemList;

        }
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
    }
}

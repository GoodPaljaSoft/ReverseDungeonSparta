using Microsoft.VisualBasic;
using ReverseDungeonSparta;
using System.Xml.Linq;


namespace ReverseDungeonSparta
{

    public class UsableItem : Item
    {
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Count { get; set; }


        public UsableItem(UsableItemInfo u)
        {
            Name = u.name;
            Information = u.info;
            Hp = u.hp;
            Mp = u.mp;
            Count = u.count;
        }

        public UsableItem()
        {
            UsableItem item = new UsableItem();
        }
    }


    // 소비 아이템 구조체
    public struct UsableItemInfo
    {
        public string name;
        public string info;
        public int hp;
        public int mp;
        public int count;

        public UsableItemInfo(string _name, string _info, int _hp, int _mp, int _count)
        {
            name = _name;
            info = _info;
            hp = _hp;
            mp = _mp;
            count = _count;
        }


        // 소비 아이템 정보
        public static UsableItemInfo[] allUsableItem =
        {
        new UsableItemInfo("하급 체력 회복 포션", "플레이어의 HP를 30 회복합니다.", 30, 0, 0),
        new UsableItemInfo("중급 체력 회복 포션", "플레이어의 HP를 50 회복합니다.", 50, 0, 0),
        new UsableItemInfo("상급 체력 회복 포션", "플레이어의 HP를 70 회복합니다.", 70, 0, 0),
        new UsableItemInfo("하급 마나 회복 포션", "플레이어의 MP를 30 회복합니다.", 0, 30, 0),
        new UsableItemInfo("중급 마나 회복 포션", "플레이어의 MP를 50 회복합니다.", 0, 50, 0),
        new UsableItemInfo("상급 마나 회복 포션", "플레이어의 MP를 70 회복합니다.", 0, 70, 0)
        };

    }
}

using Microsoft.VisualBasic;
using System.Xml.Linq;


namespace ReverseDungeonSparta.Entiity
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
            Count = 1;
        }
        public UsableItem(UsableItemInfo u,int count)
        {
            Name = u.name;
            Information = u.info;
            Hp = u.hp;
            Mp = u.mp;
            Count = count;
        }

        public UsableItem()
        {
           
        }
    }


    // 소비 아이템 구조체
    public struct UsableItemInfo
    {
        public string name;
        public string info;
        public int hp;
        public int mp;
        

        public UsableItemInfo(string _name, string _info, int _hp, int _mp)
        {
            name = _name;
            info = _info;
            hp = _hp;
            mp = _mp;
        }
    }
}

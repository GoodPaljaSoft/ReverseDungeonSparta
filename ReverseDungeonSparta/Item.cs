using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monster;

namespace ReverseDungeonSparta
{
    public class Item
    {
        string Name { get; set; }

        string Information { get; set; }
        public Item()
        {
            Name = string.Empty;
            Information = string.Empty;
        }

        // 아이템 구조체
        public struct ItemInfo
        {
            public string name;
            public string information;

            public ItemInfo(string _name, string _Information)
            {
                name = _name;
                information = _Information;
            }
        }

        public static ItemInfo[] allItem =
        {
            new ItemInfo("Item1", "20"),
            new ItemInfo("Item2", "30"),
            new ItemInfo("Item3", "10"),
        };

        
    }
}

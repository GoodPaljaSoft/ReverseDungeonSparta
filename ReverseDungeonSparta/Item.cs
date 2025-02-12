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
        public string Name { get; set; }

        public string Information { get; set; }
        public Item()
        {
            Name = string.Empty;
            Information = string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta.Manager
{
    public sealed class ItemManager : Manager<ItemManager>
    {
        public void ItemInit()
        {
            Console.WriteLine("아이템이닛");
            Console.ReadLine();
        }

    }
}

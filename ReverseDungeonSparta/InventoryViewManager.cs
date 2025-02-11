using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReverseDungeonSparta.ViewManager;
using static ReverseDungeonSparta.ViewManager3;

namespace ReverseDungeonSparta
{
    public static class InventoryViewManager
    {
        public static void EnterInventoryMenuTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지 아이템", 12);
            ViewManager.PrintText(0, 27, "   장비 아이템");
            ViewManager.PrintText("소지 아이템");
            ViewManager.PrintText("나가기");


        }

    }
    

}

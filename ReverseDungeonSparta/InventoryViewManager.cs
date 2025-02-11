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
            PrintTitleTxt("소지품 확인", 12);
            ViewManager.PrintText(0, 27, "   장비 아이템");
            ViewManager.PrintText("   소지 아이템");
            ViewManager.PrintText("   나가기");
        }

        public static void InventoryEquippedMenuTxt(List<EquipItem> itemList, ref int selectedIndex, (int, int) cursor)
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 장비", 12);
            ViewManager.PrintText(0, 27, "   장비 장착");
            ViewManager.PrintText("   장비 합성");
            ViewManager.PrintText("[C]나가기");

            List<(string, Action, Action)> itemScrollView = itemList
                                                        .Select(x => (InventorySortList(x), (Action)null, (Action)null))
                                                        .ToList();

            ScrollViewTxt(itemScrollView, ref selectedIndex, cursor);
        }

        public static string InventorySortList(EquipItem equipItem)
        {
            int[] optionArray = { equipItem.AddLuck, equipItem.AddDefence, equipItem.AddAttack, equipItem.AddIntelligence, equipItem.AddMaxHp, equipItem.AddMaxMp };
            string[] nameArray = { "AddLuck", "AddDefence", "AddAttack", "AddIntelligence", "AddMaxHp", "AddMaxMp" };
            StringBuilder sb = new StringBuilder();
            bool isEquipped = equipItem.IsEquiped;
            sb.Append(isEquipped ? " [E] ".PadRight(5) : " [-] ".PadRight(5));
            sb.Append(equipItem.Name.PadRight(20));
            sb.Append($"| {TranslateString(equipItem.Type.ToString())}");
            sb.Append($"{equipItem.Information}\n");
            for(int i = 0; i<optionArray.Length; i++)
            {
                if (optionArray[i] != 0)
                {
                    sb.Append($"|{TranslateString(nameArray[i])} +{optionArray[i]}".PadLeft(50));
                } 
            }
            return sb.ToString();
        }
        public static string TranslateString(string enumType)
        {
            switch (enumType)
            {
                case "Armor":
                    return "방어구";
                case "Weapon":
                    return "무  기";
                case "Helmet":
                    return "모  자";
                case "Shoes":
                    return "신  발";
                case "Ring":
                    return "반  지";
                case "Necklace":
                    return "목걸이";
                case "AddLuck":
                    return "행  운";
                case "AddDefence":
                    return "방어력";
                case "AddAttack":
                    return "공격력";
                case "AddIntelligence":
                    return "지  력";
                case "AddMaxHp":
                    return "체  력";
                case "AddMaxMp":
                    return "마  나";
                default:
                    return "TranslateError";
            }
        }
    }
    

}

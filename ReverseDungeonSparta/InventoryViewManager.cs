using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ReverseDungeonSparta.ViewManager;
using static ReverseDungeonSparta.ViewManager3;

namespace ReverseDungeonSparta
{
    public static class InventoryViewManager
    {
        //인벤토리 메인메뉴에 진입하면 출력할 메서드
        public static void EnterInventoryMenuTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인");
            ViewManager.PrintText(0, 26, "   장비 장착");
            ViewManager.PrintText("   장비 합성");
            ViewManager.PrintText("   아이템 사용");
            ViewManager.PrintText("[C]나가기");
        }


        //인벤토리 소지품 메뉴에 들어가면 출력할 메서드
        public static void InventoryEquippedItemMenuTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 장비");
            ViewManager.PrintText(0, 27, "   장비 장착");
            ViewManager.PrintText("   장비 합성");
            ViewManager.PrintText("[C]나가기");
        }


        //소지품 메뉴에서 아이템 장착에 들어가면 출력할 메서드
        public static void InventoryEquippedItemTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 장비");
            ViewManager.PrintText(0, 29, "[C]나가기");
        }


        //인멘토리 메뉴에서 소지품 사용에 들어가면 출력할 메서드
        public static void InventoryUseItemTxt(Player player)
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 소비 아이템 사용");
            ViewManager3.PrintPlayerStatus(player);
            ViewManager.PrintText(0, 26, "[Enter]아이템 사용");
            ViewManager.PrintText("");
            ViewManager.PrintText("");
            ViewManager.PrintText("[C]나가기");
        }


        //인벤토리를 정렬할 때 사용할 메서드
        public static string SortEquippedItemList(EquipItem equipItem)
        {

            int count = 0;
            int[] optionArray = { equipItem.AddLuck, equipItem.AddDefence, equipItem.AddAttack, equipItem.AddIntelligence, equipItem.AddMaxHp, equipItem.AddMaxMp };
            string[] nameArray = { "AddLuck", "AddDefence", "AddAttack", "AddIntelligence", "AddMaxHp", "AddMaxMp" };
            StringBuilder sb = new StringBuilder();
            bool isEquipped = equipItem.IsEquiped;
            sb.Append(isEquipped ? " [E] ".PadRight(5) : " [-] ".PadRight(5));
            sb.Append(Util.SortPadRight(equipItem.Name, 24));
            sb.Append(Util.SortPadRight($"| {TranslateString(equipItem.Type.ToString())} ", 8 ));
            for(int i = 0; i<optionArray.Length; i++)
            {

                if (optionArray[i] != 0)
                {
                    sb.Append(Util.SortPadRight($"| {TranslateString(nameArray[i])} +{optionArray[i]} ",13 ));
                    sb.Append(Util.SortPadRight($"|", 2));
                    if (count == 0)
                    {
                        sb.Append($"{equipItem.Information}\n");
                        sb.Append(Util.SortPadRight($"", 41));
                    }
                    count++;
                } 
            }
            return sb.ToString();
        }


        //아이템의 타입 별로 string을 반환하는 메서드
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


        //소비 아이템의 정보를 정렬할 때 사용할 메서드
        public static string SortUseItemList(UsableItem usableItem)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{usableItem.Name.SortPadRight(10)} | ");
            if (usableItem.Hp > 0) sb.Append($"HP +{usableItem.Hp}".SortPadRight(10) + " | ");
            if (usableItem.Mp > 0) sb.Append($"MP +{usableItem.Mp}".SortPadRight(10) + " | ");
            sb.Append($"{usableItem.Information.SortPadRight(20)}" + " | 보유 수 : ");
            sb.Append($"{usableItem.Count}개");

            return sb.ToString();
        }
    }
}

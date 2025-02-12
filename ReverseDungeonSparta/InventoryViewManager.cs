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
        //인벤토리 메인메뉴에 진입하면 출력할 메서드
        public static void EnterInventoryMenuTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인", 12);
            ViewManager.PrintText(0, 27, "   장비 아이템");
            ViewManager.PrintText("   소지 아이템");
            ViewManager.PrintText("[C]나가기");
        }


        //인벤토리 소지품 메뉴에 들어가면 출력할 메서드
        public static void InventoryEquippedItemMenuTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 장비", 12);
            ViewManager.PrintText(0, 27, "   장비 장착");
            ViewManager.PrintText("   장비 합성");
            ViewManager.PrintText("[C]나가기");
        }


        //소지품 메뉴에서 아이템 장착에 들어가면 출력할 메서드
        public static void InventoryEquippedItemTxt()
        {
            Console.Clear();
            PrintTitleTxt("소지품 확인 - 장비", 12);
            ViewManager.PrintText(0, 29, "[C]나가기");
        }


        //인멘토리 메뉴에서 소지품 확인에 들어가면 출력할 메서드



        //인벤토리를 정렬할 때 사용할 메서드
        public static string InventorySortList(EquipItem equipItem)
        {

            int count = 0;
            int[] optionArray = { equipItem.AddLuck, equipItem.AddDefence, equipItem.AddAttack, equipItem.AddIntelligence, equipItem.AddMaxHp, equipItem.AddMaxMp };
            string[] nameArray = { "AddLuck", "AddDefence", "AddAttack", "AddIntelligence", "AddMaxHp", "AddMaxMp" };
            StringBuilder sb = new StringBuilder();
            bool isEquipped = equipItem.IsEquiped;
            sb.Append(isEquipped ? " [E] ".PadRight(5) : " [-] ".PadRight(5));
            sb.Append(Util.SortPadRightItemList(equipItem.Name, 24));
            sb.Append(Util.SortPadRightItemList($"| {TranslateString(equipItem.Type.ToString())} ", 8 ));
            for(int i = 0; i<optionArray.Length; i++)
            {

                if (optionArray[i] != 0)
                {
                    sb.Append(Util.SortPadRightItemList($"| {TranslateString(nameArray[i])} +{optionArray[i]} ",13 ));
                    sb.Append(Util.SortPadRightItemList($"|", 2));
                    if (count == 0)
                    {
                        sb.Append($"{equipItem.Information}\n");
                        sb.Append(Util.SortPadRightItemList($"", 41));
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
    }
    

}

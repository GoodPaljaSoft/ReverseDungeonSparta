

using System.Numerics;
using System.Runtime.InteropServices;

namespace ReverseDungeonSparta
{
    static internal class Util
    {
        public static void PrintChoice<T>(T[] selection)
        {
            for (int i = 1; i <= selection.Length; i++)
            {
                Console.WriteLine($"{i}. {selection[i - 1]}");
            }
            Console.WriteLine("");
        }
        public static void PrintPlayerView(Player player) 
        {
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name}( {player.Job} )");
            Console.WriteLine($"공격력 : {player.Attack}+({player.AdditionalAttack})");
            Console.WriteLine($"방어력 : {player.Defence}+({player.AdditionalDefence})");
            Console.WriteLine($"체력 : {player.NowHealth}");
            Console.WriteLine($"체력 : {player.NowHealth}");
            Console.WriteLine($"Gold : {player.Gold}");
            Console.WriteLine("");
        }
        public static int GetUserInput(int minCount, int maxCount)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            //커서복귀를 위한 커서위치 저장
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            int result;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result <= maxCount && result >= minCount)//유효한 입력
                    {
                        return result;
                    }
                    else Console.WriteLine($"{minCount}에서{maxCount}사이의 숫자를 입력해주세요");
                }
                Console.SetCursorPosition(x, y);
            }
        }
        //public static Void PrintItemList(List<item> ItemList) { }
    }
}

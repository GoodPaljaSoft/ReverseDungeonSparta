

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
            Console.WriteLine($"최대 체력 : {player.MaxHP}");
            Console.WriteLine($"현재 체력 : {player.HP}");
            Console.WriteLine($"Gold : {player.Gold}");
            Console.WriteLine("");
        }

        public static int GetUserIntInput(int minCount, int maxCount)
        {
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
                    else
                    {
                        Console.WriteLine($"{minCount}에서{maxCount}사이의 숫자를 입력해주세요");
                        AudioManager.PlayMoveMenuSE(0);
                    }
                }
                Console.SetCursorPosition(x, y);
            }
        }


        //public static Void PrintItemList(List<item> ItemList) { }


        //menuList에 있는 String은 화살표를 붙일 선택지의 문자열
        //menuList에 있는 Action은 해당 화살표에 있는 메소드를 실행시킵니다.
        //nowMenu는 화살표의 출력을 움직이기 위해 본인 메서드를 재귀적으로 다시 호출합니다.
        //selectedIndex는 해당 화살표의 위치 값을 저장합니다.
        //해당 값을 전달 받고 다시 바뀐 값을 보내줘야 하기에 ref를 꼭 넣어야합니다.

        public static void GetUserInput(List<(String, Action, Action)> menuList, Action nowMenu, ref int selectedIndex)
        {
            for (int i = 0; i < menuList.Count; i++)
            {
                if (i == selectedIndex) Console.WriteLine($"-> {menuList[i].Item1}");
                else                    Console.WriteLine($"   {menuList[i].Item1}");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:    //위 화살표를 눌렀을 때
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                        AudioManager.PlayMoveMenuSE(0);
                    }
                        nowMenu();
                    break;

                case ConsoleKey.DownArrow:  //아래 화살표를 눌렀을 때
                    if (selectedIndex < menuList.Count - 1) 
                    {
                        selectedIndex++;
                        AudioManager.PlayMoveMenuSE(0);
                    }
                    nowMenu();
                    break;

                case ConsoleKey.Enter:      //엔터를 눌렀을 때
                    int tempIndex = selectedIndex;
                    selectedIndex = 0;      //selectedIndex 초기화
                    if (menuList[tempIndex].Item3 != null)
                    {
                        menuList[tempIndex].Item3();
                    }
                    menuList[tempIndex].Item2();
                    break;

                default:                    //상관 없는 키가 눌렸을 때
                    nowMenu();
                    break;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ReverseDungeonSparta.EquipItem;

namespace ReverseDungeonSparta
{
    static class ViewManager
    {
        //화면 넓이 받아오기
        static public int width = 120;      //콘솔 가로 크기
        static public int height = 30;      //콘솔 세로 크기

        static int x = 0;   //마우스 커서 x위치
        static int y = 0;   //마우스 커서 y위치

        //커서 위치 받아오기
        static int top = Console.WindowTop;
        static int left = Console.WindowLeft;

        public static void StateView(Player player)
        {
            //테스트 코드
            //DrawLine("상태보기");
            //DrawList(player);
            //DrawLine();

            //Console.WriteLine($"width: {width}");
            //Console.WriteLine($"height: {height}");
            //Console.WriteLine($"top: {top}");
            //Console.WriteLine($"left: {left}");


            //Console.ReadLine();
        }


        //한 줄을 길게 그리고 위에 제목을 출력하는 메소드
        public static void DrawLine(string sceneName)
        {

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n {sceneName}");
            DrawLine();

            Console.ForegroundColor = ConsoleColor.White;

        }


        //한 줄을 길게 그리는 메서드
        public static void DrawLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width; i++)
            {
                Console.Write("─");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        
        //플레이어 정보를 출력할 메서드
        public static void PrintPlayerState(Player player)
        {
            PrintText(1, 3, $"{player.Name} (마왕)");

            Console.SetCursorPosition(1, 3);
            Console.WriteLine($"{player.Name} (마왕)");
            Console.WriteLine($" Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            Console.WriteLine();
            //Console.WriteLine($" Lv. {player.Level} [{player.Exp}/{player.MaxExp}]");
        }


        //해당 메서드로 커서 위치를 잡고 텍스트를 출력한다.
        //이 메서드로 출력한 텍스트를 기준으로 한 줄 아래에 그리고 싶다면
        //이 다음으로 PrintText로 출력하면 된다.
        public static void PrintText(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            y++;
        }


        //해당 메서드로 위치를 잡은 커서를 기준으로 텍스트를 출력한다.
        public static void PrintText(string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            y++;
        }

        //public void PrintList(int x = 1, int y = 4, List<ItemInfo> items)
        //{
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        PrintText(x, y + i, $"{items[0].name}");
        //    }

        //}

    }
}

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
    class ViewManager
    {
        //화면 넓이 받아오기
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        //커서 위치 받아오기
        int top = Console.WindowTop;
        int left = Console.WindowLeft;

        public static ViewManager Instance { get; } = new ViewManager();

        public ViewManager()
        {
            //콘솔창 크기 지정
            Console.SetWindowSize(120, 30);


            //아이템 테스트를 위한 임시 아이템 목록
            List<EquipItemInfo> tempItems = new List<EquipItemInfo>();
            //tempItems.Add("아이템1",);



        }

        public void StateView(Player player)
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

        public void DrawLine(string sceneName)
        {

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n {sceneName}");
            DrawLine();

            Console.ForegroundColor = ConsoleColor.White;

        }


        public void DrawLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width; i++)
            {
                Console.Write("─");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintPlayerState(Player player)
        {
            PrintText(1, 3, $"{player.Name} (마왕)");

            Console.SetCursorPosition(1, 3);
            Console.WriteLine($"{player.Name} (마왕)");
            Console.WriteLine($" Lv. {player.Level} [{player.Exp}/{player.MaxExp}]");
            Console.WriteLine();
            //Console.WriteLine($" Lv. {player.Level} [{player.Exp}/{player.MaxExp}]");
        }

        public void PrintText(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
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

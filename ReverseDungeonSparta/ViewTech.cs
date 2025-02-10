
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ReverseDungeonSparta
{
    public class ViewTech
    {
        int selectedIndex = 1;

        public static void GetUserInput2(List<(string, Action, Action)> menuList, Action nowMenu, ref int selectedIndex)
        {
            int maxVisibleOption = 5;
            int startIndex =Math.Min(menuList.Count- maxVisibleOption, Math.Max(0, selectedIndex-2)); // 선택지가 중간에 오도록 5라서 2임
            int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시
            
            while (true)
            {
                Console.Clear();

                // 현재 선택지 표시
                if (menuList.Count < maxVisibleOption)
                {
                    for (int i = 0; i < menuList.Count; i++)
                    {
                        if (i == selectedIndex)
                            Console.WriteLine($"-> {menuList[i].Item1}");
                        else
                            Console.WriteLine($"   {menuList[i].Item1}");
                    }
                }
                else
                {
                    // 위로 숨겨진 선택지 개수
                    Console.WriteLine($"↑ ({startIndex}개)");
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (i == selectedIndex)
                            Console.WriteLine($"-> {menuList[i].Item1}");
                        else
                            Console.WriteLine($"   {menuList[i].Item1}");
                    }
                    // 아래로 숨겨진 선택지 개수 표시
                    Console.WriteLine($"↓ ({menuList.Count - endIndex} more)");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow: // 위 화살표를 눌렀을 때
                        if (selectedIndex > 0)
                        {
                            selectedIndex--;
                            // 선택지가 3번째 줄 이상이면 이동만, 아니면 리스트 스크롤
                            if (selectedIndex < startIndex)
                            {
                                startIndex--;
                                endIndex--;
                            }                   
                            AudioManager.PlayMoveMenuSE(0);
                        }
                        nowMenu();
                        break;

                    case ConsoleKey.DownArrow: // 아래 화살표를 눌렀을 때
                        if (selectedIndex < menuList.Count - 1)
                        {
                            selectedIndex++;  
                            // 선택지가 뒤에서 3번째 줄 이하이면 이동만, 아니면 리스트 스크롤
                            if (selectedIndex >= endIndex)
                            {
                                startIndex++;
                                endIndex++;
                            }

                            AudioManager.PlayMoveMenuSE(0);
                        }
                        nowMenu();
                        break;

                    case ConsoleKey.Enter:
                        int tempIndex = selectedIndex;
                        selectedIndex = 0;
                        menuList[tempIndex].Item2();
                        nowMenu();
                        return;
                }
            }
        }
    }
}


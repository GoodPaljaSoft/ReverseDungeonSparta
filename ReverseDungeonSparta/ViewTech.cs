
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ReverseDungeonSparta
{

    public static class ViewManager2
    {
        private static StringBuilder previousScreen = new StringBuilder();
        private static int lastPrintedLines = 0;

        /// 콘솔 화면을 갱신하는 메서드 (깜빡임 없는 UI 업데이트) 사용법
        /// 모든 뷰에 StringBuilder sb = new StringBuilder; 추가
        /// 모든 ConSole.Write()를 sb.AppendLine()으로 변경
        /// 모든 ConSole.Clear()를 Refresh(sb)로 변경
      
        public static void Refresh(StringBuilder sb)
        {
            // 콘솔 커서를 최상단으로 이동 (Console.Clear() 대신 사용)
            Console.SetCursorPosition(0, 0);

            // 변경된 부분만 출력 (이전 화면과 비교)
            if (!sb.ToString().Equals(previousScreen.ToString()))
            {
                Console.Write(sb.ToString());
                previousScreen = sb;
            }

            // 이전보다 줄이 적을 경우, 남은 공간을 덮어쓰기 위해 공백 추가
            int blankLines = lastPrintedLines - sb.ToString().Split('\n').Length;
            for (int i = 0; i < blankLines; i++)
            {
                Console.WriteLine(new string(' ', Console.BufferWidth));
            }

            // 마지막으로 출력된 줄 수 업데이트
            lastPrintedLines = sb.ToString().Split('\n').Length;
        }
    }
    public static class ViewTech
    {
        public static void GetUserInput2(List<(string, Action, Action?)> menuList, Action nowMenu, ref int selectedIndex)
        {
            int maxVisibleOption = 5;
            int startIndex =Math.Min(menuList.Count- maxVisibleOption, Math.Max(0, selectedIndex-2)); // 선택지가 중간에 오도록 5라서 2임
            int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시

            bool isBreak = false;
            while (isBreak == false)
            {
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
                        break;

                    case ConsoleKey.Enter:
                        int tempIndex = selectedIndex;
                        selectedIndex = 0;
                        menuList[tempIndex].Item2();
                        nowMenu();
                        return;

                    case ConsoleKey.C:
                        AudioManager.PlayMoveMenuSE(0);
                        isBreak = true;
                        return;
                }
                if (isBreak) break;
            }
        }

        public static void GetUserInputCursorList(List<(string, Action, Action?)> menuList, ref int selectedIndex, (int, int) cursor)
        {
            int maxVisibleOption = 5;
            int startIndex = Math.Min(menuList.Count - maxVisibleOption, Math.Max(0, selectedIndex - 2)); // 선택지가 중간에 오도록 5라서 2임
            int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시

            bool isBreak = false;
            while (isBreak == false)
            {
                ViewManager.PrintText(cursor.Item1, cursor.Item2, "");
                // 현재 선택지 표시
                if (menuList.Count < maxVisibleOption)
                {
                    for (int i = 0; i < menuList.Count; i++)
                    {
                        string str = "";
                        if (i == selectedIndex)
                            str = ($"-> {menuList[i].Item1}");
                        else
                            str = ($"   {menuList[i].Item1}");
                        Console.WriteLine(str);
                    }
                }
                else
                {
                    // 위로 숨겨진 선택지 개수
                    Console.WriteLine($"↑ ({startIndex}개)");
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        string str = "";
                        if (i == selectedIndex)
                            str = ($"-> {menuList[i].Item1}");
                        else
                            str = ($"   {menuList[i].Item1}");
                        Console.WriteLine(str);
                    }
                    // 아래로 숨겨진 선택지 개수 표시
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.WriteLine($"↓ ({menuList.Count - endIndex} more)");
                }

                ConsoleKeyInfo keyInfo = Util.CheckKeyInputExceptionEnter(selectedIndex, menuList.Count - 1);

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
                        break;

                    case ConsoleKey.Enter:
                        int tempIndex = selectedIndex;
                        if (menuList[tempIndex].Item2 != null)
                        {
                            selectedIndex = 0;
                            menuList[tempIndex].Item2();
                        }
                        return;

                    case ConsoleKey.C:
                        AudioManager.PlayMoveMenuSE(0);
                        isBreak = true;
                        selectedIndex = 0;
                        return;
                }
                if (isBreak) break;
            }
        }
    }
}


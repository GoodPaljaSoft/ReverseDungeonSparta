﻿using System.Numerics;
using System.Runtime.InteropServices;
using ReverseDungeonSparta.Entiity;
using ReverseDungeonSparta.Manager;

namespace ReverseDungeonSparta
{

    static internal class Util
    {
        //menuList에 있는 String은 화살표를 붙일 선택지의 문자열
        //menuList에 있는 Action은 해당 화살표에 있는 메소드를 실행시킵니다.
        //menuList에 있는 1번 째 Action은 해당 화살표에 있는 메소드를 실행시킵니다.
        //menuList에 있는 2번 째 Action은 해당 화살표에 있는 음성 파일을 실행시킵니다. null일 경우 소리 없이 출력
        //nowMenu는 화살표의 출력을 움직이기 위해 본인 메서드를 재귀적으로 다시 호출합니다.
        //selectedIndex는 해당 화살표의 위치 값을 저장합니다.
        //해당 값을 전달 받고 다시 바뀐 값을 보내줘야 하기에 ref를 꼭 넣어야합니다.
        //화살표 위치 입력가능한 인풋 stirng 필요 없음***
        public static void GetUserInput(List<(String, Action, Action?)> menuList, Action nowMenu, ref int selectedIndex, (int, int) cursor)
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                ViewManager.PrintText(cursor.Item1, cursor.Item2, "");
                for (int i = 0; i < menuList.Count; i++)
                {
                    string str = "";
                    if (i == selectedIndex) str = ($"-> ");
                    else str = ($"   ");
                    ViewManager.PrintText(str);
                }
                keyInfo = CheckKeyInput(selectedIndex, menuList.Count - 1);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:    //위 화살표를 눌렀을 때
                        selectedIndex--;
                        AudioManager.PlayMoveMenuSE(0);
                        break;

                    case ConsoleKey.DownArrow:  //아래 화살표를 눌렀을 때
                        selectedIndex++;
                        AudioManager.PlayMoveMenuSE(0);
                        break;

                    case ConsoleKey.Enter:      //엔터를 눌렀을 때
                        int tempIndex = selectedIndex;
                        selectedIndex = 0;      //selectedIndex 초기화
                        if (menuList[tempIndex].Item3 != null) { menuList[tempIndex].Item3(); }
                        menuList[tempIndex].Item2();
                        break;
                }
                if (keyInfo.Key == ConsoleKey.Enter) break;
            }
        }

        //리스트를 무작위로 섞고 반환하는 메서드
        public static List<T> ShuffleList<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--) //리스트의 뒤쪽 요소부터 선택하여 실행
            {
                int j = random.Next(0, i + 1);//0 ~ i까지 무작위 인덱스 선택
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        //입력 받은 키의 값이 화살표 이동에 적합한지 확인하는 메서드
        public static ConsoleKeyInfo CheckKeyInput(int nowIndex, int maxIndex)
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter ||
                    keyInfo.Key == ConsoleKey.C ||
                    (keyInfo.Key == ConsoleKey.UpArrow && nowIndex > 0) ||
                    keyInfo.Key == ConsoleKey.DownArrow && nowIndex < maxIndex)
                {
                    break;
                }
            }
            return keyInfo;
        }

        //입력 받은 키의 값이 화살표 이동에 적합한지 확인하는 메서드
        public static ConsoleKeyInfo CheckKeyInput()
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter ||
                    keyInfo.Key == ConsoleKey.C )
                {
                    break;
                }
            }
            return keyInfo;
        }
        
        public static void CheckKeyInputEnter()
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter) 
                {
                    //***AudioManager.PlayMoveMenuSE(0);
                    break; 
                }
            }
        }

        public static ConsoleKeyInfo CheckKeyInputExceptionEnter(int nowIndex, int maxIndex)
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.C ||
                    (keyInfo.Key == ConsoleKey.UpArrow && nowIndex > 0) ||
                    keyInfo.Key == ConsoleKey.DownArrow && nowIndex < maxIndex)
                {
                    break;
                }
            }
            return keyInfo;
        }

        //해당 텍스트에 한글이 얼마나 들어있는지 확인하고 정렬의 수를 조절하는 메서드 /사용 예정
        public static string SortPadRight(this string input, int defaultLength)
        {
            int countKOR = input.Count(x => x >= 0xAC00 && x <= 0xD7A3);
            return input.PadRight(defaultLength - countKOR, ' ');
        }
    }
}

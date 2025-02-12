using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{

    public static class ViewManager3
    {
        static public int monsterPostionValueX = 50;
        static public int monsterPostionValueY = 2;


        //제목과 층을 출력할 타이틀 텍스트
        public static void PrintTitleTxt(string title, int floor)
        {
            ViewManager.PrintText(0, 1, title);
            ViewManager.PrintText(ViewManager.width - 30, 1, $"현재 위치: 스파르타 던전 {floor}층");
            Console.WriteLine("");
            ViewManager.DrawLine();
        }

        //제목을 출력할 타이틀 텍스트
        public static void PrintTitleTxt(string title)
        {
            ViewManager.PrintText(0, 1, title);
            Console.WriteLine("");
            ViewManager.DrawLine();
        }


        //플레이어 정보를 출력할 메서드
        public static void PrintPlayerBattleStatus(Player player)
        {
            ViewManager.PrintText(0, 3, $"{player.Name} (마왕)");
            ViewManager.PrintText($"Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            ViewManager.PrintText("");
            ViewManager.PrintText($"HP : {player.HP}/{player.TotalMaxHP}");
            ViewManager.PrintText(14, 6, $"ATK : {player.TotalAttack}");
            ViewManager.PrintText(0, 7, $"MP : {player.MP}/{player.TotalMaxMP}");
            ViewManager.PrintText(14, 7, $"DEF : {player.TotalDefence}");
            ViewManager.PrintText(0, 8, "");
            ViewManager.DrawLine();
        }


        //플레이어의 능력치를 출력할 메서드
        public static void PrintPlayerStatus(Player player)
        {
            ViewManager.PrintText(0, 3, $"{player.Name} (마왕)");
            ViewManager.PrintText($"Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            ViewManager.PrintText("");
            ViewManager.PrintText($"HP : {player.HP}/{player.TotalMaxHP}");
            ViewManager.PrintText(25, 6, $"공격력 : {player.TotalAttack}");
            ViewManager.PrintText(50, 6, $"회피율 : {player.TotalEvasion}");
            ViewManager.PrintText(75, 6, $"행운 : {player.TotalLuck}");
            ViewManager.PrintText(100, 6, $"속도 : {player.Speed}");
            ViewManager.PrintText(0, 7, $"MP : {player.MP}/{player.TotalMaxMP}");
            ViewManager.PrintText(25, 7, $"방어력 : {player.TotalDefence}");
            ViewManager.PrintText(50, 7, $"치명타 : {player.TotalCritical}");
            ViewManager.PrintText(75, 7, $"지능 : {player.TotalIntelligence}");
            ViewManager.PrintText(100, 7, $"골드 : {player.Gold}");
            ViewManager.PrintText(0, 8, "");
            ViewManager.DrawLine();
        }


        //내려가기 창에 들어가면 출력할 메소드
        public static void PrintEnterDungeonText(Player player, int floor)
        {
            Console.Clear();
            PrintTitleTxt("내려가기", floor);
            PrintPlayerBattleStatus(player);
            ViewManager.PrintText("");
            ViewManager.PrintText("??층으로 내려갑니다...");
            ViewManager.PrintText("");
            for (int i = 0; i < 14; i++)
            {
                ViewManager.PrintText("▼");
                Thread.Sleep(150);
            }
            ViewManager.PrintText("");
            Thread.Sleep(300);
            ViewManager.PrintText("플레이어가 모험가 무리와 마주쳤습니다!");
            Thread.Sleep(300);
            ViewManager.PrintText(0, 29, "-> 다음으로");
        }


        //플레이어 턴일 때 출력할 메소드
        public static void PrintPlayerTurnText(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            ViewManager.PrintText("");
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, "-> 다음으로");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
            ViewManager.PrintText("");
            ViewManager.PrintText("플레이어의 차례입니다!");
            ViewManager.PrintText(0, 25, "   공격");
            ViewManager.PrintText("   스킬");
            ViewManager.PrintText("   상황을 지켜보기");
            ViewManager.PrintText("");
            ViewManager.PrintText("   도망가기");
        }


        //배틀 순서를 텍스트로 변환하여 반환하는 메서드
        public static string BattleOrderTxt(List<Character> battleOrderList)
        {
            StringBuilder battleOrderListText = new StringBuilder();
            foreach (Character character in battleOrderList)
            {
                Monster monster = character as Monster;
                if ((monster != null && monster.IsDie == false) ||
                    character is Player)
                {
                    battleOrderListText.Append($"{character.Name} > ");
                }
            }
            battleOrderListText.Append("플레이어 > ... ");
            return battleOrderListText.ToString();
        }


        //몬스터의 정보의 출력할 메소드. isNum이 true면 번호를 추가해서 출력.
        public static void MonsterListInfoTxt(List<Monster> monsterList)
        {
            ViewManager.PrintText(monsterPostionValueX, monsterPostionValueY, "");

            for (int i = 0; i < monsterList.Count; i++)
            {
                ViewManager.PrintText(MonsterListInfoNameString(monsterList[i]));
            }


            ViewManager.PrintText(monsterPostionValueX + 27, monsterPostionValueY, "");
            for (int i = 0; i < monsterList.Count; i++)
            {
                ViewManager.PrintText(MonsterListInfoValueString(monsterList[i]));
            }
        }


        //몬스터의 정보를 string으로 반환하는 메서드
        public static string MonsterListInfoNameString(Monster monster)
        {
            string str = "";
            //번호/레벨/이름/HP(Dead)
            str = ($"Lv.{monster.Level} {monster.Name}");
            return str;
        }


        public static string MonsterListInfoValueString(Monster monster)
        {
            string str = "";

            //죽은 몬스터가 있다면 사망 처리
            if (monster.IsDie)
                str = (str + "Dead");
            else
                str = (str + "HP:" + $"{monster.HP}/{monster.MaxHP}".PadRight(9) +
                    "MP:" + $"{monster.MP}/{monster.MaxMP}".PadRight(9) +
                    "ATK:" + $"{monster.Attack}".PadRight(4) +
                    "DEF:" + $"{monster.Defence}".PadRight(4));
            return str;
        }


        //몬스터의 공격 턴일 때 출력할 메소드
        public static void MonsterAttackTxt(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor, Monster monster)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, "-> 다음으로");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
            ViewManager.PrintText("");
            ViewManager.PrintText($"{monster.Name}의 차례입니다!");
        }


        //스킬을 선택하는 창에서 출력할 메소드
        public static void SelectedSkillTxt(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, "[C] 취소");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
            ViewManager.PrintText("");
            ViewManager.PrintText("");
        }


        //플레이어가 몬스터를 선택할 때 출력할 메소드
        public static void PlayerSelectedMonsterTxt(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, "[C] 취소");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
            ViewManager.PrintText("");
            ViewManager.PrintText("대상을 선택해 주세요.");
        }


        //플레리어가 버프 스킬을 사용할 때 사용 여부를 묻는 메소드
        public static void PlayerUseBuffSkillTxt(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 27, "-> 사용하기");
            ViewManager.PrintText(0, 29, "[C] 취소");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
        }


        //플레이어가 몬스터를 때릴 때 출력할 텍스트
        public static void PlayerAttackMonsterTxt(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 27, "-> 사용하기");
            ViewManager.PrintText(0, 29, "[C] 취소");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
        }


        //플레이어가 버프를 사용할 때 출력할 텍스트
        public static void PlayerUseBuff(Player player, List<Monster> monsters, List<Character> battleOrderList, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, "-> 다음");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
        }


        //플레이어가 전투에서 승리했을 때 출력할 텍스트
        public static void PlayerWinText(Player player, List<Monster> monsters, int floor)
        {
            int restFloor = 0;
            if(floor > 15)
            {
                restFloor = 20;
            }
            else if (floor > 10)
            {
                restFloor = 15;
            }
            else if (floor > 5)
            {
                restFloor = 10;
            }
            else if (floor > 0)
            {
                restFloor = 5;
            }


            Console.Clear();
            PrintTitleTxt("전투 결과", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 28, $"   이어서 내려가기({floor - 1}층)");
            ViewManager.PrintText($"   거점으로 돌아가기({restFloor}층)");//***변수 추가
            ViewManager.PrintText(0, 9, $"{player.Name}이(가) 모험가 {monsters.Count}명을 성공적으로 쫓아냈습니다!");
            ViewManager.PrintText("");
        }


        //플레이어가 패배했을 때 출력할 텍스트
        public static void PlayerDeafText(Player player, List<Monster> monsters, int floor)
        {
            Console.Clear();
            PrintTitleTxt("전투 결과", floor);
            PrintPlayerBattleStatus(player);
            MonsterListInfoTxt(monsters);
            ViewManager.PrintText(0, 29, $"   돌아가기");
            ViewManager.PrintText(0, 9, $"정복 실패...");
            ViewManager.PrintText("");
        }


        //플레이어가 상태 창에 들어갔을 때 출력할 텍스트
        public static void PlayerStatusTxt(Player player, ref int selectedIndex)
        {
            Console.Clear();
            PrintTitleTxt("상태보기");
            PrintPlayerStatus(player);
            ViewManager.PrintText("");
            ViewManager.PrintText("    보유스킬");
            ViewManager.PrintText("");
            ViewManager.PrintText(0, 29, "[C]나가기");

            //스킬 출력
            List<(string, Action, Action?)> skillList = player.SkillList
                                                                .Select(x => ($"{x.Name}             \n   : {x.Info}\n", (Action)null, (Action)null))
                                                                .ToList();

            ScrollViewTxt(skillList, ref selectedIndex, (0, 12), false);
        }

        //메인 메뉴 창에서 택스트 출력하는 메소드
        public static void MainMenuTxt()
        {
            Console.Clear();

            ViewManager.PrintText(100, 24, "   상태 보기");
            ViewManager.PrintText("   전투 시작");
            ViewManager.PrintText("   인벤토리");
        }


        public static void ScrollViewTxt(List<(string, Action, Action?)> menuList, ref int selectedIndex, (int, int) cursor, bool isEnter)
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
                    Console.WriteLine($"↑ ({startIndex} more)");
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
                ConsoleKeyInfo keyInfo;

                //엔터키를 입력 받을지 무시할지에 대한 체크
                if (isEnter) keyInfo = Util.CheckKeyInput(selectedIndex, menuList.Count - 1);
                else keyInfo = Util.CheckKeyInputExceptionEnter(selectedIndex, menuList.Count - 1);

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
                            //AudioManager.PlayMoveMenuSE(0);
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

                            //AudioManager.PlayMoveMenuSE(0);
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
                        isBreak = true;
                        selectedIndex = 0;
                        return;
                }
                if (isBreak) break;
            }
        }


        public static bool ScrollViewTxt(List<(string, Action, Action)> menuList, ref int selectedIndex, (int, int) cursor, bool isEnter, ref int itemIndex)
        {
            itemIndex = 0;
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
                    Console.WriteLine($"↑ ({startIndex} more)");
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
                ConsoleKeyInfo keyInfo;

                //엔터키를 입력 받을지 무시할지에 대한 체크
                if (isEnter) keyInfo = Util.CheckKeyInput(selectedIndex, menuList.Count - 1);
                else keyInfo = Util.CheckKeyInputExceptionEnter(selectedIndex, menuList.Count - 1);

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
                            //AudioManager.PlayMoveMenuSE(0);
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

                            //AudioManager.PlayMoveMenuSE(0);
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (menuList[selectedIndex].Item2 != null)
                        {
                            itemIndex = selectedIndex;
                            menuList[itemIndex].Item2();
                        }
                        return false;

                    case ConsoleKey.C:
                        selectedIndex = 0;
                        return true;
                }
            }
            return false;
        }
        public static void ScrollViewTxt(List<(string, Action, Action?)> menuList, ref int selectedIndex, (int, int) cursor, bool isEnter, out bool isBreak)
        {
            int maxVisibleOption = 5;
            int startIndex = Math.Min(menuList.Count - maxVisibleOption, Math.Max(0, selectedIndex - 2)); // 선택지가 중간에 오도록 5라서 2임
            int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시

            isBreak = false;
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
                    Console.WriteLine($"↑ ({startIndex} more)");
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
                ConsoleKeyInfo keyInfo;

                //엔터키를 입력 받을지 무시할지에 대한 체크
                if (isEnter) keyInfo = Util.CheckKeyInput(selectedIndex, menuList.Count - 1);
                else keyInfo = Util.CheckKeyInputExceptionEnter(selectedIndex, menuList.Count - 1);

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
                            //AudioManager.PlayMoveMenuSE(0);
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

                            //AudioManager.PlayMoveMenuSE(0);
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
                        isBreak = true;
                        selectedIndex = 0;
                        return;
                }
                if (isBreak) break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public static class ViewManager3
    {
        //제목과 층을 출력할 타이틀 텍스트
        public static void PrintTitleTxt(string title, int floor)
        {
            ViewManager.PrintText(0, 1, title);
            ViewManager.PrintText(90, 1, $"현재 위치: 스파르타 던전 {floor}층");
            Console.WriteLine("");
            ViewManager.DrawLine();
        }

        //플레이어 정보를 출력할 메서드
        public static void PrintPlayerState(Player player)
        {
            ViewManager.PrintText(0, 3, $"{player.Name} (마왕)");
            ViewManager.PrintText($"Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            ViewManager.PrintText("");
            ViewManager.PrintText($"HP : {player.HP}/{player.MaxHP}");
            ViewManager.PrintText($"MP : {player.MP}/{player.MaxMP}");
            ViewManager.PrintText("");
            ViewManager.DrawLine();
        }

       
        //내려가기 창에 들어가면 출력할 메소드
        public static void PrintEnterDungeonText(Player player)
        {
            Console.Clear();
            PrintTitleTxt("내려가기", 11);
            PrintPlayerState(player);
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
            ViewManager.PrintText(0, 29,"-> 다음으로");
        }


        //플레이어 턴일 때 출력할 메소드
        public static void PrintPlayerTurnText(Player player, List<Monster> monsters, List<Character> battleOrderList)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", 12);
            ViewManager.PrintText("");
            PrintPlayerState(player);
            MonsterListInfoTxt(false, monsters);
            ViewManager.PrintText(0, 29, "-> 다음으로");
            ViewManager.PrintText(0, 9,BattleOrderTxt(battleOrderList));
            ViewManager.PrintText("");
            ViewManager.PrintText("플레이어의 차례입니다!");
            Util.CheckKeyInputEnter();
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
        public static void MonsterListInfoTxt(bool isNum, List<Monster> monsterList)
        {
            ViewManager.PrintText(40, 2, "");
            for (int i = 0; i < monsterList.Count; i++)
            {
                //번호/레벨/이름/HP(Dead)
                string str = ($"{(isNum ? (i + 1) : "")}Lv.{monsterList[i].Level} {monsterList[i].Name}");

                //죽은 몬스터가 있다면 사망 처리
                if (monsterList[i].IsDie)
                    ViewManager.PrintText(str + "Dead");
                else
                    ViewManager.PrintText(str + $" HP: {monsterList[i].HP} " +
                        $"ATK: {monsterList[i].Attack} " +
                        $"DEF: {monsterList[i].Defence}");
            }
        }


        //몬스터의 공격 턴일 때 출력할 메소드
        public static void MonsterAttackTxt(Player player, List<Monster> monsters, List<Character> battleOrderList)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", 12);
            PrintPlayerState(player);
            MonsterListInfoTxt(false, monsters);
            ViewManager.PrintText(0, 29, "-> 다음으로");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
        }


        //스킬을 선택하는 창에서 출력할 메소드
        public static void SelectedSkillTxt(Player player, List<Monster> monsters, List<Character> battleOrderList)
        {
            Console.Clear();
            PrintTitleTxt("전투 발생", 12);
            PrintPlayerState(player);
            MonsterListInfoTxt(false, monsters);
            ViewManager.PrintText(0, 29, "[C] 취소");
            ViewManager.PrintText(0, 9, BattleOrderTxt(battleOrderList));
        }


        //메인 메뉴 창에서 택스트 출력하는 메소드
        public static void MainMenuTxt()
        {
            Console.Clear();
            ViewManager4.PrintTitle();
            ViewManager.PrintText(100, 24, "   상태 보기");
            ViewManager.PrintText("   전투 시작");
            ViewManager.PrintText("   인벤토리");
        }
    }
}

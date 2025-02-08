using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    class GameManager
    {
        public static GameManager Instance { get; } = new GameManager();

        Player player = new Player();
        BattleManager BattleManagerInstance { get; set; }

        int selectedIndex = 0;

        public GameManager()
        {
            BattleManagerInstance = new BattleManager(player);
        }

        public void PlayerStatusMenu()
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Util.PrintPlayerView(player);
        }

        public void EnterPlayerStatusMenu()
        {
            AudioManager.PlayMoveMenuSE(0);
            PlayerStatusMenu();
        }

        public void EnterBattleMenu()
        {
            AudioManager.PlayBattleBGM();
            AudioManager.PlayMoveMenuSE(0);
            BattleManagerInstance.StartBattle();
        }

        public void GameMenu() // 시작화면 구현
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영입니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine("");

            List<(String, Action)> menuItems = new List<(string, Action)>
            {
                ("1. 상태 보기", PlayerStatusMenu),
                ("2. 전투 시작", EnterBattleMenu)
            };



            for(int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedIndex) Console.WriteLine($"-> {menuItems[i].Item1}");
                else                    Console.WriteLine($"   {menuItems[i].Item1}");
            }


            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if(selectedIndex > 0)
                    {
                        selectedIndex--;
                    }
                    GameMenu();
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < menuItems.Count - 1)
                    {
                        selectedIndex++;
                    }
                    GameMenu();
                    break;
                case ConsoleKey.Enter:
                    menuItems[selectedIndex].Item2();
                    break;
                default:
                    GameMenu();
                    break;
            }
        }
    }
}

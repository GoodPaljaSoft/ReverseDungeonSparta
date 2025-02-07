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

        public GameManager()
        {

        }
        public void GameMenu() // 시작화면 구현
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영입니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine($"\n1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("");
    
            int result = Util.GetUserInput(1, 2);

            switch (result)
            {
                case 1: // 상태보기화면 구현
                    Console.Clear();
                    Console.WriteLine("상태보기");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                    Util.PrintPlayerView(player);
                    int outMenu = Util.GetUserInput(0, 0);
                    if(outMenu == 0)
                    {
                        GameMenu();
                    }
                    break;
                case 2: //전투시작화면 
                    BattleManager battleManager = new BattleManager(player);
                    battleManager.StartBattle();

                    GameMenu();
                    break;
            }
        }
    }
}

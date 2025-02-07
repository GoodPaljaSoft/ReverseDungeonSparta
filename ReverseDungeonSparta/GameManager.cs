using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    internal class GameManager
    {
        public static GameManager Instance { get; } = new GameManager();
        public GameManager() 
        {
        
        }
        public void GameMenu()
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
                case 1:
                    //Util.PrintPlayerView();
                    Console.WriteLine("상태보기");
                    break;
                case 2:
                    Console.WriteLine("전투시작");
                    break;
                
            }
        }


    }
}

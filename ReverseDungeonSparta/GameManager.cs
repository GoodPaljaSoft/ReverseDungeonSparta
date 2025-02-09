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

        public int selectedIndex = 0;

        public GameManager()
        {
            BattleManagerInstance = new BattleManager(player);
            Console.CursorVisible = false;          //깜빡이는 커서를 비활성화
        }

        public void PlayerStatusMenu()
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Util.PrintPlayerView(player);
        }
        public void InventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("갖고 있는 아이템의 정보가 표시됩니다.");
            Console.WriteLine("");
            Console.WriteLine("");

            List<(String, Action)> inventoryItems = new List<(string, Action)>
            {
                ("아이템 조합", EquipItem.ItemUpgrade),
                ("나가기", GameMenu)
            };
            Util.GetUserInput(inventoryItems, InventoryMenu, ref selectedIndex);
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
            //고정으로 출력할 텍스트를 위쪽에 미리 그려둡니다.
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영입니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine("");


            //선택지로 출력할 텍스트와 진입할 메소드를 menuItems의 요소로 집어 넣어줍니다.
            List<(String, Action)> menuItems = new List<(string, Action)>
            {
                ("1. 상태 보기", PlayerStatusMenu),
                ("2. 전투 시작", EnterBattleMenu),
                ("3. 인벤토리", InventoryMenu)
                //("3. 아이템 메뉴", [아이테 메뉴에 진입하는 메소드 이름])
                //...
            };


            //Util.GetUserInput은
            //1. 만들어준 List<(String, Action)> 목록
            //2. 해당 유틸을 실행하는 본인 메서드
            //3. 클래스 필드에서 선언한 int 변수를 ref형태로 넣습니다.
            Util.GetUserInput(menuItems, GameMenu, ref selectedIndex);
        }
    }
}

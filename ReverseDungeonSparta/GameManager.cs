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

        List<(String, Action, Action)> menuItems;

        Player player = new Player();
        public Player Player => player;
        BattleManager BattleManagerInstance { get; set; }

        public int selectedIndex = 0;

        public GameManager()
        {
            BattleManagerInstance = new BattleManager(player);
            Console.CursorVisible = false;          //깜빡이는 커서를 비활성화
            Console.SetWindowSize(ViewManager.width, ViewManager.height);         //콘솔창 크기 지정
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
            //  Console.WriteLine(player.equipItemList[0].ItemInfo.itemName);  //넣은 리스트를 아이템 출력할 때
            player.LoadEquipItems();
            menuItems = new List<(string, Action, Action)>
            {
                ("장비 아이템", EquipItemMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("소비 아이템", UsableItem.UseItemView, () => AudioManager.PlayMoveMenuSE(0)),
                ("나가기", GameMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, InventoryMenu, ref selectedIndex);
        }
        public void EquipItemMenu()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인  - 장비");
            Console.WriteLine("갖고 있는 아이템의 정보가 표시됩니다.");
            Console.WriteLine("");
            Console.WriteLine("");
            //  Console.WriteLine(player.equipItemList[0].ItemInfo.itemName);  //넣은 리스트를 아이템 출력할 때

            menuItems = new List<(string, Action, Action)>
            {
                ("장비 장착", player.LoadEquipItems, () => AudioManager.PlayMoveMenuSE(0)),
                ("장비 합성", UsableItem.UseItemView, () => AudioManager.PlayMoveMenuSE(0)),
                ("나가기", InventoryMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, EquipItemMenu, ref selectedIndex);
        }
        public void ItemUpgrade()
        {

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
            //매개변수로 무언가를 집어넣어야하는 메소드일 경우 다음과 같이 사용 () =>  메소드명(매개변수들)
            //리스트 3번째에 입력 받는 오디오는 null로 선언해도 정상작동 됩니다.
            //새로운 (string, Action, Action) 입력하기 전 반점(,) 필수
            menuItems = new List<(string, Action, Action)>
            {
                ("상태 보기", PlayerStatusMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("전투 시작", EnterBattleMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("인벤토리", InventoryMenu, () => AudioManager.PlayMoveMenuSE(0))
                //("아이템 메뉴", [아이테 메뉴에 진입하는 메소드 이름], [출력할 오디오 메소드])
                //("조합", sum, null)
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

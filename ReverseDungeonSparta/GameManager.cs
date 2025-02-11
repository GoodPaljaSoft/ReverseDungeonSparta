﻿using System;
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

        //플레이어의 공략 진척도
        private int dungeonClearLevel = 0;
        public int DungeonClearLevel
        {
            get
            {
                return dungeonClearLevel;
            }
            set
            {
                //플레이어의 공략 진척도에 변화가 생기면
                //StageClearCheck() 메서드를 통해 각 스테이지 클리어 여부를 확인한다.(후진했을 수도 있으므로...)
                dungeonClearLevel = value;
                StageClearCheck();
            }
        }
        public bool[] clearCheck = new bool[19];

        public GameManager()
        {
            BattleManagerInstance = new BattleManager(player);
            Console.CursorVisible = false;          //깜빡이는 커서를 비활성화


            //공략 진척도에 따라 탑 출력 색 변함
            //level 5 == 현재 15층까지 클리어한 상태
            DungeonClearLevel = 5;
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
            ViewManager.DrawLine("인벤토리");

            //Console.WriteLine("인벤토리");
            //Console.WriteLine("갖고 있는 아이템의 정보가 표시됩니다.");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //  Console.WriteLine(player.equipItemList[0].ItemInfo.itemName);  //넣은 리스트를 아이템 출력할 때
            player.LoadEquipItems();


            //아이템 출력 임시 코드
            ViewManager.PrintList(player.equipItemList);

            
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
            BattleManagerInstance.EnterTheBattle();
        }

        public void TitleSMenu()
        {
            menuItems = new List<(string, Action, Action)>
            {
                ("", GameMenu, null),
                ("", GameMenu, null),
                ("", InventoryMenu, null)

            };

            Util.GetUserInput(menuItems, GameMenu, ref selectedIndex, (100, 23));
        }

        public void GameMenu() // 시작화면 구현
        {
            //Console.SetCursorPosition(100, 25);
 
            ViewManager3.MainMenuTxt();

            //선택지로 출력할 텍스트와 진입할 메소드를 menuItems의 요소로 집어 넣어줍니다.
            //매개변수로 무언가를 집어넣어야하는 메소드일 경우 다음과 같이 사용 () =>  메소드명(매개변수들)
            //리스트 3번째에 입력 받는 오디오는 null로 선언해도 정상작동 됩니다.
            //새로운 (string, Action, Action) 입력하기 전 반점(,) 필수
            menuItems = new List<(string, Action, Action)>
            {
                ("", PlayerStatusMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", EnterBattleMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", InventoryMenu, () => AudioManager.PlayMoveMenuSE(0))
                //("아이템 메뉴", [아이테 메뉴에 진입하는 메소드 이름], [출력할 오디오 메소드])
                //("조합", sum, null)
                //...
            };


            //Util.GetUserInput은
            //1. 만들어준 List<(String, Action)> 목록
            //2. 해당 유틸을 실행하는 본인 메서드
            //3. 클래스 필드에서 선언한 int 변수를 ref형태로 넣습니다.
            Util.GetUserInput(menuItems, GameMenu, ref selectedIndex, (100, 23));
        }

        
        public void StageClearCheck()
        {
            for(int i=0; i < dungeonClearLevel; i++)
            {
                clearCheck[i] = true; 
            }
        }

        public void tempCountdawn()
        {
            dungeonClearLevel++;
            Thread.Sleep(3000);
        }

    }
}
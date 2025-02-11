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

        List<(string, Action, Action)> menuItems;

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

                if (value < 20)
                {
                    dungeonClearLevel = value;
                    StageClearCheck();
                }
            }
        }
        public bool[] clearCheck = new bool[19];

        public GameManager()
        {
            BattleManagerInstance = new BattleManager(player, 20);
            Console.CursorVisible = false;          //깜빡이는 커서를 비활성화
            Console.SetWindowSize(ViewManager.width, ViewManager.height);         //콘솔창 크기 지정

            //인트로 데이터베이스 초기화
            DataBase.IntroTextInit();

            //애니메이션 텍스트 메서드 테스트
            //IntroScene();

            //아이템 장착 테스트
            //player.equipItemList[0].IsEquiped = true;

        }
        public void PlayerStatusMenu()
        {
            ViewManager3.PlayerStatusTxt(player, ref selectedIndex);
            GameMenu();
        }
        #region 소지품 확인 
        public void InventoryMenu()
        {
            Console.Clear();
            ViewManager.DrawLine("인벤토리");

            //아이템 출력 임시 코드
            ViewManager.PrintList(player.equipItemList);

            menuItems = new List<(string, Action, Action)>
            {
                ("장비 아이템", EquipmentMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("소비 아이템", UsableItem.UseItemView, () => AudioManager.PlayMoveMenuSE(0)),
                ("나가기", GameMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, InventoryMenu, ref selectedIndex);
        }
        #endregion 
        #region 소지품 확인 - 장비
        public void EquipmentMenu()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인  - 장비");
            Console.WriteLine("");
            Console.WriteLine("");
            ViewManager.PrintList(player.equipItemList);
            //  Console.WriteLine(player.equipItemList[0].ItemInfo.itemName);  //넣은 리스트를 아이템 출력할 때

            menuItems = new List<(string, Action, Action)>
            {
                ("장비 장착", EquipItemMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("장비 합성", ItemUpgradeMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("나가기", InventoryMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, EquipmentMenu, ref selectedIndex);
        }
        #endregion 
        #region 소지품 확인 - 장비 장착
        public void EquipItemMenu()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인  - 장비 장착");
            // player.IsEquipItem(EquipItem item);  내일 다시 구현해야함. ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ
            menuItems = new List<(string, Action, Action)>
            {
                ("나가기", EquipmentMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, EquipItemMenu, ref selectedIndex);
        }
        #endregion
        #region 소지품 확인 - 장비 합성 씬
        public void ItemUpgradeMenu()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인  - 장비합성");

            //EquipItem.ItemUpgrade();
            menuItems = new List<(string, Action, Action)>
            {
                ("나가기", EquipItemMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(menuItems, ItemUpgradeMenu, ref selectedIndex);
        }
        #endregion
        public void EnterBattleMenu()
        {
            BattleManagerInstance = new BattleManager(player, 20);
            AudioManager.PlayBattleBGM();
            AudioManager.PlayMoveMenuSE(0);
            BattleManagerInstance.EnterTheBattle();
        }

        public void TitleSMenu()
        {
            ViewManager.TitleMenuTxt();
            ViewManager.PrintTitle();

            menuItems = new List<(string, Action, Action)>
            {
                ("", GameMenu, null),
                ("", GameMenu, null),
                ("", GameMenu, null)
            };

            Util.GetUserInput(menuItems, TitleSMenu, ref selectedIndex, (100, 23));
        }

        public void GameMenu() // 시작화면 구현
        {
            

            //고정으로 출력할 텍스트를 위쪽에 미리 그려둡니다.
            ViewManager.MainMenuTxt();
            ViewManager.PrintCurrentFloors(20-DungeonClearLevel);
            ViewManager.DrawLine(2);

            //선택지로 출력할 텍스트와 진입할 메소드를 menuItems의 요소로 집어 넣어줍니다.
            //매개변수로 무언가를 집어넣어야하는 메소드일 경우 다음과 같이 사용 () =>  메소드명(매개변수들)
            //리스트 3번째에 입력 받는 오디오는 null로 선언해도 정상작동 됩니다.
            //새로운 (string, Action, Action) 입력하기 전 반점(,) 필수
            menuItems = new List<(string, Action, Action)>
            {
                //상태 확인, 소지품 확인, 내려가기, 휴식하기, 저장하기, 게임종료
                ("", PlayerStatusMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", InventoryMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", EnterBattleMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", GameMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", GameMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", GameMenu, () => AudioManager.PlayMoveMenuSE(0))
                //("아이템 메뉴", [아이테 메뉴에 진입하는 메소드 이름], [출력할 오디오 메소드])
                //("조합", sum, null)
                //...
            };
            //Util.GetUserInput은
            //1. 만들어준 List<(String, Action)> 목록
            //2. 해당 유틸을 실행하는 본인 메서드
            //3. 클래스 필드에서 선언한 int 변수를 ref형태로 넣습니다.
            Util.GetUserInput(menuItems, GameMenu, ref selectedIndex, (3, 23));

            //스테이지 레벨 반영 시험 코드
            DungeonClearLevel++;


        }

        public void IntroScene()
        {
            ViewManager.PrintLongTextAnimation(DataBase.introText);                
        }

        public void StageClearCheck()
        {
            //초기화
            for (int i = 0; i < clearCheck.Length; i++)
            {
                clearCheck[i] = false;
            }

            //클리어한 스테이지까지 true로 변경
            for (int i = 0; i < dungeonClearLevel; i++)
            {
                clearCheck[i] = true;
            }
        }

   

    }

}

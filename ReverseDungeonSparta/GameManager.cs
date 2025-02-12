using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ReverseDungeonSparta
{
    class GameManager
    {
        public static GameManager Instance { get; } = new GameManager();

        List<(string, Action, Action?)> menuItems;

        Player player = new Player();
        public Player Player => player;
        public BattleManager BattleManagerInstance { get; set; }

        private EquipItem main= new();
        private EquipItem offering = new();

        public int selectedIndex = 0;

        //플레이어의 공략 진척도
        private int dungeonClearLevel = 0;      //던전에 입장 시도를 할 경우 1 증가하여 반환
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
            BattleManagerInstance = new BattleManager(player, DungeonClearLevel);
            Console.CursorVisible = false;          //깜빡이는 커서를 비활성화
            Console.SetWindowSize(ViewManager.width, ViewManager.height);         //콘솔창 크기 지정

            //인트로 데이터베이스 초기화
            DataBase.IntroTextInit();
            ViewManager.ViewInit();

            //애니메이션 텍스트 메서드 테스트
            IntroScene();

        }


        public void PlayerStatusMenu()
        {
            ViewManager3.PlayerStatusTxt(player, ref selectedIndex);
            GameMenu();

        }


        #region 소지품 확인 
        public void InventoryMenu()
        {
            // Console.Clear();
            //아이템 출력 임시 코드
            // ViewManager.PrintList(player.equipItemList);

            InventoryViewManager.EnterInventoryMenuTxt();

            //아이템 출력 임시 코드
            //ViewManager.PrintList(player.equipItemList);

            menuItems = new List<(string, Action, Action?)>
            {
                ("", EquipItemMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", ItemUpgradeMenu, () => AudioManager.PlayMoveMenuSE(0)),
                ("", UseItemMenu, () => AudioManager.PlayMoveMenuSE(0))
            };

            ViewManager3.ScrollViewTxt(menuItems, ref selectedIndex, (0, 26), true);

            GameMenu();
        }
        #endregion 


        #region 소지품 확인 - 장비 장착
        //아이템을 장착할 때 실행할 메서드
        public void EquipItemMenu()
        {
            InventoryViewManager.InventoryEquippedItemTxt();

            while (true)
            {
                if (ViewEquippedEquippedList() == true) break;
            }

            InventoryMenu();
        }


        //아이템의 리스트를 보여 줄 때 실행할 메서드
        public bool ViewEquippedEquippedList()
        {
            int itemIndex = 0;
            player.SortEquippedItemList();
            //1번째 액션에 플레이어가 아이템을 player.equipItemList Action 구현하면 됨
            List<(string, Action, Action?)> itemScrollView = player.equipItemList
                                            .Select(x => (InventoryViewManager.SortEquippedItemList(x) + "\n", (Action)(() => player.EquipEquipItem(ref itemIndex)), (Action)null))
                                            .ToList();

            bool isExit = ViewManager3.ScrollViewTxt(itemScrollView, ref selectedIndex, (0, 5), true, ref itemIndex);

            return isExit;
        }
      
        #endregion


        #region 소지품 확인 - 장비 합성 씬

            public void ItemUpgradeMenu()
        {
            Console.Clear();
            ViewManager.DrawLine("소지품 확인 - 장비 합성", "합성할 장비 2가지를 선택해 주세요");
            Console.WriteLine("[아이템 조합]");
            Console.WriteLine("조합을 원하시는 아이템을 입력해주세요.");
            ViewManager.PrintText(0, 29, "[C]나가기");

            while (true)
            {
                if (ViewEquippedSelectedList() == true) break;
            }
            UpgradeDeSelect();
        }
        public bool ViewEquippedSelectedList()
        {
            bool isBreak = false;
            int itemIndex = 0;
            player.SortEquippedItemList();
            //1번째 액션에 플레이어가 아이템을 player.equipItemList Action 구현하면 됨
            List<(string, Action, Action?)> itemScrollView = player.equipItemList
                                            .Select(x => (InventoryViewManager.InventoryUpgradeSortList(x) + "\n", (Action)(() => UpgradeSelect(x)), (Action)null))
                                            .ToList();

            ViewManager3.ScrollViewTxt(itemScrollView, ref selectedIndex, (0, 5), true, out isBreak);

            return isBreak;
        }
        public void UpgradeDeSelect()
        {
            main.IsSelected = false;
            offering.IsSelected = false;
            main = new EquipItem();
            offering = new EquipItem();
        }
        private void UpgradeSelect(EquipItem equipItem)
        {
            if (main.Name == "")
            {
                main = equipItem;
                main.IsSelected = true;
                Console.WriteLine($"첫번째 아이템{main.Name}이 선택되었습니다.");
            }
            else
            {
                offering = equipItem;
                offering.IsSelected = true;
                Player.ItemUpgrade(main, offering, player.equipItemList);
                UpgradeDeSelect();
                ItemUpgradeMenu();
            }
        }
        #endregion

        // 소비 아이템 선택 메뉴
        public void UseItemMenu()
        {
            Player player = GameManager.Instance.Player; // Player 객체 가져오기
            InventoryViewManager.InventoryUseItemTxt(player);


            while (true)
            {
                if (ViewUsableItemList()) break;
            }

            InventoryMenu();
        }

        //소비 아이템의 리스트를 보여 줄 때 실행할 메서드
        public bool ViewUsableItemList()
        {
            player.SortUseItemList();//플레이어 사용 아이템 정렬
            int itemIndex = 0;
            //1번째 액션에 플레이어가 아이템을 player.equipItemList Action 구현하면 됨
            List<(string, Action, Action)> usableItemList = player.UsableItemInventory
                                            .Select(x => (InventoryViewManager.SortUseItemList(x) + "\n", (Action)(() => UseSelectedItem(ref itemIndex)), (Action)null))
                                            .ToList();

            bool isExit = ViewManager3.ScrollViewTxt(usableItemList, ref selectedIndex, (0, 10), true, ref itemIndex);

            return isExit;
        }

        // 소비 창에서 플레이어 스탯 보기
        public static void UsableStatusView(Player player)
        {
            ViewManager3.PrintPlayerStatus(player);
        }

        private static void ShowRecoveryMessage()
        {
            Console.WriteLine("엔터 키를 눌러 계속 진행하세요...");
            Console.ReadLine();
            Console.Clear();
        }


        // 선택한 소비 아이템 사용
        public static void UseSelectedItem(ref int itemIndex)
        {
            Player player = GameManager.Instance.Player;
            UsableItem selectedItem = player.UsableItemInventory[itemIndex];

            if (selectedItem.Count > 0)
            {
                (bool itemUsed, string recoveryMessage) = ApplyItemEffect(selectedItem);  // 아이템 효과 적용

                if (itemUsed)
                {
                    selectedItem.Count--; // 사용하면 보유 수 차감
                    Console.WriteLine("");
                    Console.WriteLine($"[{selectedItem.Name}] 아이템을 사용했습니다!");
                    if (!string.IsNullOrEmpty(recoveryMessage))
                    {
                        Console.WriteLine(recoveryMessage);
                    }
                    // 만약 사용한 아이템의 개수가 0이 되면 리스트에서 제거
                    if (selectedItem.Count == 0)
                    {
                        player.UsableItemInventory.RemoveAt(itemIndex);
                    }
                }
                else
                {
                    Console.WriteLine("");
                    // HP 또는 MP가 이미 최대치라면 사용되지 않음
                    if (!string.IsNullOrEmpty(recoveryMessage))
                    {
                        Console.WriteLine(recoveryMessage);
                    }
                }
            }
            else
            {
                // 아이템이 없다면
                Console.WriteLine("이 아이템을 사용할 수 없습니다.");
            }
        }


        // 아이템 효과 적용
        public static (bool itemUsed, string recoveryMessage) ApplyItemEffect(UsableItem item)
        {
            Player player = GameManager.Instance.Player;
            bool itemUsed = false; // 아이템 사용 여부 확인
            string recoveryMessage = ""; // 회복 메시지

            // HP 회복 아이템일 때
            if (item.Hp > 0)
            {
                if (player.HP < player.MaxHP) // 플레이어의 HP가 최대가 아닐 때
                {
                    int newHP = Math.Min(player.HP + item.Hp, player.MaxHP);
                    recoveryMessage += $"\n플레이어의 HP를 {newHP - player.HP} 회복합니다.\n현재 플레이어의 HP: {player.HP} -> {newHP}\n";
                    player.HP = newHP;
                    itemUsed = true; // 아이템 사용 표시
                }
                else
                {
                    recoveryMessage += "HP가 이미 최대입니다.\n"; // 이미 HP가 최대일 때
                }
            }

            // MP 회복 아이템일 때
            if (item.Mp > 0)
            {
                if (player.MP < player.MaxMP) // 플레이어의 MP가 최대가 아닐 때
                {
                    int newMP = Math.Min(player.MP + item.Mp, player.MaxMP);
                    recoveryMessage += $"\n플레이어의 MP를 {newMP - player.MP} 회복합니다.\n현재 플레이어의 MP: {player.MP} -> {newMP}\n";
                    player.MP = newMP;
                    itemUsed = true; // 아이템 사용 표시
                }
                else
                {
                    recoveryMessage += "MP가 이미 최대입니다.\n"; // 이미 최대 MP일 때
                }
            }

            return (itemUsed, recoveryMessage); // 아이템 사용 여부 반환
        }


        //플레이어가 메인 메뉴에서 던전에 입장할 때 실행할 메서드
        public void EnterBattleMenu()
        {
            DungeonClearLevel++;
            BattleManagerInstance = new BattleManager(player, DungeonClearLevel);
            AudioManager.PlayBattleBGM();
            AudioManager.PlayMoveMenuSE(0);
            BattleManagerInstance.EnterTheBattle();
        }


        public void TitleSMenu()
        {
            ViewManager.TitleMenuTxt();
            ViewManager.PrintTitle();

            menuItems = new List<(string, Action, Action?)>
            {
                ("", GameMenu, null),
                ("", GameMenu, null),
                ("", GameMenu, null)
            };

            Util.GetUserInput(menuItems, TitleSMenu, ref selectedIndex, (100, 23));
        }


        //메인 메뉴에 입장할 때 실행할 메서드
        public void GameMenu() // 시작화면 구현
        {


            //고정으로 출력할 텍스트를 위쪽에 미리 그려둡니다.
            ViewManager.MainMenuTxt();
            ViewManager.PrintCurrentFloors(20 - DungeonClearLevel);
            ViewManager.DrawLine(2);

            //선택지로 출력할 텍스트와 진입할 메소드를 menuItems의 요소로 집어 넣어줍니다.
            //매개변수로 무언가를 집어넣어야하는 메소드일 경우 다음과 같이 사용 () =>  메소드명(매개변수들)
            //리스트 3번째에 입력 받는 오디오는 null로 선언해도 정상작동 됩니다.
            //새로운 (string, Action, Action?) 입력하기 전 반점(,) 필수
            menuItems = new List<(string, Action, Action?)>
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

            player.Name = Console.ReadLine();
            DataBase.playerName = player.Name;

            ViewManager.PrintLongTextAnimation(DataBase.introText2);

            Console.ReadKey();
        }


        public void StageClearCheck()
        {
            //초기화
            for (int i = 0; i < clearCheck.Length; i++)
            {
                clearCheck[i] = false;
            }

            //클리어한 스테이지까지 true로 변경
            for (int i = 0; i < DungeonClearLevel; i++)
            {
                clearCheck[i] = true;
            }
        }
    }
}

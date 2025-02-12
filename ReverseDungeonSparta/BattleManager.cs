using ReverseDungeonSparta;
using System.Text;
using System.Threading;
using System.Transactions;


public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();
    List<Character> battleOrderList;        //플레이어 턴이 돌아올 때까지의 순서를 저장할 리스트
    List<(string, Action, Action?)> menuItems;
    Skill playerSelectSkill;
    Player player;
    Random random = new Random();
    TurnManager turnManager;
    bool isDungeonEnd = false;      //플레이어가 죽거나 모든 몬스터를 잡았는지 확인
    int oldPlayerHP;                //던전 입장 전 플레이어의 HP를 저장할 변수
    int selectedIndex = 0;          //화살표의 위치를 저장할 int변수
    int[] selectedMonsterIndex;
    int listCount = 0;              //battleOrderList의 Lengh를 업데이트 할 때 사용
    public int dungeonLevel = 0;
    public int dungeonMaxFloor = 20;    //던전의 최대 층 수


    //배틀 매니저 생성자
    public BattleManager(Player player, int floor)
    {
        //***
        //추후 층 수를 기반으로 난이도 조절
        dungeonLevel = floor;

        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int frontRand = random.Next(1, 3);       //1~2사이의 수 만큼 전열 랜덤 값 출력
        int backRand = random.Next(1, 3);       //1~2사이의 수 만큼 후열 랜덤 값 출력
        monsterList = Monster.GetMonsterList(frontRand, backRand, dungeonLevel);   //값으로 나온 만큼 몬스터 생성
        this.player = player;
        oldPlayerHP = player.HP;

        List<Character> allCharacterList = new List<Character>();

        allCharacterList.AddRange(monsterList);
        allCharacterList.Add(player);

        turnManager = new TurnManager(allCharacterList);

        foreach (Monster monster in monsterList)
        {
            monster.SkillList = Skill.AddMonsterSkill(monster, (dungeonMaxFloor - dungeonLevel)/3);
        }
    }



    //도망갈 경우 실행할 메서드
    public void PlayerEscapeBattle()
    {
        if (dungeonLevel <= 5)
        {
            dungeonLevel = 0;
        }
        else if (dungeonLevel <= 10)
        {
            dungeonLevel = 5;
        }
        else if (dungeonLevel <= 15)
        {
            dungeonLevel = 10;
        }
        else if (dungeonLevel <= 20)
        {
            dungeonLevel = 15;
        }
        GameManager.Instance.DungeonClearLevel = dungeonLevel;
    }


    //순서 리스트에서 해당 캐릭터를 빼고, 뺀 수만큼 listCount를 줄여주는 메서드
    public void RemoveOrderListCharacter(Character character)
    {
        battleOrderList.Remove(character);
    }


    //배틀매니저에서 배틀이 시작 될 때 실행할 메서드
    public void EnterTheBattle()
    {
        Console.Clear();
        ViewManager3.PrintEnterDungeonText(player, dungeonMaxFloor - dungeonLevel);
        Util.CheckKeyInputEnter();
        StartBattle();
    }


    //입력된 턴 순서로 캐릭터들에게 턴을 배정하는 메서드
    public void StartBattle()
    {
        while (isDungeonEnd == false)
        {
            turnManager.CalculateTurnPreview(); // 플레이어 나올 때까지 프리뷰를 계산
            battleOrderList = turnManager.SeclectCharacter;

            listCount = battleOrderList.Count;
            for (int i = 0; i < listCount; i++)
            {
                AudioManager.PlayMoveMenuSE(0);
                if (battleOrderList.Count > 0)
                {
                    bool isPlayer = battleOrderList[0] is Player;

                    //버프의 턴을 시작
                    battleOrderList[0].TurnStartBuff();

                    if (isPlayer)
                    {
                        playerSelectSkill = null;//등록된 플레이어 스킬 초기화
                        selectedMonsterIndex = null;
                        StartPlayerBattle();
                    }
                    else
                    {
                        Monster monster = battleOrderList[0] as Monster;
                        StartMonsterBattle(monster); ;
                    }
                    battleOrderList[0].TurnEndBuff();

                    battleOrderList.RemoveAt(0);
                }

                if (isDungeonEnd)
                {
                    break;
                }
            }
        }
    }


    //플레이어의 턴이 시작 되었을 때 시작할 메서드
    public void StartPlayerBattle()
    {
        ViewManager3.PrintPlayerTurnText(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel);

        menuItems = new List<(string, Action, Action?)>
            {
                ("", PlayerSelectMonster, () => AudioManager.PlayMoveMenuSE(0)),
                ("", PlayerSelectSkillNum, () => AudioManager.PlayMoveMenuSE(0)),
                ("", PlayerUseItemSelect, () => AudioManager.PlayMoveMenuSE(0)),
                ("", PlayerEscapeBattle, () => AudioManager.PlayMoveMenuSE(0))
            };
        
        //플레이어가 공격을 선택할 수 있는 입력칸
        Util.GetUserInput(menuItems, StartPlayerBattle, ref selectedIndex, (0, 25));
    }


    //플레이어가 스킬 사용을 누른 후 스킬의 번호를 선택하는 메소드
    public void PlayerSelectSkillNum()
    {
        ViewManager3.SelectedSkillTxt(player, monsterList, battleOrderList, 20 - dungeonLevel);

        //플레이어가 가지고 있는 스킬의 수 만큼 menuItems 작성
        List<(string, Action, Action?)> skillList = player.SkillList
                                .Select(x => ($"{x.Name}                 \n    : {x.Info}\n", (Action)PlayerSelectMonster, (Action)null))
                                .ToList();

        //플레이어가 스킬을 선택할 수 있는 입력칸
        PlayerInputSkillNum(skillList, ref selectedIndex);

        //selectedIndex의 값을 받고 해당 칸의 스킬을 입력
    }


    //플레이어가 공격할 몬스터를 선택하는 메소드 
    public void PlayerSelectMonster()
    {
        List<string> monsterListTxt = new List<string>();

        monsterListTxt = monsterList
            .Select(x => ViewManager3.MonsterListInfoNameString(x))
            .ToList();

        //여기서 먼저 플레이어가 지정한 공격 타입이 어택인지 버퍼인지 확인하는 로직이 필요함***

        //이후 플레이어가 지정한 공격타입이 버퍼일 경우와 어택일 경우를 나눠서 출력을 바꿔야함.

        GetMonsterIndex(monsterListTxt, PlayerSelectMonster, ref selectedMonsterIndex);
    }


    //플레이어가 몬스터를 공격한 이후에 실행할 메서드
    public void PlayerAttackMonster(List<Monster> monsters)
    {
        ViewManager3.PlayerAttackMonsterTxt(player, monsters, battleOrderList, dungeonMaxFloor - dungeonLevel);
        List<int> beforeMonstehpr = monsters.Select(x => x.HP).ToList();

        List<Character> characters = monsters.Select(x => (Character)x).ToList();

        player.Attacking(characters, playerSelectSkill);
    }


    //플레이어가 자신에게 스킬을 사용할 때 사용할 메서드
    public void PlayerUseBuffer()
    {
        int beforeHP = player.HP;
        int beforeAttack = player.TotalAttack;
        int beforeDeffense = player.TotalDefence;
        int beforeCritical = player.Critical;
        int beforeEvasion = player.Evasion;

        //출력
        ViewManager3.PlayerUseBuff(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel);
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        player.AddBuff(player, playerSelectSkill);
        Console.WriteLine($"효과 : {playerSelectSkill.Info}");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine($"플레이어 체  력 : {beforeHP} -> {player.HP}");
        Console.WriteLine("");
        Console.WriteLine($"플레이어 공격력 : {beforeAttack} -> {player.TotalAttack}");
        Console.WriteLine("");
        Console.WriteLine($"플레이어 방어력 : {beforeDeffense} -> {player.TotalDefence}");
        Console.WriteLine("");
        Console.WriteLine($"플레이어 치명타 : {beforeCritical} -> {player.Critical}");
        Console.WriteLine("");
        Console.WriteLine($"플레이어 회  피 : {beforeEvasion} -> {player.Evasion}");
        Console.WriteLine("");
        Console.WriteLine("");

        Util.CheckKeyInputEnter();
    }


    //플레이어가 승리했는지 패배했는지 확인하는 메서드
    public void CheckPlayerWin()
    {
        //몬스터가 전부 죽었는지 확인
        //죽었다면 전투 클리어
        //살아있다면 몬스터의 턴 시작
        bool isWin = true;

        foreach (Monster mon in monsterList)
        {
            if (mon.IsDie == false)
            {
                isWin = false;
            }
        }

        if (isWin)//모든 몬스터가 죽었다면
        {
            //플레이어 승리
            AudioManager.PlayDungeonClearSE(0);
            PlayerWin();
        }
        else
        {
            AudioManager.PlayMoveMenuSE(0);
        }
    }


    //몬스터의 턴이 되었을 때 실행할 메서드
    public void StartMonsterBattle(Monster monster)
    {
        List<(String, Action)> menuItems = new List<(string, Action)>
        {
            ("0. 다음", PlayerDefeat)
        };

        int beforeplayerHP = player.HP;

        ViewManager3.MonsterAttackTxt(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel, monster);

        monster.MonsterAttack(player, monsterList);

        //몬스터가 어떤 공격을 했는지에 따라 효과음 변환하여 출력***

        if (player.HP <= 0)
        {
            AudioManager.PlayPlayerDieSE(0);
            PlayerDefeat();
        }
    }


    //플레이어가 승리했을 때 실행할 메서드
    public void PlayerWin()
    {
        int rewardCount = 1 + (dungeonLevel / 4);

        int rewardEXP = new Random().Next(0, 5) + (dungeonLevel * (2 + (new Random().Next(0, 3))));

        int A = 0;
        int B = 0;

        for( int i = 0; i <  rewardCount; i++ )
        {
            int rand = new Random().Next(0, 2);
            if (rand == 0) A++;
            else B++;
        }

        List<EquipItem> rewardItemList = Player.RandomRewardList(A);
        List<UsableItem> rewardUseItemList = Player.RandomRewardUseList(B);

        menuItems = new List<(string, Action, Action?)>
        {
            ("", GameManager.Instance.EnterBattleMenu, null),
            ("", PlayerGoRestRoom, AudioManager.PlayMenuBGM)
        };

        //승리
        ViewManager3.PlayerWinText(player, monsterList, dungeonMaxFloor - dungeonLevel);


        Console.WriteLine("");
        Console.WriteLine($"[흭득 보상]");
        Console.WriteLine($"");
        Console.WriteLine($"-EXP  ({rewardEXP}) -> [{player.NowEXP}/{player.MaxEXP}]");//***
        Console.WriteLine($"");
        Console.WriteLine($"[흭득 아이템]");

        player.NowEXP += rewardEXP;

        foreach( var item in rewardItemList)
        {
            Console.WriteLine($"{item.Name}");
            player.equipItemList.Add(item);
        }
        foreach (var item in rewardUseItemList)
        {
            Console.WriteLine($"{item.Name}");
            player.UsableItemInventory.Add(item);
        }


        if (player.MaxEXP <= player.NowEXP)ViewManager3.PlayerLevelUpTxt(player);

        isDungeonEnd = true;   //던전 종료
        player.ResetAllBuff(); //버프 초기화
        Util.GetUserInput(menuItems, PlayerWin, ref selectedIndex, (0, 27));
        AudioManager.PlayMoveMenuSE(0);

        //메뉴 출력 추가
    }


    //플레이어가 거점에 돌아갈 때 설정할 메서드
    public void PlayerGoRestRoom()
    {
        GameManager.Instance.DungeonClearLevel = (dungeonMaxFloor - ViewManager3.PlayerRestRoomInt(dungeonMaxFloor - dungeonLevel));
        GameManager.Instance.GameMenu();
    }


    //플레이어가 패배했을 때 실행할 메서드
    public void PlayerDefeat()
    {
        //패배
        ViewManager3.PlayerDeafText(player, monsterList, dungeonMaxFloor - dungeonLevel);

        Util.CheckKeyInputEnter();
        isDungeonEnd = true;
        player.ResetAllBuff();      //버프 초기화
    }


    //원하는 스킬을 선택하고 선택에 성공한다면 해당 스킬을 playerSelectSkill에 저장하는 메서드
    public void PlayerInputSkillNum(List<(string, Action, Action?)> menuList, ref int selectedIndex)
    {
        int maxVisibleOption = 5;
        int startIndex = Math.Min(menuList.Count - maxVisibleOption, Math.Max(0, selectedIndex - 2)); // 선택지가 중간에 오도록 5라서 2임
        int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시


        while (true)
        {
            ViewManager.PrintText(0, 11, "");

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
                Console.WriteLine($"↑ ({startIndex}개)");
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
                Console.WriteLine($"↓ ({menuList.Count - endIndex}개)");
            }

            ConsoleKeyInfo keyInfo = Util.CheckKeyInput(selectedIndex, menuList.Count - 1);

            switch (keyInfo.Key)
            {
                case ConsoleKey.C:
                    AudioManager.PlayMoveMenuSE(0);
                    selectedIndex = 0;
                    StartPlayerBattle();
                    break;

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
                        AudioManager.PlayMoveMenuSE(0);
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

                        AudioManager.PlayMoveMenuSE(0);
                    }
                    break;

                case ConsoleKey.Enter:
                    int tempIndex = selectedIndex;
                    selectedIndex = 0;
                    playerSelectSkill = player.SkillList[tempIndex];
                    if (menuList[tempIndex].Item3 != null) menuList[tempIndex].Item3();
                    PlayerSelectMonster();
                    return;
            }
        }
    }


    //플레이어가 선택한 공격 방식에 따라 몬스터의 이름 옆에 화살표를 출력하고 해당 위치 값을 selectedMonsterIndex에 저장하는 메소드
    public void GetMonsterIndex(List<String> monsterStringList, Action nowMenu, ref int[] selectedMonsterIndex)
    {
        //이 부분에서 플레이어가 사용하는 스킬이 있는지 확인
        //사용하는 스킬이 있다면 해당 스킬의 타입을 확인
        //스킬의 타입이 버퍼라면 다르게 작동하는 로직을 사용

        if (playerSelectSkill != null && playerSelectSkill.ApplyType == ApplyType.Team)
        {
            //플레이어가 지정하는 부분을 상대 몬스터가 아닌 자기 자신이 되도록 함.
            ViewManager3.PlayerUseBuffSkillTxt(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel);
            ViewManager.PrintText(3, 12, $"스킬 : {playerSelectSkill.Name}");
            ViewManager.PrintText($"     : {playerSelectSkill.Info}");

            ConsoleKeyInfo keyInfo = Util.CheckKeyInput();
            switch (keyInfo.Key)
            {
                case ConsoleKey.C:
                    selectedMonsterIndex = null;
                    //AudioManager.PlayMoveMenuSE(0);
                    playerSelectSkill = null;
                    StartPlayerBattle();
                    break;

                case ConsoleKey.Enter:      //엔터를 눌렀을 때
                    PlayerUseBuffer();
                    break;
            }
        }
        else
        {
            bool isBreak = false;
            while (isBreak == false)
            {
                ViewManager.PrintText(ViewManager3.monsterPostionValueX - 3 , ViewManager3.monsterPostionValueY, "");
                //지정한 몬스터를 담을 리스트
                List<Monster> selectMonsterList = new List<Monster>();

                //스킬이 지정되어 있는지 확인하고 없다면 평타로 취급 되어 한칸짜리 공격으로 지정.
                ExtentEnum extentEnum;
                if (playerSelectSkill != null) extentEnum = playerSelectSkill.Extent;
                else extentEnum = ExtentEnum.First;

                //selectedMonsterIndex가 비어있을 경우 extentEnum에 해당하는 범위 값을 int[]로 가져옴
                if (selectedMonsterIndex == null) selectedMonsterIndex = Skill.GetExtent(extentEnum);

                //몬스터의 최대 생성 수인 4마리를 기준으로 반복문 시작.int[]값에 해당하는 부분에 화살표 표시를 처리함.
                for (int i = 0; i < 4; i++)
                {
                    string txt = "";
                    string level = "";
                    string name = "";
                    bool isDie = false;
                    if (i < monsterList.Count && monsterList[i] != null)
                    {
                        level = monsterList[i].Level.ToString();
                        name = monsterList[i].Name;
                        isDie = monsterList[i].IsDie;
                    }

                    bool isSelected = selectedMonsterIndex.Contains(i);

                    if (monsterList.Count > i)
                    {
                        txt = (isSelected ? "-> " : "   ") + monsterStringList[i];
                        if (isSelected) selectMonsterList.Add(monsterList[i]);
                    }
                    else
                    {
                        txt = (isSelected ? "-> " : "   ") + "[비어있음]";
                    }
                    ViewManager.PrintText(txt);
                }

                ConsoleKeyInfo keyInfo = Util.CheckKeyInput(selectedMonsterIndex.Last(), 3);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:    //위 화살표를 눌렀을 때
                        AudioManager.PlayMoveMenuSE(0);
                        selectedMonsterIndex = Util.DownExtent(selectedMonsterIndex);
                        break;

                    case ConsoleKey.DownArrow:  //아래 화살표를 눌렀을 때
                        AudioManager.PlayMoveMenuSE(0);
                        selectedMonsterIndex = Util.UpExtent(selectedMonsterIndex);
                        break;

                    case ConsoleKey.C:
                        selectedMonsterIndex = null;
                        AudioManager.PlayMoveMenuSE(0);
                        isBreak = true;
                        playerSelectSkill = null;
                        StartPlayerBattle();
                        break;

                    case ConsoleKey.Enter:      //엔터를 눌렀을 때
                        isBreak = true;
                        PlayerAttackMonster(selectMonsterList);
                        break;
                }
            }

        }
    }


    //플레이어가 소비 아이템 선택으로 들어갔을 때 출력할 택스트
    public void PlayerUseItemSelect()
    {
        player.SortUseItemList();
        ViewManager3.PlayerSelectUseItemTxt(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel);


        //플레이어가 가지고 있는 스킬의 수 만큼 menuItems 작성
        List<(string, Action, Action?)> UsableItemList = player.UsableItemInventory
                                .Select(x => ($"{x.Name}                 \n    : {x.Information}\n", (Action)PlayerSelectMonster, (Action)null))
                                .ToList();

        //플레이어가 스킬을 선택할 수 있는 입력칸
        PlayerInputUseItemNum(UsableItemList, ref selectedIndex);
    }


    //플레이어가 소비 아이템을 열었을 때 출력할 커서들
    public void PlayerInputUseItemNum(List<(string, Action, Action?)> menuList, ref int selectedIndex)
    {
        int maxVisibleOption = 5;
        int startIndex = Math.Min(menuList.Count - maxVisibleOption, Math.Max(0, selectedIndex - 2)); // 선택지가 중간에 오도록 5라서 2임
        int endIndex = Math.Min(startIndex + maxVisibleOption, menuList.Count); // 5개까지만 표시


        while (true)
        {
            ViewManager.PrintText(0, 11, "");

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
                Console.WriteLine($"↑ ({startIndex}개)");
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
                Console.WriteLine($"↓ ({menuList.Count - endIndex}개)");
            }

            ConsoleKeyInfo keyInfo = Util.CheckKeyInput(selectedIndex, menuList.Count - 1);

            switch (keyInfo.Key)
            {
                case ConsoleKey.C:
                    AudioManager.PlayMoveMenuSE(0);
                    selectedIndex = 0;
                    StartPlayerBattle();
                    return;

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
                        AudioManager.PlayMoveMenuSE(0);
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
                        AudioManager.PlayMoveMenuSE(0);
                    }
                    break;

                case ConsoleKey.Enter:
                    (int, int) playerStatus = (player.HP, player.MP);
                    

                    int tempIndex = selectedIndex;
                    selectedIndex = 0;
                    if (menuList[tempIndex].Item3 != null) menuList[tempIndex].Item3();
                    player.SortUseItemList();
                    UsableItem item = player.UsableItemInventory[tempIndex];
                    GameManager.Instance.UseSelectedItem(ref tempIndex);
                    ViewManager3.PlayerUseUseItemTxt(player, monsterList, battleOrderList, dungeonMaxFloor - dungeonLevel, item, playerStatus);
                    return;
            }
        }
    }
}


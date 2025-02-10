using ReverseDungeonSparta;
using System.Text;
using System.Threading;


public class BattleManager
{
    //구현해야 할 것들.
    //3. 회복 스킬 로직 완성.
    //4. 몬스터 생성 로직 난이도 별로 다르게 조정할 필요 있음.
    //5. 텍스트의 출력 위치를 커서 위치를 이용해 고정하여 바뀌는 텍스트만 초기화 되도록 변경.
    //

    List<Monster> monsterList = new List<Monster>();
    List<Character> battleOrderList;        //플레이어 턴이 돌아올 때까지의 순서를 저장할 리스트
    List<(String, Action, Action)> menuItems;
    Skill playerSelectSkill;
    Player player;
    Random random = new Random();
    TurnManager turnManager;
    bool isDungeonEnd = false;      //플레이어가 죽거나 모든 몬스터를 잡았는지 확인
    int oldPlayerHP;                //던전 입장 전 플레이어의 HP를 저장할 변수
    int selectedIndex = 0;          //화살표의 위치를 저장할 int변수
    int[] selectedMonsterIndex;
    int listCount = 0;              //battleOrderList의 Lengh를 업데이트 할 때 사용

    //배틀 매니저 생성자
    public BattleManager(Player player)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int frontRand = random.Next(0, 1);       //1~2사이의 수 만큼 전열 랜덤 값 출력
        int backRand = random.Next(1, 4);       //1~2사이의 수 만큼 후열 랜덤 값 출력
        monsterList = Monster.GetMonsterList(frontRand, backRand);   //값으로 나온 만큼 몬스터 생성
        this.player = player;
        oldPlayerHP = player.HP;

        List<Character> allCharacterList = new List<Character>();

        allCharacterList.AddRange(monsterList);
        allCharacterList.Add(player);

        turnManager = new TurnManager(allCharacterList);
    }


    //배틀 순서를 텍스트로 변환하여 반환하는 메서드
    public string BattleOrderTxt()
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


    //순서 리스트에서 해당 캐릭터를 빼고, 뺀 수만큼 listCount를 줄여주는 메서드
    public void RemoveOrderListCharacter(Character character)
    {
        battleOrderList.Remove(character);
    }


    //몬스터의 정보의 출력할 메소드. isNum이 true면 번호를 추가해서 출력.
    public void MonsterListInfoTxt(bool isNum)
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            //번호/레벨/이름/HP(Dead)
            Console.Write($"{(isNum ? (i + 1) : "")}Lv.{monsterList[i].Level} {monsterList[i].Name} HP ");

            //죽은 몬스터가 있다면 사망 처리
            if (monsterList[i].IsDie)
                Console.WriteLine("Dead");
            else
                Console.WriteLine(monsterList[i].HP);
        }
    }


    //배틀매니저에서 배틀이 시작 될 때 실행할 메서드
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
            }
        }
    }


    //플레이어의 턴이 시작 되었을 때 시작할 메서드
    public void StartPlayerBattle()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        MonsterListInfoTxt(false);
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");
        Console.WriteLine("");


        menuItems = new List<(string, Action, Action)>
            {
                ("공격하기", PlayerSelectMonster, () => AudioManager.PlayMoveMenuSE(0)),
                ("스킬 사용하기", PlayerSelectSkillNum, () => AudioManager.PlayMoveMenuSE(0))
            };

        //플레이어가 공격을 선택할 수 있는 입력칸
        Util.GetUserInput(menuItems, StartPlayerBattle, ref selectedIndex);
    }


    //플레이어가 스킬 사용을 누른 후 스킬의 번호를 선택하는 메소드
    public void PlayerSelectSkillNum()
    {
        menuItems = new List<(string, Action, Action)>();

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        MonsterListInfoTxt(false);
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");
        Console.WriteLine("");
        Console.WriteLine("[C] 돌아가기");

        //플레이어가 가지고 있는 스킬의 수 만큼 menuItems 작성
        List<(string, Action)> skillList = player.SkillList
                                .Select(x => (x.Name, (Action)PlayerSelectMonster))
                                .ToList();

        //플레이어가 스킬을 선택할 수 있는 입력칸
        GetUserSkillInput(skillList, PlayerSelectSkillNum, ref selectedIndex);
    }


    //플레이어가 공격할 몬스터를 선택하는 메소드 
    public void PlayerSelectMonster()
    {
        List<string> monsterListTxt = new List<string>();

        monsterListTxt = monsterList
            .Select(x => ($"Lv.{x.Level} {x.Name} {(x.IsDie ? "Dead" : (" HP " + x.HP))}"))
            .ToList();


        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine("[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");//플레이어 최대 체력 추가 필요***
        Console.WriteLine("");
        Console.WriteLine("[C] 돌아가기");

        //여기서 먼저 플레이어가 지정한 공격 타입이 어택인지 버퍼인지 확인하는 로직이 필요함***

        //이후 플레이어가 지정한 공격타입이 버퍼일 경우와 어택일 경우를 나눠서 출력을 바꿔야함.

        GetMonsterIndex(monsterListTxt, PlayerSelectMonster, ref selectedMonsterIndex);
    }


    //플레이어가 몬스터를 공격한 이후에 실행할 메서드
    public void PlayerAttackMonster(List<Monster> monsters)
    {
        List<int> beforeMonstehpr = monsters.Select(x => x.HP).ToList();

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 공격!");
        foreach (Monster monster in monsters)
        {
            int beforeMonsterHP = monster.HP;
            player.Attacking(monster,monsterList, out int damage);
            RemoveOrderListCharacter(monster);
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
        }
        Console.WriteLine("");
        for (int i = 0; i < beforeMonstehpr.Count; i++)
        {
            Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name}");
            Console.WriteLine($"HP {(beforeMonstehpr[i] == 0 ? "Dead" : beforeMonstehpr[i])} -> {(monsters[i].IsDie ? "Dead" : (monsters[i].HP))}");
            Console.WriteLine("");
        }
        Console.WriteLine("");
        Console.WriteLine("-> 다음");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.Enter: //플레이어가 승리했는지 확인
                CheckPlayerWin(); break;
        }
    }


    //플레이어가 자신에게 스킬을 사용할 때 사용할 메서드
    public void PlayerUseBuffer()
    {
        int beforeAttack = player.Attack;
        //버프 추가
        player.AddBuff(player, playerSelectSkill);

        //출력
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 스킬 사용!");
        Console.WriteLine($"플레이어 공격력 : {beforeAttack} -> {player.Attack}");
        Console.WriteLine($"{playerSelectSkill.Name}");
        Console.WriteLine($"{playerSelectSkill.Info}");
        Console.WriteLine("");
        Console.WriteLine("-> 다음");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.Enter: //플레이어가 승리했는지 확인
                CheckPlayerWin(); break;
        }
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

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine($"Lv. {monster.Level} {monster.Name}의 공격!");
        monster.Attacking(player,monsterList, out int damage);
        Console.WriteLine($"{player.Name}을(를) 맞췄습니다.  [데미지 : {damage}]");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {beforeplayerHP} -> {player.HP}");
        Console.WriteLine("");
        Console.WriteLine("-> 다음");

        //몬스터가 어떤 공격을 했는지에 따라 효과음 변환하여 출력***

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
        }

        if (player.HP <= 0)
        {
            AudioManager.PlayPlayerDieSE(0);
            PlayerDefeat();
        }
    }


    //플레이어가 승리했을 때 실행할 메서드
    public void PlayerWin()
    {
        //승리
        Console.Clear();
        Console.WriteLine("Battle!! - Result");
        Console.WriteLine();
        Console.WriteLine("Victory");
        Console.WriteLine($"던전에서 몬스터 {monsterList.Count}마리를 잡았습니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {oldPlayerHP} -> {player.HP}"); //던전 입장시 체력을 만들어서 출력해 주었습니다
        Console.WriteLine();
        Console.WriteLine("-> 0. 다음");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.Enter:
                isDungeonEnd = true;
                //승리 처리
                player.ResetAllBuff(); //버프 초기화
                AudioManager.PlayMenuBGM();
                GameManager gameManager = new GameManager();
                AudioManager.PlayMoveMenuSE(0);
                gameManager.GameMenu();
                break;
        }
    }


    //플레이어가 패배했을 때 실행할 메서드
    public void PlayerDefeat()
    {
        //패배
        Console.Clear();
        Console.WriteLine("Battle!! - Result");
        Console.WriteLine();
        Console.WriteLine("You Lose");
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {oldPlayerHP} -> {player.HP}");
        Console.WriteLine();
        Console.WriteLine("-> 다음");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.Enter:
                isDungeonEnd = true;
                player.ResetAllBuff();      //버프 초기화
                //패배 처리
                break;
        }
    }


    //원하는 스킬을 선택하고 선택에 성공한다면 해당 스킬을 playerSelectSkill에 저장하는 메서드
    public void GetUserSkillInput(List<(String, Action)> menuList, Action nowMenu, ref int selectedIndex)
    {
        while (selectedIndex < player.SkillList.Count && player.SkillList[selectedIndex].ConsumptionMP > player.MP)
        {
            selectedIndex++;
        }

        for (int i = 0; i < menuList.Count; i++)
        {
            if (i == selectedIndex) Console.WriteLine($"-> {menuList[i].Item1}");
            else Console.WriteLine($"   {menuList[i].Item1}");
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:    //위 화살표를 눌렀을 때
                if (selectedIndex > 0)
                {
                    AudioManager.PlayMoveMenuSE(0);
                    while (true)
                    {
                        selectedIndex--;
                        if (player.SkillList[selectedIndex].ConsumptionMP <= player.MP) break;
                    }
                }
                nowMenu();
                break;

            case ConsoleKey.DownArrow:  //아래 화살표를 눌렀을 때
                if (selectedIndex < menuList.Count - 1)
                {
                    AudioManager.PlayMoveMenuSE(0);
                    while (true)
                    {
                        selectedIndex++;
                        if (player.SkillList[selectedIndex - 1].ConsumptionMP <= player.MP) break;
                    }
                }
                nowMenu();
                break;

            case ConsoleKey.C:  //아래 화살표를 눌렀을 때
                selectedIndex = 0;
                AudioManager.PlayMoveMenuSE(0);
                StartPlayerBattle();
                //취소 처리
                break;

            case ConsoleKey.Enter:      //엔터를 눌렀을 때
                if (player.SkillList.Count > selectedIndex)
                {
                    AudioManager.PlayMoveMenuSE(0);
                    int tempIndex = selectedIndex;
                    selectedIndex = 0;
                    playerSelectSkill = player.SkillList[tempIndex];
                    menuList[tempIndex].Item2();
                }
                else
                {
                    AudioManager.PlayMoveMenuSE(0);
                    nowMenu();
                }

                break;

            default:                    //상관 없는 키가 눌렸을 때
                nowMenu();
                break;
        }
    }


    //플레이어가 선택한 공격 방식에 따라 몬스터의 이름 옆에 화살표를 출력하고 해당 위치 값을 selectedMonsterIndex에 저장하는 메소드
    public void GetMonsterIndex(List<String> menuItems, Action nowMenu, ref int[] selectedMonsterIndex)
    {
        //이 부분에서 플레이어가 사용하는 스킬이 있는지 확인
        //사용하는 스킬이 있다면 해당 스킬의 타입을 확인
        //스킬의 타입이 버퍼라면 다르게 작동하는 로직을 사용

        if (playerSelectSkill != null && playerSelectSkill.ApplyType == ApplyType.Team)
        {
            //플레이어가 지정하는 부분을 상대 몬스터가 아닌 자기 자신이 되도록 함.
            Console.WriteLine("-> [본인에게 사용]");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.C:
                    selectedMonsterIndex = null;
                    AudioManager.PlayMoveMenuSE(0);
                    playerSelectSkill = null;
                    StartPlayerBattle();
                    break;

                case ConsoleKey.Enter:      //엔터를 눌렀을 때
                    PlayerUseBuffer();
                    break;

                default:                    //상관 없는 키가 눌렸을 때
                    nowMenu();
                    break;
            }
        }
        else
        {
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
                    txt = (isSelected ? "-> " : "   ") + menuItems[i];
                    if (isSelected) selectMonsterList.Add(monsterList[i]);
                }
                else
                {
                    txt = (isSelected ? "-> " : "   ") + "[null]";
                }
                Console.WriteLine(txt);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:    //위 화살표를 눌렀을 때
                    AudioManager.PlayMoveMenuSE(0);
                    selectedMonsterIndex = Util.DownExtent(selectedMonsterIndex);
                    nowMenu();
                    break;

                case ConsoleKey.DownArrow:  //아래 화살표를 눌렀을 때
                    AudioManager.PlayMoveMenuSE(0);
                    selectedMonsterIndex = Util.UpExtent(selectedMonsterIndex);
                    nowMenu();
                    break;

                case ConsoleKey.C:
                    selectedMonsterIndex = null;
                    AudioManager.PlayMoveMenuSE(0);
                    playerSelectSkill = null;
                    StartPlayerBattle();
                    break;

                case ConsoleKey.Enter:      //엔터를 눌렀을 때
                                            //***공격 실행
                    PlayerAttackMonster(selectMonsterList);
                    break;

                default:                    //상관 없는 키가 눌렸을 때
                    nowMenu();
                    break;
            }
        }
    }
}


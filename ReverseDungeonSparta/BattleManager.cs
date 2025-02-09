using ReverseDungeonSparta;
using System.Text;
using System.Threading;


public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();
    Player player;
    int oldPlayerHP;   //던전 입장 전 플레이어의 HP를 저장할 변수

    bool isDungeonEnd = false;      //플레이어가 죽거나 모든 몬스터를 잡았는지 확인

    Random random = new Random();
    private TurnManager turnManager;

    int listCount = 0;                      //battleOrderList의 Lengh를 업데이트 할 때 사용

    List<Character> battleOrderList;        //플레이어 턴이 돌아올 때까지의 순서를 저장할 리스트

    //배틀 매니저 생성자
    public BattleManager(Player player)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int frontRand = random.Next(1, 3);       //1~2사이의 수 만큼 전열 랜덤 값 출력
        int backRand = random.Next(1, 3);       //1~2사이의 수 만큼 후열 랜덤 값 출력
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
                if (battleOrderList.Count > 0)
                {
                    bool isPlayer = battleOrderList[0] is Player;

                    if (isPlayer)
                    {
                        StartPlayerBattle();
                    }
                    else
                    {
                        Monster monster = battleOrderList[0] as Monster;

                        StartMonsterBattle(monster);
                    }

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
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬");


        //플레이어가 공격을 선택할 수 있는 입력칸
        int input = Util.GetUserInput(1, 2);

        switch (input)
        {
            case 1:
                AudioManager.PlayMoveMenuSE(0);
                PlayerSelectMonster();
                break;

            case 2:
                AudioManager.PlayMoveMenuSE(0);
                PlayerSelectSkill();
                break;
        }
    }


    //플레이어가 스킬을 사용할 때 들어오는 메소드
    public void PlayerSelectSkill()
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
        for(int i = 0; i < player.SkillList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.SkillList[i].Name}");
        }


        //플레이어가 스킬을 선택할 수 있는 입력칸
        int input = Util.GetUserInput(0, player.SkillList.Count);

        //*** 나중에 스위치문 지우고 플레이어 스킬의 수만큼 자동으로 받아서 실행하는 메서드 제작 필요
        switch (input)
        {
            case 0: //취소
                AudioManager.PlayMoveMenuSE(0);
                break;

            default:
                //1~스킬 카운트의 값을 확인후 해당 스킬을 실행***
                break;
        }
    }


    //플레이어가 공격할 몬스터를 선택하는 메소드 
    public void PlayerSelectMonster()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        MonsterListInfoTxt(true);
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine("[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");//플레이어 최대 체력 추가 필요***
        Console.WriteLine("");
        Console.WriteLine("0. 취소");

        int input = Util.GetUserInput(0, monsterList.Count);

        switch (input)
        {
            case 0:
                AudioManager.PlayMoveMenuSE(0);
                Console.WriteLine("플레이어가 턴을 넘겼습니다.");
                Thread.Sleep(1000);

                break;
            default:
                //죽은 몬스터를 선택했다면
                if (monsterList[input - 1].IsDie)
                {
                    //잘못된 입력임을 알려주고
                    AudioManager.PlayMoveMenuSE(0);
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);

                    //처음으로 돌아감
                    PlayerSelectMonster();
                }
                else //살아있는 몬스터를 선택했다면
                {
                    //플레이어가 선택한 공격 타입에 따라 효과음 소리 변환***
                    PlayerAttackMonster(monsterList[input - 1]);
                }

                break;
        }

    }


    //플레이어가 몬스터를 공격한 이후에 실행할 메서드
    public void PlayerAttackMonster(Monster monster)
    {
        int beforeMonsterHP = monster.HP;
        int playerDamage = player.Attack;
        player.Attacking(monster, out int damage);
        RemoveOrderListCharacter(monster);
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 공격!");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
        Console.WriteLine($"HP {beforeMonsterHP} -> {(monster.IsDie ? "Dead" : (monster.HP))}");
        Console.WriteLine("");
        Console.WriteLine("0. 다음");


        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
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
                break;
        }
    }


    //몬스터의 턴이 되었을 때 실행할 메서드
    public void StartMonsterBattle(Monster monster)
    {
        int beforeplayerHP = player.HP;
        monster.Attacking(player, out int damage);

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine($"Lv. {monster.Level} {monster.Name}의 공격!");
        Console.WriteLine($"{player.Name}을(를) 맞췄습니다.  [데미지 : {damage}]");
        Console.WriteLine("");
        Console.WriteLine($"{BattleOrderTxt()}");
        Console.WriteLine("");
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {beforeplayerHP} -> {player.HP}");
        Console.WriteLine("");
        Console.WriteLine("0. 다음");

        //몬스터가 어떤 공격을 했는지에 따라 효과음 변환하여 출력***

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                if (player.HP == 0)
                {
                    AudioManager.PlayPlayerDieSE(0);
                    PlayerDefeat();
                }
                break;
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
        Console.WriteLine("0. 다음");

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                isDungeonEnd = true;
                //승리 처리
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
        Console.WriteLine("0. 다음");


        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                isDungeonEnd = true;
                //패배 처리
                break;
        }
    }
}


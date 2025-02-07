using ReverseDungeonSparta;
using System.Threading;


public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();
    Player player;                                  
    int oldPlayerHP;   //던전 입장 전 플레이어의 HP를 저장할 변수

    bool isDungeonEnd = false;

    Random random = new Random();
    private TurnManager turnManager;


    //배틀 매니저 생성자
    public BattleManager(Player player)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int frontRand = random.Next(1, 3);       //1~2사이의 수 만큼 전열 랜덤 값 출력
        int backRand = random.Next(1, 3);       //1~2사이의 수 만큼 후열 랜덤 값 출력
        monsterList = Monster.GetMonsterList(frontRand, backRand);   //값으로 나온 만큼 몬스터 생성
        this.player = player;
        oldPlayerHP = player.HP;

        player.Speed = 50;//***임시 플레이어 스피드 지정

        List<Character> allCharacterList = new List<Character>();

        allCharacterList.AddRange(monsterList);
        allCharacterList.Add(player);

        turnManager = new TurnManager(allCharacterList);
    }


    //배틀매니저에서 배틀이 시작 될 때 실행할 메서드
    public void StartBattle()
    {
        while (isDungeonEnd == false)
        {
            turnManager.CalculateTurnPreview(); // 5턴 프리뷰를 계산
            var currentCharacter = turnManager.TurnPreview.First();

            bool isPlayer = currentCharacter is Player;

            if (isPlayer)
            {
                StartPlayerBattle();
            }
            else
            {
                Monster monster = currentCharacter as Monster;

                StartMonsterBattle(monster);
            }
        }
    }


    //플레이어의 턴이 시작 되었을 때 시작할 메서드
    public void StartPlayerBattle() 
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        for (int i = 0; i < monsterList.Count; i++)
        {
            //번호/레벨/이름/HP(Dead)
            Console.Write($"Lv.{monsterList[i].Level} {monsterList[i].Name} HP ");

            //죽은 몬스터가 있다면 사망 처리
            if (monsterList[i].IsDie)
                Console.WriteLine("Dead");
            else
                Console.WriteLine(monsterList[i].HP);
        }
        Console.WriteLine("");
        Console.WriteLine($"[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");
        Console.WriteLine("");
        Console.WriteLine("1. 공격");

        //플레이어가 공격을 선택할 수 있는 입력칸
        int input = Util.GetUserInput(1, 1);

        switch (input)
        {
            case 1:
                PlayerSelectMonster();
                break;
        }
    }


    //플레이어가 공격할 몬스터를 선택하는 메소드 
    public void PlayerSelectMonster()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        for (int i = 0; i < monsterList.Count; i++)
        {

            //번호/레벨/이름/HP(Dead)
            Console.Write($"{i + 1}) Lv.{monsterList[i].Level} {monsterList[i].Name} HP ");

            //죽은 몬스터가 있다면 사망 처리
            //죽은 몬스터의 텍스트는 어두운 색으로 표시***
            if (monsterList[i].IsDie)
                Console.WriteLine("Dead");
            else
                Console.WriteLine(monsterList[i].HP);
        }

        Console.WriteLine();
        Console.WriteLine("[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.HP}/{player.MaxHP}");//플레이어 최대 체력 추가 필요***
        Console.WriteLine();
        Console.WriteLine("0. 취소");

        int input = Util.GetUserInput(0, monsterList.Count);

        switch (input)
        {
            case 0:
                Console.WriteLine("플레이어가 턴을 넘겼습니다.");
                Thread.Sleep(1000);

                break;
            default:
                //죽은 몬스터를 선택했다면
                if (monsterList[input - 1].IsDie)
                {
                    //잘못된 입력임을 알려주고
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);

                    //처음으로 돌아감
                    PlayerSelectMonster();
                }
                else //살아있는 몬스터를 선택했다면
                {
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

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 공격!");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
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
                foreach(Monster mon in monsterList)
                {
                    if(mon.IsDie == false)
                    {
                        isWin = false;
                    }
                }

                if(isWin)//모든 몬스터가 죽었다면
                {
                    //플레이어 승리
                    PlayerWin();
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
        Console.WriteLine();
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {beforeplayerHP} -> {player.HP}");
        Console.WriteLine();
        Console.WriteLine("0. 다음");

        //공격받기 전 체력 저장 (위치가 애매해서 모두와 상의를 해보고 싶음)***

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                if(player.HP == 0)
                {
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


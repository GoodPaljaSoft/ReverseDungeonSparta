using ReverseDungeonSparta;
using System.Threading;


public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();
    Player player;                                  
    int statingHp;  //던전 입장시 체력

    Random random = new Random();

    public BattleManager(Player player)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int frontRand = random.Next(1, 3);       //1~2사이의 수 만큼 전열 랜덤 값 출력
        int backRand = random.Next(1, 3);       //1~2사이의 수 만큼 후열 랜덤 값 출력
        monsterList = Monster.GetMonsterList(frontRand, backRand);   //값으로 나온 만큼 몬스터 생성
        this.player = player;
    }


    //플레이어보다 빠른 몬스터를 찾음.
    //해당 몬스터를 임시 리스트에 저장.
    //해당 리스트에서 속도가 빠른 순서로 어택을 실행.
    //StartBattle로 이동


    public void ComputeFastSpeed()
    {
        List<Monster> speedArray = new List<Monster>();
        int playerSpeed = 10; //***플레이어 스피드 대입
        foreach (Monster monster in monsterList)
        {
            if(monster.Speed > playerSpeed)//***몬스터 속도 대입
            {
                speedArray.Add(monster);
            }
        }

        speedArray.Sort((x, y) => x.Speed.CompareTo(y.Speed));

        foreach(Monster monster in speedArray)
        {
            MonsterTurn(monster);
        }

        StartBattle();
    }



    public void StartBattle() 
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
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
        Console.WriteLine();
        Console.WriteLine($"[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.NowHealth}/{player.MaxHealth}");
        Console.WriteLine();
        Console.WriteLine("1. 공격");

        //플레이어가 공격을 선택할 수 있는 입력칸
        int input = Util.GetUserInput(1, 1);

        switch (input)
        {
            case 1:
                PlayerTurn();
                break;
        }
    }


    //플레이어가 공격할 몬스터를 선택할 수 있는 메소드 
    public void PlayerTurn()
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
        Console.WriteLine($"HP {player.NowHealth}/{player.MaxHealth}");//플레이어 최대 체력 추가 필요***
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
                    PlayerTurn();
                }
                else //살아있는 몬스터를 선택했다면
                {
                    PlayerAttackMonster(monsterList[input - 1]);
                }

                break;
        }

    }


    //플레이어가 턴을 넘기거나 공격한 이후에 실행할 메서드
    public void PlayerAttackMonster(Monster monster)
    {
        int beforeMonsterHP = monster.HP;
        int playerDamage = player.Attack;
        monster.OnDamage(playerDamage);

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 공격!");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]"); //***나중에 공격력 수치 처리 필요
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
                else//몬스터가 하나라도 살아있다면
                {

                    foreach (Monster mon in monsterList)
                    {
                        MonsterTurn(mon);
                    }


                    //몬스터의 턴 시작
                    foreach (Monster mon in monsterList)
                    {
                        if(monster.IsDie == false || player.NowHealth > 0)     //몬스터가 살아있으며 플레이어의 체력이 남아있을 때 몬스터의 턴 시작
                        {
                            MonsterTurn(mon);
                        }
                    }

                    //몬스터의 모든 차례가 끝나고 플레이어의 체력이 남아있으면
                    if(player.NowHealth > 0)
                    {
                        StartBattle();  //몬스터의 턴이 끝나면 처음부터 시작
                    }
                }
                break;
        }
    }

    public void MonsterTurn(Monster monster)
    {
        int playerHP = player.NowHealth - monster.Attack;
        if(playerHP < 0)
        {
            playerHP = 0;
        }

        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine($"Lv. {monster.Level} {monster.Name}의 공격!");
        Console.WriteLine($"{player.Name}을(를) 맞췄습니다.  [데미지 : {monster.Attacking}]");
        Console.WriteLine();
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {player.NowHealth} -> {playerHP}");
        Console.WriteLine();
        Console.WriteLine("0. 다음");

        player.NowHealth = playerHP;

        //공격받기 전 체력 저장 (위치가 애매해서 모두와 상의를 해보고 싶음)***

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                if(player.NowHealth == 0)
                {
                    PlayerDefeat();
                }
                break;
        }
    }

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
        Console.WriteLine($"HP {statingHp} -> {player.NowHealth}"); //던전 입장시 체력을 만들어서 출력해 주었습니다
        Console.WriteLine();
        Console.WriteLine("0. 다음");

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                //승리 처리
                break;
        }
    }

    public void PlayerDefeat()
    {
        //패배
        Console.Clear();
        Console.WriteLine("Battle!! - Result");
        Console.WriteLine();
        Console.WriteLine("You Lose");
        Console.WriteLine($"Lv. {player.Level} {player.Name}");
        Console.WriteLine($"HP {statingHp} -> {player.NowHealth}");
        Console.WriteLine();
        Console.WriteLine("0. 다음");


        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                //패배 처리
                break;
        }
    }
}




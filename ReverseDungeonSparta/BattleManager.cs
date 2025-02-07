using ReverseDungeonSparta;
using System;
using System.Threading;

public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();
    Player player;
    int beforeHp;   //공격 받기 전 체력
    int statingHp;  //던전 입장시 체력

    Random random = new Random();

    public BattleManager(Player player)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int rand = random.Next(1, 5);       //1~4 사이의 수 만큼 랜덤 값 출력
        monsterList = Monster.GetMonsterList(rand);   //값으로 나온 만큼 몬스터 생성
        this.player = player;
        beforeHp = statingHp = player.NowHealth;
    }


    public void StartBattle() 
    {
        //몬스터의 기본 정보 출력***
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        for (int i = 0; i < monsterList.Count; i++)
        {

            //번호/레벨/이름/HP(Dead)
            Console.Write($"{monsterList[i].Level} {monsterList[i].Name} HP ");

            //죽은 몬스터가 있다면 사망 처리
            //죽은 몬스터의 텍스트는 어두운 색으로 표시***
            if (monsterList[i].IsDie)
                Console.WriteLine("Dead");
            else
                Console.WriteLine(monsterList[i].Hp);
        }
        Console.WriteLine();
        Console.WriteLine($"[내 정보]");
        Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.Job.ToString()})");
        Console.WriteLine($"HP {player.NowHealth}/{player.MaxHealth}");//플레이어 최대 체력 추가 필요***
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
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        for (int i = 0; i < monsterList.Count; i++)
        {

            //번호/레벨/이름/HP(Dead)
            Console.Write($"Lv.{monsterList[i].Level} {monsterList[i].Name} HP ");

            //죽은 몬스터가 있다면 사망 처리
            //죽은 몬스터의 텍스트는 어두운 색으로 표시***
            if (monsterList[i].IsDie)
                Console.WriteLine("Dead");
            else
                Console.WriteLine(monsterList[i].Hp);
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
        int afterMonsterHP = monster.Hp - player.Attack;
        if(afterMonsterHP < 0)
        {
            afterMonsterHP = 0;
            monster.Dead();
        }

        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} 의 공격!");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {player.Attack}]"); //***나중에 공격력 수치 처리 필요
        Console.WriteLine("");
        Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
        Console.WriteLine($"HP {monster.Hp} -> {(monster.IsDie ? "Dead" : (afterMonsterHP))}");
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
                    //몬스터의 턴 시작
                    foreach(Monster mon in monsterList)
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
        int playerHP = player.NowHealth - monster.Atk;
        if(playerHP < 0)
        {
            playerHP = 0;
        }

        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine($"Lv. {monster.Level} {monster.Name}의 공격!");
        Console.WriteLine($"{player.Name}을(를) 맞췄습니다.  [데미지 : {monster.Atk}]");
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
        Console.WriteLine("Battle!! - Result");
        Console.WriteLine();
        Console.WriteLine("Victory");
        Console.WriteLine($"던전에서 몬스터 {monsterList.Count + 1}마리를 잡았습니다.");
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




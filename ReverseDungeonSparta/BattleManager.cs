using ReverseDungeonSparta;
using System;

public class BattleManager
{
    List<Monster> monsterList = new List<Monster>();

    Random random = new Random();

    public BattleManager(/*Player player*/)
    {
        monsterList = new List<Monster>();     //몬스터 리스트 초기화
        int rand = random.Next(1, 5);       //1~4 사이의 수 만큼 랜덤 값 출력
                                            //값으로 나온 만큼 몬스터 생성***
    }


    public void StartBattle()
    {
        //몬스터의 기본 정보 출력***
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine("[몬스터 정보 출력");
        Console.WriteLine();
        Console.WriteLine("[내 정보]");
        Console.WriteLine("플레이어 이름");
        Console.WriteLine("플레이어 체력");
        Console.WriteLine();
        Console.WriteLine("1. 공격");

        //플레이어가 공격을 선택할 수 있는 입력칸
        int input = Util.GetUserInput(1, 1);

        switch (input)
        {
            case 1:
                //플레이어의 공격 턴으로 이동***
                PlayerTurn();
                break;
        }
    }

    public void PlayerTurn()
    {
        //몬스터의 기본 정보 출력***
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine("[몬스터 정보 출력] -> 번호 붙여서 출력");
        Console.WriteLine();
        Console.WriteLine("[내 정보]");
        Console.WriteLine("플레이어 이름");
        Console.WriteLine("플레이어 체력");
        Console.WriteLine();
        Console.WriteLine("0. 취소");

        int input = Util.GetUserInput(0, monsterList.Count);

        switch(input)
        {
            case 0:
                //바로 몬스터의 턴으로 이동
                break;

            default:
                //monsterList[input - 1]가 플레이어에게 피해를 입음.***
                //피해를 입은 몬스터가 현재 체력이 0 이하로 떨어졌을 경우
                //isDie를 True로 변경후 해당 몬스터의 체력을 0으로 고정.
                break;
        }

        PlayerEndTurn();
    }


    public void PlayerEndTurn()
    {
        //몬스터의 기본 정보 출력***
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine("[몬스터 정보 출력] -> 번호 붙여서 출력"); //죽은 몬스터가 있다면 사망 처리***
        Console.WriteLine();
        Console.WriteLine("[내 정보]");
        Console.WriteLine("플레이어 이름");
        Console.WriteLine("플레이어 체력");
        Console.WriteLine();
        Console.WriteLine("0. 다음");

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                //살아 있는 몬스터가 없으면
                //플레이어의 승리 처리로 넘어감***

                //살아있는 몬스터가 있다면
                //반복문으로 시작
                foreach(Monster monster in monsterList)
                {
                    MonsterTurn();
                    monster.Attack();
                    //플레이어가 데미지를 입음***

                    //if(Playerdie == true)***
                    break;
                }
                break;
        }


        //-> 전투 시작 창으로 넘어감
        StartBattle();
    }

    public void MonsterTurn()
    {
        //몬스터의 기본 정보 출력***
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.WriteLine("[몬스터 정보 출력] -> 번호 붙여서 출력"); //죽은 몬스터가 있다면 사망 처리***
        Console.WriteLine();
        Console.WriteLine("[내 정보]");
        Console.WriteLine("플레이어 이름");
        Console.WriteLine("플레이어 체력");
        Console.WriteLine();
        Console.WriteLine("0. 다음");

        int input = Util.GetUserInput(0, 0);

        switch (input)
        {
            case 0:
                break;
        }
    }
}




using ReverseDungeonSparta;
using System;

public class Monster
{
    static Random random = new Random();

    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    //public float Def { get; set; }
    public bool IsDie { get; set; }

    public Monster(MonsterInfo monsterInfo)
    {
        Name = monsterInfo.name;
        Level = random.Next(1, 5); 
        Hp = monsterInfo.hp + (2 * Level);
        Atk = monsterInfo.Atk + (2 * Level);
        IsDie = false;          //몬스터 죽음 상태 false 고정
    }

    //몬스터의 공격 처리를 실행할 메서드 
    public void Attack(Player player)
    {
        //플레이어 체력 감소
        player.NowHealth -= Atk;

        if(player.NowHealth < 0)
        {
            player.NowHealth = 0;
            //플레이어 사망 처리 및 전투 종료 메서드로 이동
        }
    }


    //몬스터가 사망 시 실행할 메소드
    public void Dead()
    {
        IsDie = true;

        //나중에 델리게이트를 추가한다면 명령어 추가***
    }


    //몬스터가 데미지를 입는 메소드
    public void OnDamage(int damage)
    {
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
            Dead();
        }
    }


    //몬스터 종류 3가지 정의
    public static MonsterInfo[] allMonster =
    {
        new MonsterInfo("Monster1", 20, 3),
        new MonsterInfo("Monster2", 30, 2),
        new MonsterInfo("Monster3", 10, 5),
    };


    //랜덤으로 반환된 몬스터를 몬스터리스트에 정리해서 반환
    public static List<Monster> GetMonsterList(int num)
    {
        List<Monster> monsterList = new List<Monster>();

        for(int i = 0; i < num; i++)
        {
            monsterList.Add(InstanceMonster());
        }

        return monsterList;
    }


    //랜덤으로 몬스터를 하나씩 반환
    public static Monster InstanceMonster()
    {
        int rand = random.Next(0, allMonster.Length);
        MonsterInfo monsterInfo = allMonster[rand];

        return new Monster(monsterInfo);

    }


    //몬스터의 정보를 저장할 구조체
    public struct MonsterInfo
    {
        public string name;
        public int hp;
        public int Atk;

        public MonsterInfo(string _name, int _hp, int _Atk)
        {
            name= _name;
            hp= _hp;
            Atk= _Atk;
        }
    }
}

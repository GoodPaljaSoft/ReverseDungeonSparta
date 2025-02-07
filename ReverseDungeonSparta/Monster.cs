using ReverseDungeonSparta;
using System;

public class Monster :Character
{
    static Random random = new Random();

    private int hp;
    public override int MaxHP { get; set; }
    public override int HP
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp <= 0)
            {
                IsDead();
            }
            else if (hp > MaxHP)
            {
                hp = MaxHP;
            }
        }
    }
    public override string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsDie { get; set; }


    public Monster(MonsterInfo monsterInfo)
    {
        Name = monsterInfo.name;
        Level = random.Next(1, 5);

        MaxHP = monsterInfo.hp + (2 * Level);
        HP = MaxHP;
        MaxMP = 100;
        MP = MaxMP;

        Attack = monsterInfo.atk + (2 * Level);
        Speed = monsterInfo.speed;
        Luck = 5;
        Defence = 5;
        Intelligence = 5;

        Critical = 5;
        Evasion = 5;

        IsDie = false;          //몬스터 죽음 상태 false 고정
    }

    //전사 3가지 정의
    public static MonsterInfo[] AllWarrior =
    {
        //이름, 체력, 공격력
        new MonsterInfo("균형잡힌 전사", 40, 2, 5),
        new MonsterInfo("다혈질 전사", 30, 4, 5),
        new MonsterInfo("건장한 전사", 45, 1, 5)
    };

    //마법사 3가지 정의
    public static MonsterInfo[] AllMagician =
    {
        new MonsterInfo("균형잡힌 마법사", 20, 4, 5),
        new MonsterInfo("한방이 있는 마법사", 15, 8, 5),
        new MonsterInfo("근성있는 마법사", 40, 2, 5)
    };

    //힐러 3가지 정의
    public static MonsterInfo[] AllHealer =
    {
        new MonsterInfo("소심한 힐러", 30, 3, 5),
        new MonsterInfo("화가 많은 힐러", 25, 4, 5),
        new MonsterInfo("근성있는 힐러", 45, 2, 5)
    };

    //도적 3가지 정의
    public static MonsterInfo[] AllRogue =
    {
        new MonsterInfo("보물상자를 찾는 도적", 45, 2, 5),
        new MonsterInfo("한방을 노리는 도적", 25, 4, 5),
        new MonsterInfo("시체를 뒤지던 도적", 30, 1, 5)
    };


    //궁수 3가지 정의
    public static MonsterInfo[] AllArcher =
    {
        new MonsterInfo("인내심 있는 궁수", 30, 2, 5),
        new MonsterInfo("현상금을 노리는 궁수", 20, 3, 5),
        new MonsterInfo("날렵한 명사수", 10, 5, 5)
    };


    //몬스터가 사망할 때 처리할 메서드
    public void IsDead()
    {
        hp = 0;
        IsDie = true;
        Speed = 0;
    }


    //랜덤으로 반환된 몬스터를 몬스터리스트에 정리해서 반환
    public static List<Monster> GetMonsterList(int frontCount, int backCount)
    {

        List<Monster> monsterList = new List<Monster>();

        for (int i = 0; i < frontCount; i++)
        {
            monsterList.Add(InstanceFrontMonster());
        }

        for (int i = 0; i < backCount; i++)
        {
            monsterList.Add(InstanceBackMonster());
        }

        return monsterList;
    }


    //랜덤으로 전열 몬스터 하나를 반환
    public static Monster InstanceFrontMonster()
    {
        List<MonsterInfo> frontAllMonsterInfo = new List<MonsterInfo>();

        foreach(MonsterInfo monsterinfo in AllWarrior)
        {
            frontAllMonsterInfo.Add(monsterinfo);
        }

        foreach (MonsterInfo monsterinfo in AllRogue)
        {
            frontAllMonsterInfo.Add(monsterinfo);
        }
        int rand = random.Next(0, frontAllMonsterInfo.Count);

        MonsterInfo monsterInfo = frontAllMonsterInfo[rand];

        return new Monster(new MonsterInfo(monsterInfo.name, monsterInfo.hp, monsterInfo.atk, monsterInfo.speed));
    }


    //랜덤으로 후열 몬스터 하나를 반환
    public static Monster InstanceBackMonster()
    {
        List<MonsterInfo> backAllMonsterInfo = new List<MonsterInfo>();

        foreach (MonsterInfo monsterinfo in AllArcher)
        {
            backAllMonsterInfo.Add(monsterinfo);
        }
        foreach (MonsterInfo monsterinfo in AllHealer)
        {
            backAllMonsterInfo.Add(monsterinfo);
        }
        foreach (MonsterInfo monsterinfo in AllMagician)
        {
            backAllMonsterInfo.Add(monsterinfo);
        }

        int rand = random.Next(0, backAllMonsterInfo.Count);

        MonsterInfo monsterInfo = backAllMonsterInfo[rand];

        return new Monster(new MonsterInfo(monsterInfo.name, monsterInfo.hp, monsterInfo.atk, monsterInfo.speed));
    }

    //몬스터의 정보를 저장할 구조체
    public struct MonsterInfo
    {
        public string name;
        public int hp;
        public int atk;
        public int speed;

        public MonsterInfo(string _name, int _hp, int _atk, int _speed)
        {
            name= _name;
            hp= _hp;
            atk= _atk;
            speed = _speed;
        }
    }
}

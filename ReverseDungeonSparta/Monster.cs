using ReverseDungeonSparta;
using System;
using System.Security.Cryptography.X509Certificates;

public class Monster : Character
{
    static Random random = new Random();
    public MonsterType Type { get; set; }
    public int Level { get; set; }
    public bool IsDie { get; set; }


    public Monster(MonsterInfo monsterInfo)
    {
        Name = monsterInfo.name;
        Level = random.Next(1, 5); //*** 레벨 추후에 난이도 조절에 조정 필요
        Type = monsterInfo.type;
        MaxHP = monsterInfo.hp + (2 * Level);
        Attack = monsterInfo.atk + (2 * Level);
        Speed = monsterInfo.speed;
        HP = MaxHP;
        MaxMP = 100;
        MP = MaxMP;
        Luck = 5;
        Defence = 5;
        Intelligence = 5;
        Critical = 5;
        Evasion = 5;

        IsDie = false;          //몬스터 죽음 상태 false 고정
        //SkillList = Skill.AddMonsterSkill(this, 3);//*** 스킬 갯수 추후에 난이도 조절에 조정 필요
    }

    //전사 3가지 정의
    public static MonsterInfo[] AllWarrior =
    {
        //이름, 체력, 공격력
        new MonsterInfo("균형잡힌 전사",MonsterType.Warrior, 40, 2, 5),
        new MonsterInfo("다혈질 전사",MonsterType.Warrior, 30, 4, 5),
        new MonsterInfo("건장한 전사",MonsterType.Warrior, 45, 1, 5)
    };

    //마법사 3가지 정의
    public static MonsterInfo[] AllMagician =
    {
        new MonsterInfo("균형잡힌 마법사",MonsterType.Magician, 20, 4, 5),
        new MonsterInfo("한방이 있는 마법사",MonsterType.Magician, 15, 8, 5),
        new MonsterInfo("근성있는 마법사",MonsterType.Magician, 40, 2, 5)
    };

    //힐러 3가지 정의
    public static MonsterInfo[] AllHealer =
    {
        new MonsterInfo("소심한 힐러",MonsterType.Healer, 30, 3, 5),
        new MonsterInfo("화가 많은 힐러",MonsterType.Healer, 25, 4, 5),
        new MonsterInfo("근성있는 힐러",MonsterType.Healer, 45, 2, 5)
    };

    //도적 3가지 정의
    public static MonsterInfo[] AllRogue =
    {
        new MonsterInfo("보물상자를 찾는 도적",MonsterType.Rouge, 45, 2, 5),
        new MonsterInfo("한방을 노리는 도적",MonsterType.Rouge, 25, 4, 5),
        new MonsterInfo("시체를 뒤지던 도적",MonsterType.Rouge, 30, 1, 5)
    };


    //궁수 3가지 정의
    public static MonsterInfo[] AllArcher =
    {
        new MonsterInfo("인내심 있는 궁수",MonsterType.Acher, 30, 2, 5),
        new MonsterInfo("현상금을 노리는 궁수",MonsterType.Acher, 20, 3, 5),
        new MonsterInfo("날렵한 명사수",MonsterType.Acher, 10, 5, 5)
    };


    public override void Attacking(Character target,List<Monster> monsters ,out int damage)
    {
        Random random = new Random();
        Skill skill = null;
        int rand = random.Next(0, 1);
        double attackDamage = (double)Attack;
        if(rand == 0)
        {
            //무작위로 섞은 스킬리스트 0번 스킬 사용
            if (SkillList.Count > 0)
            {
                SkillList = Util.ShuffleList(SkillList);
                if (SkillList[0].ConsumptionMP <= MP)
                {
                    skill = SkillList[0];
                    attackDamage *= skill.Value;
                    Console.WriteLine($"스킬 발동 : {skill.Name}");
                }
            }
        }

        //데미지 계산식
        double margin = attackDamage * 0.1d;
        margin = Math.Ceiling(margin);

        damage = new Random().Next((int)(attackDamage - margin), (int)(attackDamage + margin));

        if (skill != null)
        {
            if(skill.ApplyType == ApplyType.Enemy)
            {
                //플레이어 공격
                OnDamage(target, damage);
            }
            else if (skill.ApplyType == ApplyType.Team)
            {
                //팀에게 사용
                rand = random.Next(0, monsters.Count);
                monsters[rand].AddBuff((Character)this, skill);
            }
        }
        else
        {
            //플레이어 공격
            OnDamage(target, damage);
        }
    }


    //해당 스킬의 타입을 확인하고 물리, 마법, 힐에 따라 작용 방식을 달리 만드는 메서드
    public void CheckSkillType(Skill skill)
    {
        if(skill.Type == SkillType.Physical)
        {
            //해당 캐릭터의 데미지에서 물리 공격력에 기반하여 타격
            //상대방의 방어력을 기준으로 데미지가 감소
        }
        else if(skill.Type == SkillType.Magic)
        {
            //해당 캐릭터의 데미지에서 지능에 기반하여 타격
            //상대방의 방어력을 무시하고 타격
        }
        else if(skill.Type == SkillType.Buffer)
        {
            //해당 캐릭터의 지능에 영향을 받아 계산함.
            //적용 대상을 상대방에서 아군으로 바꾸는 로직 필요

            //해당 스킬을 사용하는 객체가 몬스터일 경우와 플레이어일 경우
        }
    }

    //몬스터가 사망할 때 처리할 메서드
    public void IsDead()
    {
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

        foreach (MonsterInfo monsterinfo in AllWarrior)
        {
            frontAllMonsterInfo.Add(monsterinfo);
        }

        foreach (MonsterInfo monsterinfo in AllRogue)
        {
            frontAllMonsterInfo.Add(monsterinfo);
        }
        int rand = random.Next(0, frontAllMonsterInfo.Count);

        MonsterInfo monsterInfo = frontAllMonsterInfo[rand];

        return new Monster(monsterInfo);
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

        return new Monster(monsterInfo);
    }

    //몬스터의 정보를 저장할 구조체
    public struct MonsterInfo
    {
        public string name;
        public MonsterType type;
        public int hp;
        public int atk;
        public int speed;

        public MonsterInfo(string _name, MonsterType _type, int _hp, int _atk, int _speed)
        {
            name = _name;
            hp = _hp;
            type = _type;
            atk = _atk;
            speed = _speed;
        }
    }

    public enum MonsterType
    {
        Warrior,
        Rouge,
        Magician,
        Healer,
        Acher
    };
}

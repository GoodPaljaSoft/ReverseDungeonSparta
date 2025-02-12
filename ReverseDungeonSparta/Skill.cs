using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monster;

namespace ReverseDungeonSparta
{
    public class Skill
    {
        public Skill(SkillInfo skillInfo)
        {
            Name = skillInfo.name;
            Value = skillInfo.value;
            BufferType = skillInfo.buffType;
            BufferTurn = skillInfo.bufferTurn;
            Info = skillInfo.info;
            ApplyType = skillInfo.applyType;
            Extent = skillInfo.extent;
            Type = skillInfo.type;
            ConsumptionMP = skillInfo.consumptionMP;
        }

        public string Name { get; set; }                //스킬 이름
        public double Value { get; set; }               //스킬 효과 배수 또는 값
        public int ConsumptionMP { get; set; }          //소모 마나
        public int BufferTurn { get; set; }             //버프의 지속 턴 시간
        public string Info { get; set; }                //스킬 설명
        public BuffType BufferType { get; set; }        //버프의 지속 턴 시간
        public ExtentEnum Extent { get; set; }          //스킬 범위
        public ApplyType ApplyType { get; set; }        //스킬 적용 타입(팀, 적)
        public SkillType Type { get; set; }             //스킬 타입


        //스킬 범위 enum을 받은 후 범위를 int[]로 반환하는 메서드
        public static int[] GetExtent(ExtentEnum extentEnum)
        {
            switch (extentEnum)
            {
                case ExtentEnum.First:
                    return new int[] { 0 };
                case ExtentEnum.FirstAndThird:
                    return new int[] { 0, 2 };
                case ExtentEnum.FirstAndFourth:
                    return new int[] { 0, 3 };
                case ExtentEnum.Second:
                    return new int[] { 0, 1 };
                case ExtentEnum.Third:
                    return new int[] { 0, 1, 2 };
                case ExtentEnum.Fourth:
                    return new int[] { 0, 1, 2, 3 };
                default:
                    return null;
            }
        }


        //들어온 스킬 배열을 기반으로 스킬 리스트로 반환
        public static List<Skill> AddSkillListInstance(SkillInfo[] skillInfoArray)
        {
            List<Skill> backAllMonsterInfo = skillInfoArray
                .Select(x => new Skill(x))
                .ToList();

            return backAllMonsterInfo;
        }


        //몬스터의 스킬을 만들 때 사용할 메서드
        public static List<Skill> AddMonsterSkill(Monster monster, int num)
        {
            List<Skill> skillList = new List<Skill>();

            if (monster.Type == MonsterType.Warrior ||
                monster.Type == MonsterType.Rouge ||
                monster.Type == MonsterType.Acher)
            {
                skillList = AddSkillListInstance(PhysicalSkill);
            }
            else if (monster.Type == MonsterType.Magician)
            {
                skillList = AddSkillListInstance(MagicSkill);
            }
            else if (monster.Type == MonsterType.Healer)
            {
                skillList = AddSkillListInstance(HealingSkill);
            }

            if(num > skillList.Count) { num = skillList.Count; }

            skillList = skillList.Take(num).ToList();

            return skillList;
        }


        //플레이어의 스킬을 만들 때 사용할 메서드
        public static List<Skill> AddPlayerSkill(Player player, int num)
        {
            List<Skill> skillList = AddSkillListInstance(PlayerSkill);

            if (player.SkillList != null)
            {
                //플레이어의 스킬에서 겹치는 이름을 제거 한 리스트를 반환함.
                foreach (Skill skill in player.SkillList)
                {
                    skillList = skillList.Where(x => x.Name != skill.Name).ToList();
                }
            }

            skillList = skillList.Take(num).ToList();

            return skillList;
        }


        //플레이어의 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] PlayerSkill = new SkillInfo[]
        {
        new SkillInfo("강타", 1.5d, ExtentEnum.First, ApplyType.Enemy, SkillType.Physical, 5, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("휘두르기", 1.0d, ExtentEnum.FirstAndThird, ApplyType.Enemy, SkillType.Physical, 8, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("공격력 강화", 1.5d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 5,BuffType.AttackBuff, 1, "스킬 설명"),
        new SkillInfo("방어력 강화", 1.0d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 8, BuffType.DefenceBuff, 2, "스킬 설명"),
        new SkillInfo("회전회오리", 0.7d, ExtentEnum.Fourth, ApplyType.Enemy, SkillType.Physical, 10, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("힐링", 1.5d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 5, BuffType.HealingBuff, 1, "스킬 설명"),
        new SkillInfo("퓨어 힐", 1.0d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 8, BuffType.HealingBuff, 1, "스킬 설명"),
        new SkillInfo("지속 힐", 0.7d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 3, BuffType.HealingBuff, 3, "스킬 설명")
        };


        //물리 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] PhysicalSkill = new SkillInfo[]
        {
        new SkillInfo("강타", 1.5d, ExtentEnum.First, ApplyType.Enemy, SkillType.Physical, 5, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("휘두르기", 1.0d, ExtentEnum.FirstAndThird, ApplyType.Enemy, SkillType.Physical, 8, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("회전회오리", 0.7d, ExtentEnum.Fourth, ApplyType.Enemy, SkillType.Physical, 10, BuffType.None, 0, "스킬 설명")
        };


        //마법 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] MagicSkill = new SkillInfo[]
        {
        new SkillInfo("파이어볼", 1.5d, ExtentEnum.First, ApplyType.Enemy, SkillType.Magic, 5, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("버스트샷", 1.0d, ExtentEnum.FirstAndThird, ApplyType.Enemy, SkillType.Magic, 8, BuffType.None, 0, "스킬 설명"),
        new SkillInfo("지진", 0.7d, ExtentEnum.Third, ApplyType.Enemy, SkillType.Magic, 10, BuffType.None, 0, "스킬 설명")
        };


        //버프 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] BufferSkill = new SkillInfo[]
        {
        new SkillInfo("공격력 강화", 1.5d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 5,BuffType.AttackBuff, 1, "스킬 설명"),
        new SkillInfo("방어력 강화", 1.0d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 8, BuffType.DefenceBuff, 2, "스킬 설명"),
        new SkillInfo("행운 강화", 0.7d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 10, BuffType.LuckBuff, 3, "스킬 설명")
        };


        //힐 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] HealingSkill = new SkillInfo[]
        {
        new SkillInfo("힐링", 1.5d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 5, BuffType.HealingBuff, 1, "스킬 설명"),
        new SkillInfo("퓨어 힐", 1.0d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 8, BuffType.HealingBuff, 1, "스킬 설명"),
        new SkillInfo("지속 힐", 0.7d, ExtentEnum.First, ApplyType.Team, SkillType.Buffer, 3, BuffType.HealingBuff, 3, "스킬 설명")
        };
    }

    //스킬 범위를 enum으로 저장
    public enum ExtentEnum : byte
    {
        First,
        FirstAndThird,
        FirstAndFourth,
        Second,
        Third,
        Fourth
    };


    //스킬 타입을 enum으로 저장
    public enum SkillType : byte
    {
        Physical,
        Magic,
        Buffer
    };

    
    //스킬의 적용 상대를 저장
    public enum ApplyType : byte
    {
        Enemy,
        Team
    }


    //스킬의 정보를 담을 구조체
    public struct SkillInfo
    {
        public string name;
        public double value;
        public int consumptionMP;
        public int bufferTurn;
        public string info;
        public ExtentEnum extent;
        public ApplyType applyType;
        public SkillType type;
        public BuffType buffType;

        public SkillInfo(string _name, double _value, ExtentEnum _extent, ApplyType _applyType,SkillType _type, 
                        int _consumptionMP, BuffType _buffType,int _bufferTurn, string _info)
        {
            this.name = _name;
            this.value = _value;
            this.extent = _extent;
            this.applyType = _applyType;
            this.type = _type;
            this.consumptionMP = _consumptionMP;
            this.buffType = _buffType;
            this.bufferTurn = _bufferTurn;
            this.info = _info;
        }
    }

}

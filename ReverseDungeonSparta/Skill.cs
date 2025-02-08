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
            Extent = skillInfo.extent;
            Type = skillInfo.type;
            ConsumptionMP = skillInfo.consumptionMP;
        }

        public string Name { get; set; }
        public double Value { get; set; }
        public ExtentEnum Extent { get; set; }
        public SkillType Type { get; set; }
        public int ConsumptionMP { get; set; }


        public int[] GetExtent(ExtentEnum extentEnum)
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

        public int[] UpExtent(int[] extentArray)
        {
            int[] result = extentArray;
            int lastNum = extentArray.Last();

            if (lastNum >= 3)
            {
                result = extentArray.Select(x => x + 1).ToArray();
            }

            return result;
        }

        public int[] DownExtent(int[] extentArray)
        {
            int[] result = extentArray;
            int firstNum = extentArray.First();

            if (firstNum <= 0)
            {
                result = extentArray.Select(x => x - 1).ToArray();
            }

            return result;
        }

        //들어온 스킬 배열을 기반으로 스킬 리스트로 반환
        public static List<Skill> AddSkillListInstance(SkillInfo[] skillInfoArray)
        {
            List<Skill> backAllMonsterInfo = new List<Skill>();

            backAllMonsterInfo = skillInfoArray
                .Select(x => new Skill(new SkillInfo(x.name, x.value, x.extent, x.type, x.consumptionMP))).ToList();

            return backAllMonsterInfo;
        }

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

        public static List<Skill> AddPlayerSkill(Player player, int num)
        {
            List<Skill> skillList = AddSkillListInstance(PlayerSkill);

            if (player.SkillList != null)
            {
                foreach (Skill skill in player.SkillList)
                {
                    skillList = skillList.Where(x => x.Name != skill.Name).ToList();
                }
            }

            skillList = skillList.Take(num).ToList();

            return skillList;
        }


        public static SkillInfo[] PlayerSkill = new SkillInfo[]
        {
        new SkillInfo("강타", 1.5d, ExtentEnum.First, SkillType.Physical, 5),
        new SkillInfo("휘두르기", 1.0d, ExtentEnum.FirstAndThird, SkillType.Physical, 8),
        new SkillInfo("회전회오리", 0.7d, ExtentEnum.Fourth, SkillType.Physical, 10)
        };

        public static SkillInfo[] PhysicalSkill = new SkillInfo[]
        {
        new SkillInfo("강타", 1.5d, ExtentEnum.First, SkillType.Physical, 5),
        new SkillInfo("휘두르기", 1.0d, ExtentEnum.FirstAndThird, SkillType.Physical, 8),
        new SkillInfo("회전회오리", 0.7d, ExtentEnum.Fourth, SkillType.Physical, 10)
        };


        public static SkillInfo[] MagicSkill = new SkillInfo[]
        {
        new SkillInfo("파이어볼", 1.5d, ExtentEnum.First, SkillType.Magic, 5),
        new SkillInfo("버스트샷", 1.0d, ExtentEnum.FirstAndThird, SkillType.Magic, 8),
        new SkillInfo("지진", 0.7d, ExtentEnum.Third, SkillType.Magic, 10)
        };


        public static SkillInfo[] HealingSkill = new SkillInfo[]
        {
        new SkillInfo("힐링", 1.5d, ExtentEnum.First, SkillType.Buffer, 5),
        new SkillInfo("퓨어힐", 1.0d, ExtentEnum.First, SkillType.Buffer, 8),
        new SkillInfo("올힐", 0.7d, ExtentEnum.First, SkillType.Buffer, 10)
        };
    }

    public enum ExtentEnum
    {
        First,
        FirstAndThird,
        FirstAndFourth,
        Second,
        Third,
        Fourth
    };

    public enum SkillType
    {
        Physical,
        Magic,
        Buffer
    };

    public struct SkillInfo
    {
        public string name;
        public double value;
        public ExtentEnum extent;
        public SkillType type;
        public int consumptionMP;

        public SkillInfo(string _name, double _value, ExtentEnum _extent, SkillType _type, int _consumptionMP)
        {
            this.name = _name;
            this.value = _value;
            this.extent = _extent;
            this.type = _type;
            this.consumptionMP = _consumptionMP;
        }
    }

}

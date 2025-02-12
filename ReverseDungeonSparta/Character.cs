using System.Numerics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ReverseDungeonSparta
{
    public class Character : Buffer
    {
        private int _hp;
        private int _mp;

        public string Name { get; set; } = string.Empty;//이름
        public int Luck { get; set; }//행운(치명타 확률, 회피율에 연관)
        public int Attack { get; set; }//공격력
        public int Defence { get; set; }//방어력
        public int Critical { get; set; }//치명타확률
        public int Evasion { get; set; }//회피력
        public int Intelligence { get; set; }//지능 마법 스킬에 연관
        public int TotalMaxHP
        {
            get
            {
                int valueMaxHP = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList;
                    foreach (var equipItem in isEquippedList)
                    {
                        valueMaxHP += equipItem.AddMaxHp;
                    }
                }
                return MaxHP + valueMaxHP;
            }
            private set { }
        }
        public int TotalMaxMP
        {
            get
            {
                int valueMaxMp = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList;
                    foreach (var equipItem in isEquippedList)
                    {
                        valueMaxMp += equipItem.AddMaxMp;
                    }
                }
                return MaxMP + valueMaxMp;
            }
            private set { }
        }

        public int TotalLuck //행운(치명타 확률, 회피율에 연관)
        {
            get
            {
                int valueLuk = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList;
                    foreach (var equipItem in isEquippedList)
                    {
                        valueLuk += equipItem.AddLuck;
                    }
                }


                return (int)(valueLuk + Luck);
            }
            private set { }
        }//최종 지능
        public int TotalIntelligence
        {
            get
            {
                int valueInt = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList; //장착리스트
                    foreach (var equipItem in isEquippedList)
                    {
                        valueInt += equipItem.AddIntelligence;
                    }
                }
                double value = Intelligence;
                if (IntelligenceBuff.Count > 0)
                {
                    value += IntelligenceBuff.Select(x => x.Item1).Sum();
                }
                return (int)(value + valueInt);
            }
            private set { }
        }//최종 지능
        public int TotalDefence
        {
            get
            {
                int valueDef = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList;
                    foreach (var equipItem in isEquippedList)
                    {
                        valueDef += equipItem.AddDefence;
                    }
                }
                double value = 1d;
                if (DefenceBuff.Count > 0)
                {
                    value = DefenceBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)((Defence + valueDef) * value);
            }
            private set { }
        }//최종 방어력
        public int TotalAttack
        {
            get
            {
                int valueAtk = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> isEquippedList = player.isEquippedList;
                    foreach (var equipItem in isEquippedList)
                    {
                        valueAtk += equipItem.AddAttack;
                    }
                }
                double value = 1d;
                if (AttackBuff.Count > 0)
                {
                    value = AttackBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                int result = (int)(Attack * value);
                return (int)((Attack + valueAtk) * value);
            }
            private set { }
        }//최종 공격력
        public int TotalCritical
        {
            get
            {
                //기본 값은 Luck 수치, 모든 Luck 관련 버프를 더한 후 나온 Luck / 2가 최종 치명타 확률
                double value = TotalLuck;
                if (LuckBuff.Count > 0) value += LuckBuff.Select(x => x.Item1).Sum();
                if (Critical * (value / 2) > 50) return 50;
                else return (int)(Critical * (value / 2));
            }
            private set { }
        }//최종 치명타 확률
        public int TotalEvasion
        {
            get
            {
                //기본 값은 Luck 수치, 모든 Luck 관련 버프를 더한 후 나온 Luck / 2가 최종 회피 확률
                double value = TotalLuck;
                if (LuckBuff.Count > 0) value += LuckBuff.Select(x => x.Item1).Sum();
                if (Evasion * (value / 2) > 50) return 50;
                else return (int)(Evasion * (value / 2));
            }
            private set { }
        }//최종 회피율

        public int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    _hp = 0;
                    Monster monster = GetMonster();
                    if (monster != null) monster.IsDead();
                }
                else if (_hp > TotalMaxHP) _hp = TotalMaxHP;
            }
        }               //체력
        public int MaxHP { get; set; } //최대 체력

        public int MP
        {
            get
            { return _mp; }
            set
            {
                _mp = value;
                if (_mp <= 0) _mp = 0;
                else if (_mp > TotalMaxMP) _mp = TotalMaxMP;
            }
        }               //마나
        public int MaxMP { get; set; }//최대 마다

        public int Speed { get; set; }  //속도
        public List<Skill> SkillList { get; set; }  //가지고 있는 스킬


        //해당 클래스의 객체가 타겟을 때렸을 때 사용하는 메서드
        public virtual void Attacking(List<Character> targets, Skill skill)
        {
            ViewManager.PrintText(0, 10, "");

            //데미지 계산식
            double margin = TotalAttack * 0.1f;
            margin = Math.Ceiling(margin);
            int damage = 0;


            damage = new Random().Next(TotalAttack - (int)margin, TotalAttack + (int)margin);


            if (skill != null && skill.Type == SkillType.Magic)
            {
                margin = TotalIntelligence * 0.1f;
                damage = new Random().Next(TotalIntelligence - (int)margin, TotalIntelligence + (int)margin);
            }


            SkillType skillType = SkillType.Physical;


            ViewManager.PrintText("");

            if (skill != null)
            {
                skillType = skill.Type;
                ViewManager.PrintText($"{this.Name}의 스킬 사용!");
                MP -= skill.ConsumptionMP;
                foreach (Character onTarget in targets)
                {
                    string pullName = this.Name == onTarget.Name ? "자신" : $"{onTarget.Name}";
                    ViewManager.PrintText($"{this.Name}은(는) {onTarget.Name}에게 {skill.Name}을(를) 사용했다!");
                }
            }
            else
            {
                ViewManager.PrintText($"{this.Name}의 공격!");
                foreach (Character onTarget in targets)
                {
                    ViewManager.PrintText($"{this.Name}은(는) {onTarget.Name}에게 공격을 시도했다!");
                }
            }

            Util.CheckKeyInputEnter();

            List<int> criticalDamageList = new List<int>();
            foreach (Character onTarget in targets)
            {
                onTarget.OnDamage1(this, damage ,out int criticalDamage, skill);
                criticalDamageList.Add(criticalDamage);
            }

            for (int i = 0; i < criticalDamageList.Count; i++)
            {
                targets[i].OnDamage2(this, criticalDamageList[i], skill);

                Monster monster = targets[i] as Monster;

                if (monster != null && monster.IsDie == true)
                    GameManager.Instance.BattleManagerInstance.RemoveOrderListCharacter(monster);
            }
            GameManager.Instance.BattleManagerInstance.CheckPlayerWin();
        }


        // 해당 클래스를 가지고 있는 객체가 데미지를 입는 메소드1
        public void OnDamage1(Character target, int damage, out int criticalDamage, Skill skill)
        {
            criticalDamage = 0;
            SkillType skillType = SkillType.Physical;
            if (skill != null) { skillType = skill.Type; }

            if (skill != null && skill.ApplyType == ApplyType.Team)
            {
                int beforeHP = this.HP;
                int beforeMP = this.MP;
                int beforeATK = this.TotalAttack;
                int beforeDEF = this.TotalDefence;
                int beforeCritical = this.TotalCritical;
                int beforeEvasion = this.Evasion;

                AddBuff(target, skill);
                ViewManager.PrintText($"{this.Name}의 스테이터스 변화");
                ViewManager.PrintText($"");
                ViewManager.PrintText($"체  력: {beforeHP} -> {this.HP}");
                ViewManager.PrintText($"마  나: {beforeMP} -> {this.MP}");
                ViewManager.PrintText($"공격력: {beforeATK} -> {this.TotalAttack}");
                ViewManager.PrintText($"방어력: {beforeDEF} -> {this.TotalDefence}");
                ViewManager.PrintText($"치명타: {beforeCritical}% -> {this.TotalCritical}%");
                ViewManager.PrintText($"회  피: {beforeEvasion}% -> {this.TotalEvasion}%");
                ViewManager.PrintText($"");
            }
            else
            {
                //치명타가 발생한 경우
                if (ComputeManager.TryChance(target.TotalCritical))
                {
                    ViewManager.PrintText($"{this.Name}에게 치명적인 일격!!!");
                    Util.CheckKeyInputEnter();
                    criticalDamage = damage * 2;
                }
            }
        }


        // 해당 클래스를 가지고 있는 객체가 데미지를 입는 메소드2
        public void OnDamage2(Character target, int damage, Skill skill)
        {
            ViewManager.PrintText("");
            int cursorY = Console.CursorTop;
            string empty = new string(' ', 100);
            ViewManager.PrintText(0, cursorY, empty);
            ViewManager.PrintText(empty);
            ViewManager.PrintText(0, cursorY, "");


            SkillType skillType = SkillType.Physical;
            ApplyType applyType = ApplyType.Enemy;
            if (skill != null)
            {
                skillType = skill.Type;
                applyType = skill.ApplyType;
            }

            if (this is Monster && HP == 0)
            {
                ViewManager.PrintText($"{this.Name}에게 아무 일도 일어나지 않았다.");
                ViewManager.PrintText($"{this.Name}은(는) 이미 쓰러져있었다.");
            }
            else if (applyType == ApplyType.Enemy)
            {
                //데미지에서 방어력을 제외한 데미지로 취급,
                if (skillType == SkillType.Physical)
                {
                    damage -= TotalDefence;
                }

                if (damage < 0) damage = 0;

                //TotalEvasion의 수치 만큼의 확률로 회피
                if (ComputeManager.TryChance(TotalEvasion))
                {
                    ViewManager.PrintText("회피 성공!");
                    ViewManager.PrintText($"{Name}은(는) {target.Name}의 공격을 피했습니다!");
                }
                else
                {
                    int beforeHP = HP;
                    HP -= damage;

                    ViewManager.PrintText($"{target.Name}에게 총 {damage} 데미지를 입었습니다! ({beforeHP} -> {(HP == 0 ? "Dead" : HP)})");

                    if (HP == 0)
                    {
                        ViewManager.PrintText($"{this.Name}은(는) 쓰러졌습니다.");
                    }
                }

            }
            Util.CheckKeyInputEnter();
            ViewManager.PrintText(0, cursorY, "");
        }


        //캐릭터클래스를 플레이어로 바꿔주는 메서드
        public Player GetPlayer()
        {
            if (this is Player) return (Player)this;
            return null;
        }


        //캐릭터클래스를 몬스터로 바꿔주는 메서드
        public Monster GetMonster()
        {
            if (this is Monster) return (Monster)this;
            return null;
        }


        //턴 시작 시 선언되어 회복 리스트의 요소가 있는지 확인하고 힐을 실행하는 메서드
        public void CheckHealingList(bool useNow)
        {
            if(HealingBuff.Count > 0)
            {
                foreach (var heal in HealingBuff)
                {
                    int beforeHP = HP;

                    HP += heal.Item1;
                    if(useNow == false)
                    {
                        Console.WriteLine($"{Name}은(는) {heal.Item1}의 체력을 회복했다!");
                        Console.WriteLine($"체력 : {beforeHP}/{TotalMaxHP} -> {HP}/{TotalMaxHP}");
                    }
                }
            }
        }
    }

}

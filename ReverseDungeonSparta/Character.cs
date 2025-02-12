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
        public int TotalMaxHP { get; set; }
        public int TotalMaxMP { get; set; }
            
        public int TotalLuck //행운(치명타 확률, 회피율에 연관)
        {
            get
            {
                int valueLuk = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> equipItemList = player.equipItemList;
                    foreach (var equipItem in equipItemList)
                    {
                        valueLuk += equipItem.AddLuck;
                    }
                }

                return (int)(valueLuk);
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
                    List<EquipItem> equipItemList = player.equipItemList;
                    foreach (var equipItem in equipItemList)
                    {
                        valueInt += equipItem.AddIntelligence;
                    }
                }
                double value = Intelligence;
                if(IntelligenceBuff.Count > 0)
                {
                    value += IntelligenceBuff.Select(x => x.Item1).Sum();
                }
                return (int)(value+ valueInt);
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
                    List<EquipItem> equipItemList = player.equipItemList;
                    foreach(var equipItem in equipItemList)
                    {
                        valueDef += equipItem.AddDefence;
                    }
                }
                double value = 1d;
                if (DefenceBuff.Count > 0)
                {
                    value = DefenceBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)((Defence+valueDef) * value);
            } private set { }
        }//최종 방어력
        public int TotalAttack
        {
            get
            {
                int valueAtk = 0;
                Player player = GetPlayer();
                if (player != null)
                {
                    List<EquipItem> equipItems = player.equipItemList;
                    foreach (var equipItem in equipItems)
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
            }private set {}
        }//최종 공격력
        public int TotalCritical
        {
            get
            {
                //기본 값은 Luck 수치, 모든 Luck 관련 버프를 더한 후 나온 Luck / 2가 최종 치명타 확률
                double value = TotalLuck;
                if (LuckBuff.Count > 0)value += LuckBuff.Select(x => x.Item1).Sum();
                if (Critical * (value / 2) > 50) return 50;
                else return (int)(Critical * (value / 2));
            }private set { }
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
            } private set { }
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
                else if (_hp > MaxHP) _hp = MaxHP;
            }
        }               //체력
        public int MaxHP  //최대 체력
        {
            get
            {
                if (TotalMaxHP == 0)
                {
                    TotalMaxHP = 100;

                    Player player = GetPlayer();
                    if (player != null)
                    {
                        List<EquipItem> equipItemList = player.equipItemList;
                        foreach (var equipItem in equipItemList)
                        {
                            TotalMaxHP += equipItem.AddMaxHp;
                        }
                    }
                }
                return TotalMaxHP;
            }
            set { }
        }
        public int MP 
        { get 
            { return _mp; } 
            set 
            {
                _mp = value;
                if(_mp <= 0) _mp = 0;
                else if (_mp > MaxMP) _mp = MaxMP;
            } 
        }               //마나
        public int MaxMP //최대 마다
        {
            get
            {
                if (TotalMaxMP == 0)
                {
                    TotalMaxMP = 100;

                    Player player = GetPlayer();
                    if (player != null)
                    {
                        List<EquipItem> equipItemList = player.equipItemList;
                        foreach (var equipItem in equipItemList)
                        {
                            TotalMaxMP += equipItem.AddMaxMp;
                        }
                    }
                }
                return TotalMaxMP;
            }
            set { }
        }
        public int Speed { get; set; }  //속도
        public List<Skill> SkillList { get; set; }  //가지고 있는 스킬


        // 타겟을 매개변수로 받아 데미지를 계산하고 반환
        public virtual void Attacking(List<Character> targets, List<Monster> monsters, out int damage, Skill skill)
        {
            //데미지 계산식
            double margin = TotalAttack * 0.1f;
            margin = Math.Ceiling(margin);

            damage = new Random().Next(TotalAttack - (int)margin, TotalAttack + (int)margin);

            SkillType skillType = SkillType.Physical;

            if (skill != null)
            {
                skillType = skill.Type;
                ViewManager.PrintText($"{this.Name}의 스킬 사용!");
                foreach(Character onTarget in targets)
                {
                    string pullName = this.Name == onTarget.Name ? "자신" : $"{onTarget.Name}";
                    ViewManager.PrintText($"{this.Name}은 {onTarget.Name}에게 {skill.Name}을(를) 사용했다!");
                }
                ViewManager.PrintText("");
                ViewManager.PrintText($"     [{skill.Name}]");
                ViewManager.PrintText($"     : {skill.Info}");
                ViewManager.PrintText("");
            }
            else
            {
                ViewManager.PrintText($"{this.Name}의 공격!");
                foreach (Character onTarget in targets)
                {
                    ViewManager.PrintText($"{this.Name}은(는) {onTarget.Name}에게 공격을 시도했다!");
                }
            }

            foreach (Character onTarget in targets)
            {
                onTarget.OnDamage(this, damage, skill);
            }
        }


        // 데미지를 입는 메소드
        public void OnDamage(Character target, int damage, Skill skill)
        {
            Util.CheckKeyInputEnter();
            SkillType skillType = SkillType.Physical;
            if (skill != null) {skillType = skill.Type;}
            

            if (skill != null && skill.ApplyType == ApplyType.Team)
            {
                int beforeHP = this.HP;
                int beforeATK = this.TotalAttack;
                int beforeDEF = this.TotalDefence;
                int beforeCritical = this.TotalCritical;
                int beforeEvasion = this.Evasion;

                AddBuff(target, skill);
                ViewManager.PrintText($"{this.Name}의 스테이터스 변화");
                ViewManager.PrintText($"");
                ViewManager.PrintText($"{beforeHP} -> {this.HP}");
                ViewManager.PrintText($"{beforeATK} -> {this.TotalAttack}");
                ViewManager.PrintText($"{beforeDEF} -> {this.TotalDefence}");
                ViewManager.PrintText($"{beforeCritical}% -> {this.TotalCritical}%");
                ViewManager.PrintText($"{beforeEvasion}% -> {this.Evasion}%");
                ViewManager.PrintText($"");
            }
            else
            {
                //공격 실행




            }

            //TotalEvasion의 수치 만큼의 확률로 회피
            if (ComputeManager.TryChance(TotalEvasion))
            {
                ViewManager.PrintText("회피 성공!");
                ViewManager.PrintText($"{Name}은(는) {target.Name}의 공격을 피했습니다!");
            }
            else
            {
                ViewManager.PrintText("");
                //치명타가 발생한 경우
                if (ComputeManager.TryChance(target.TotalCritical))
                {
                    ViewManager.PrintText("치명적인 일격!!!");
                    damage *= 2;
                    Util.CheckKeyInputEnter();
                }

                //데미지에서 방어력을 제외한 데미지로 취급,
                if (skillType == SkillType.Physical)
                {
                    damage -= TotalDefence;
                }

                if (damage < 0) damage = 0;

                ViewManager.PrintText($"{target.Name}에게 총 {damage} 데미지를 입었습니다! ({HP} -> {HP - damage})");

                HP -= damage;
            }
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
    }

}

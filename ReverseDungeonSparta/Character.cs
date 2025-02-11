using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace ReverseDungeonSparta
{
    public class Character : Buffer
    {
        private int _hp;
        private int _mp;

        public string Name { get; set; } = string.Empty;    //이름
        public int Luck { get; set; }//행운(치명타 확률, 회피율에 연관)
        public int Attack { get; set; }//공격력
        public int Defence { get; set; }//방어력
        public int Critical { get; set; }//치명타확률
        public int Evasion {  get; set; }//회피력
        public int Intelligence { get; set; }//지능 마법 스킬에 연관
        public int TotalDefence
        {
            get
            {
                double value = 1d;
                if (DefenceBuff.Count > 0)
                {
                    value = DefenceBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)(Defence * value);
            }
            set { Defence = value; }
        }//최종 방어력
        public int TotalAttack
        {
            get
            {
                double value = 1d;
                if (AttackBuff.Count > 0)
                {
                    value = AttackBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                int result = (int)(Attack * value);
                return (int)(Attack * value);
            }
            set { Attack = value; }
        }//최종 공격력
        public int TotalCritical
        {
            get
            {
                double value = 1d;
                if (LuckBuff.Count > 0)
                {
                    value = LuckBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)(Critical * value);
            }
            set { Critical = value; }
        }//최종 치명타 확률
        public int TotalEvasion
        {
            get
            {
                double value = 1d;
                if (LuckBuff.Count > 0)
                {
                    value = LuckBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)(Evasion * value);
            }
            set { Evasion = value; }
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
        }           //체력
        public int MaxHP { get; set; }  //최대 체력
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
        public int MaxMP { get; set; }  //최대 마다
        public int Speed { get; set; }  //속도
        public List<Skill> SkillList { get; set; }  //가지고 있는 스킬


        // 타겟을 매개변수로 받아 데미지를 계산하고 반환
        public virtual void Attacking(Character target, List<Monster> monsters, out int damage)
        {
            //데미지 계산식
            double margin = TotalAttack * 0.1f;
            margin = Math.Ceiling(margin);

            damage = new Random().Next(TotalAttack - (int)margin, TotalAttack + (int)margin);

            target.OnDamage(this, damage);
        }


        // 데미지를 입는 메소드
        public void OnDamage(Character target, int damage)
        {
            int beforeHP = HP;
            HP -= damage;


            ViewManager.PrintText("");
            ViewManager.PrintText($"{target.Name}은(는) 총 {damage}의 피해를 입었습니다.)");
            ViewManager.PrintText("");
            ViewManager.PrintText($"{target.Name}에게 총 {damage} 데미지를 입혔습니다! ({beforeHP} -> {HP})");


            ViewManager.PrintText("회피 성공!");
            ViewManager.PrintText($"{Name}은(는) {target.Name}의 공격을 피했습니다!");
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

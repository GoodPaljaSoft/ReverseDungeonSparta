namespace ReverseDungeonSparta
{
    public class Character : Buffer
    {
        private int _hp;
        private int _attack;
        public string Name { get; set; } = string.Empty;
        public int Luck { get; set; }
        public int Defence { get; set; }
        public int Attack 
        {
            get 
            {
                double value = 1d;
                if (AttackBuff.Count > 0)
                {
                    value = AttackBuff.Select(x => x.Item1).Aggregate((total, next) => total * next);
                }
                return (int)(_attack * value);
            }
            set { _attack = value; }
        }
        public int Intelligence { get; set; }

        public int HP {
            get { return _hp; }
            set
            {
                _hp = value;

                if (_hp <= 0)
                {
                    _hp = 0;
                    Monster monster = GetMonster();
                    if(monster != null) monster.IsDead();
                }
                else if (_hp > MaxHP) _hp = MaxHP;
            }
        }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public int Speed { get; set; }

        public int Critical { get; set; }   // 치명타 확률
        public int Evasion { get; set; }    // 회피율

        public List<Skill> SkillList { get; set; }


        // 타겟을 매개변수로 받아 데미지를 계산하고 반환
        public virtual void Attacking(Character target,List<Monster> monsters ,out int damage)
        {
            //데미지 계산식
            double margin = Attack * 0.1f;
            margin = Math.Ceiling(margin);

            damage = new Random().Next(Attack - (int)margin, Attack + (int)margin);

            OnDamage(target, damage);
        }


        // 데미지를 입는 메소드
        public void OnDamage(Character target, int damage)
        {
            target.HP -= damage;

            if (target.HP <= 0)
            {
                target.HP = 0;
            }
        }


        //캐릭터클래스를 플레이어로 바꿔주는 메서드
        public Player GetPlayer()
        {
            if(this is Player) return (Player)this;
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

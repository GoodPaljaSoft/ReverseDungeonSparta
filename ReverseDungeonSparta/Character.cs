namespace ReverseDungeonSparta
{
    public class Character
    {
        public virtual string Name { get; set; } = string.Empty;
        public virtual int Luck { get; set; }
        public virtual int Defence { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Intelligence { get; set; }

        public virtual int HP { get; set; }
        public virtual int MaxHP { get; set; }
        public virtual int MP { get; set; }
        public virtual int MaxMP { get; set; }

        public virtual int Speed { get; set; }

        public virtual int Critical { get; set; }   // 치명타 확률
        public virtual int Evasion { get; set; }    // 회피율

        public List<Skill> SkillList { get; set; }


        // 타겟을 매개변수로 받아 데미지를 계산하고 반환
        public void Attacking(Character target, out int damage)
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


    }

}

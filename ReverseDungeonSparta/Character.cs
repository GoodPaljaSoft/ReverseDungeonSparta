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


        // 타겟을 매개변수로 받아 데미지를 계산하고 반환
        public int Attacking(Character target)
        {
            //데미지 계산식
            double margin = Attack * 0.1f;
            margin = Math.Ceiling(margin);

            int damage = new Random().Next(Attack - (int)margin, Attack + (int)margin);
            return damage;
        }

        // 데미지를 입는 메소드
        public void OnDamage(int damage)
        {
            HP -= damage;
        }


    }

}

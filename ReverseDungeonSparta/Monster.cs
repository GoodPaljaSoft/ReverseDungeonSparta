using System;

public class Monster
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    //public float Def { get; set; }
    public bool IsDie { get; set; } 

    public Monster(string name, int level, int hp, int atk, bool isDie)
    {
        Name = name;
        Level = level;
        Hp = hp;
        Atk = atk;
        IsDie = isDie;
    }


    //


    //공격 처리를 실행할 메서드
    public void Attack()
    {

    }

    public void Dead()
    {

    }

}

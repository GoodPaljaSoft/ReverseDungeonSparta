using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public class Buffer
    {
        //double은 배수 수치, int는 유지되는 턴을 가진다.
        public List<(double, int)> AttackBuff { get; set; } = new List<(double, int)>();
        public List<(double, int)> DefenceBuff { get; set; } = new List<(double, int)>();
        public List<(double, int)> CriticalBuff { get; set; } = new List<(double, int)>();
        public List<(double, int)> EvasionBuff { get; set; } = new List<(double, int)>();
        public List<(double, int)> HealingBuff { get; set; } = new List<(double, int)>();


        //턴을 끝낸 후 사용할 버프 메소드. 버프의 카운터를 1개씩 내림
        public void TurnEndBuff()
        {
            if (AttackBuff.Count > 0)
            {
                AttackBuff = AttackBuff
                    .Select(x => (x.Item1, x.Item2 - 1))
                    .ToList();
            }

            if (DefenceBuff.Count > 0)
            {
                DefenceBuff = DefenceBuff
                    .Select(x => (x.Item1, x.Item2 - 1))
                    .ToList();
            }

            if (CriticalBuff.Count > 0)
            {
                CriticalBuff = CriticalBuff
                    .Select(x => (x.Item1, x.Item2 - 1))
                    .ToList();
            }

            if (EvasionBuff.Count > 0)
            {
                EvasionBuff = EvasionBuff
                    .Select(x => (x.Item1, x.Item2 - 1))
                    .ToList();
            }

            if (HealingBuff.Count > 0)
            {
                HealingBuff = HealingBuff
                    .Select(x => (x.Item1, x.Item2 - 1))
                    .ToList();
            }
        }


        //턴이 시작됐을 때 사용할 버프 메소드. 버프의 카운터가 0이라면 버프 종료
        public void TurnStartBuff()
        {
            if (AttackBuff.Count > 0)
            {
                AttackBuff = AttackBuff
                    .Where(x => x.Item2 > 0)
                    .ToList();
            }

            if (DefenceBuff.Count > 0)
            {
                DefenceBuff = DefenceBuff
                    .Where(x => x.Item2 > 0)
                    .ToList();
            }

            if (CriticalBuff.Count > 0)
            {
                CriticalBuff = CriticalBuff
                    .Where(x => x.Item2 > 0)
                    .ToList();
            }

            if (EvasionBuff.Count > 0)
            {
                EvasionBuff = EvasionBuff
                    .Where(x => x.Item2 > 0)
                    .ToList();
            }

            if (HealingBuff.Count > 0)
            {
                HealingBuff = HealingBuff
                    .Where(x => x.Item2 > 0)
                    .ToList();

                if (HealingBuff.Count > 0)
                {
                    //본인 차례가 됐을 때 힐링 관련 버프가 남아있다면 해당 수치만큼 회복
                    Character character = this as Character;
                    foreach (var x in HealingBuff)
                    {
                        character.HP += (int)x.Item1;
                    }
                }
            }


        }


        //버프를 추가하는 메소드
        public void AddBuff(Character useCharacter,Skill skill)
        {
            BuffType buffType = skill.BufferType;
            double value = skill.Value;
            int turnCount = skill.BufferTurn;

            //스킬을 사용한 캐릭터가 버프가 올라가는 본인일 경우 턴 카운터를 하나 올려서 적용함.
            if(useCharacter == (Character)this)
            {
                turnCount++;
            }

            if (buffType == BuffType.AttackBuff)
            {
                AttackBuff.Add((value, turnCount));
            }
            else if (buffType == BuffType.DefenceBuff)
            {
                DefenceBuff.Add((value, turnCount));
            }
            else if (buffType == BuffType.CriticalBuff)
            {
                CriticalBuff.Add((value, turnCount));
            }
            else if (buffType == BuffType.EvasionBuff)
            {
                EvasionBuff.Add((value, turnCount));
            }
            else if (buffType == BuffType.HealingBuff)
            {
                EvasionBuff.Add((value, turnCount));
            }
        }


        //전투가 끝난 후 모든 버프를 해제하는 메서드
        public void ResetAllBuff()
        {
            AttackBuff = new List<(double, int)>();
            DefenceBuff = new List<(double, int)>();
            CriticalBuff = new List<(double, int)>();
            EvasionBuff = new List<(double, int)>();
            HealingBuff = new List<(double, int)>();
        }


        //턴을 시작하기 전 버프의 카운터가 0인 버프를 지움.
        //버프 카운터가 1이라면 1턴 유지.
        //즉 A B C 순서로 반복하며 작동한다고 가정했을 때,
        //A가 C에게 공격력 버프 1턴을 걸었음.
        //C는 본인 차례가 될 때까지 공격력 버프를 기본적으로 가지고 있음
        //본인 차례가 된 후 공격 이후 턴을 넘기면 해당 카운트는 0이 됨.
        //이후 본인 차례가 오면 해당 버프가 사라짐.
        //턴이 끝난 경우 모든 버프의 카운터를 1 줄임.
        //버프를 남에게 걸어준 경우랑 자신에게 건 경우를 나눠서 생갹해야함.
        //버프를 만이 본인에게 걸었다면 해당 버프카운트를 1 올린 상태로 추가하도록 함.


        //버프의 수치만큼 값을 적용시키는 메서드
        public void ApplyBuffValue(ref double attack, ref double defence, ref double critical, ref double evasion, ref double HP)
        {
            if (AttackBuff.Count > 0)
            {
                foreach (var x in AttackBuff)
                {
                    attack *= x.Item1;
                }
            }

            if (DefenceBuff.Count > 0)
            {
                foreach (var x in DefenceBuff)
                {
                    defence *= x.Item1;
                }
            }

            if (CriticalBuff.Count > 0)
            {
                foreach (var x in CriticalBuff)
                {
                    critical *= x.Item1;
                }
            }

            if (EvasionBuff.Count > 0)
            {
                foreach (var x in EvasionBuff)
                {
                    evasion *= x.Item1;
                }
            }

            if (HealingBuff.Count > 0)
            {
                foreach (var x in HealingBuff)
                {
                    HP += x.Item1;
                }
            }
        }
    }



    public enum BuffType
    {
        AttackBuff,
        DefenceBuff,
        CriticalBuff,
        EvasionBuff,
        HealingBuff,
        None
    }
}

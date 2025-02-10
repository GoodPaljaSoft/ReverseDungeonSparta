using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ReverseDungeonSparta;

public class TurnManager
{
    public List<Character> Characters { get; private set; }

    //속도를 기준으로 객체의 턴을 저장해서 플레이어를 기준으로 플레나올 때까지의 캐릭터를 전부 보낼 때 사용
    public List<Character> SeclectCharacter { get; private set; }   
    
    public List<(Character, double)> turnQueue { get; private set; }


    //턴 매니저 생성자
    public TurnManager(List<Character> characters)
    {
        Characters = characters;
        SeclectCharacter = new List<Character>();
        turnQueue = new List<(Character, double)>();


        turnQueue = Characters
            .Select(c => (c, 100.0d / c.Speed))
            .ToList();
    }


    //캐릭터 리스트를 스피드의 순서대로 정렬한 후 맨 앞의 캐릭터를 가져온 후 SeclectCharacter에 저장
    //이후 해당 캐릭터를 리스트에서 삭제한 후 속도를 더한 값으로 재정의하여 다시 리스트의 속도 순서에 맞게 추가함.
    public void CalculateTurnPreview()
    {
        //플레이어가 character 속에 존재하는지 확인
        bool isIncludePlayer = false;
        foreach (Character character in Characters)
        {
            if (character is Player)  isIncludePlayer = true;
        }

        SeclectCharacter = new List<Character>();

        //존재할 경우 시작
        if (isIncludePlayer)
        {
            turnQueue = turnQueue.Where(x => x.Item1.Speed != 0).ToList();
            while (true)
            {
                var nextCharacter = turnQueue.OrderBy(t => t.Item2).First();

                if (nextCharacter.Item1 is Player && SeclectCharacter.Count > 0)
                {
                    break;
                }

                SeclectCharacter.Add(nextCharacter.Item1);
                turnQueue = turnQueue
                    .Where(t => t.Item1 != nextCharacter.Item1)
                    .Append((nextCharacter.Item1, nextCharacter.Item2 + 100.0 / nextCharacter.Item1.Speed))
                    .OrderBy(t => t.Item2)
                    .ToList();
            }
        }

    }
}

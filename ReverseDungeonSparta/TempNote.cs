using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using ReverseDungeonSparta;

public class TurnManager
{
    public List<Character> Characters { get; private set; }
    public Character SeclectCharacter { get; private set; }
    public List<(Character, double)> turnQueue { get; private set; }


    //턴 매니저 생성자
    public TurnManager(List<Character> characters)
    {
        Characters = characters;
        SeclectCharacter = new Character();
        turnQueue = new List<(Character, double)>();
        turnQueue = Characters
    .Select(c => (c, 100.0d / c.Speed))
    .ToList();
    }


    //캐릭터 리스트를 스피드의 순서대로 정렬한 후 맨 앞의 캐릭터를 가져온 후 SeclectCharacter에 저장
    //이후 해당 캐릭터를 리스트에서 삭제한 후 속도를 더한 값으로 재정의하여 다시 리스트의 속도 순서에 맞게 추가함.
    public void CalculateTurnPreview(int previewTurns = 5)
    {
        SeclectCharacter = new Character();
        var nextCharacter = turnQueue.OrderBy(t => t.Item2).First();
        SeclectCharacter = nextCharacter.Item1;
        turnQueue = turnQueue
            .Where(t => t.Item1 != nextCharacter.Item1)
            .Append((nextCharacter.Item1, nextCharacter.Item2 + 100.0 / nextCharacter.Item1.Speed))
            .OrderBy(t => t.Item2)
            .ToList();
    }
}

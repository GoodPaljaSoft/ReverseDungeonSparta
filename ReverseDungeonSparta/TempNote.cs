using System;
using System.Collections.Generic;
using System.Linq;
using ReverseDungeonSparta;

public class TurnManager
{
    public List<Character> Characters { get; private set; }
    public List<Character> TurnPreview { get; private set; }

    public TurnManager()
    {
        Characters = new List<Character>();
        TurnPreview = new List<Character>();
    }


    public void CalculateTurnPreview(int previewTurns = 5)
    {
        var turnQueue = Characters
            .Select(c => (character: c, nextTurn: 100.0 / c.Speed))
            .ToList();

        //플레이어 스피드 10 -> 먼저 실행 10
        //나머지 몬스터   5               20

        TurnPreview.Clear();

        for (int i = 0; i < previewTurns; i++)
        {
            var nextCharacter = turnQueue.OrderBy(t => t.nextTurn).First();
            TurnPreview.Add(nextCharacter.character);
            turnQueue = turnQueue
                .Where(t => t.character != nextCharacter.character)
                .Append((character: nextCharacter.character, nextTurn: nextCharacter.nextTurn + 100.0 / nextCharacter.character.Speed))
                .OrderBy(t => t.nextTurn)
                .ToList();
        }
    }
}

public class BattleManager2
{
    public event Action<Character> OnTurnStart; // 턴 시작 이벤트
    private TurnManager turnManager;

    public void Battle()
    {
        turnManager.CalculateTurnPreview(); // 5턴 프리뷰를 계산
        var currentCharacter = turnManager.TurnPreview.First();

        TurnPlay(currentCharacter);
    }

    public void TurnPlay(Character character)
    {
        // 턴 시작 시 이벤트 발생
        OnTurnStart?.Invoke(character);

        // 현재 턴 처리 로직
        Console.WriteLine($"{character.Name}의 턴");

        // 예시: 몬스터가 사망하면 Speed를 0으로 설정 턴에서 제외
        if (/*몬스터라면*/)
        {
            Console.WriteLine($"{character.Name} 디짐");
            character.Speed = 0;
        }
    }
}

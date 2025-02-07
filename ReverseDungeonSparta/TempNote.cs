using System;
using System.Collections.Generic;
using System.Linq;
using ReverseDungeonSparta;

public class TurnManager
{
    public List<Charater> Charaters { get; private set; }
    public List<Charater> TurnPreview { get; private set; }

    public TurnManager()
    {
        Charaters = new List<Charater>();
        TurnPreview = new List<Charater>();
    }



    public void CalculateTurnPreview(int previewTurns = 5)
    {
        var turnQueue = Charaters
            .Select(c => (charater: c, nextTurn: 100.0 / c.Speed))
            .ToList();

        TurnPreview.Clear();

        for (int i = 0; i < previewTurns; i++)
        {
            var nextCharater = turnQueue.OrderBy(t => t.nextTurn).First();
            TurnPreview.Add(nextCharater.charater);
            turnQueue = turnQueue
                .Where(t => t.charater != nextCharater.charater)
                .Append((nextCharater.charater, nextCharater.nextTurn + 100.0 / nextCharater.charater.Speed))
                .OrderBy(t => t.nextTurn)
                .ToList();
        }
    }
}

public class BattleManager
{
    public event Action<Charater> OnTurnStart; // 턴 시작 이벤트
    private TurnManager turnManager;

    public void Battle()
    {
        turnManager.CalculateTurnPreview(); // 5턴 프리뷰를 계산
        var currentCharater = turnManager.TurnPreview.First();

        TurnPlay(currentCharater);
    }

    public void TurnPlay(Charater charater)
    {
        // 턴 시작 시 이벤트 발생
        OnTurnStart?.Invoke(charater);

        // 현재 턴 처리 로직
        Console.WriteLine($"{charater.Name}의 턴");

        // 예시: 몬스터가 사망하면 Speed를 0으로 설정 턴에서 제외
        if (/*몬스터라면*/)
        {
            Console.WriteLine($"{charater.Name} 디짐");
            charater.Speed = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public class TempNote
    { 
        // using System.Linq
        // using System.Collection.Generic
        // 상기 유징문 필요
        // 스피드를 기반으로 턴 계산하기, 프리뷰에 5개 이름생김, 상위클래스 Character 구현되어 있어야함, 인스턴스 몬스터 생성시 네임에 식별기호 있어야함(예 궁수A,궁수B) 
        List<string> turnOrderPreview = new List<string>();
        public void CalculateTurnOrder(List<Character> Monsters, Character player, int previewTurns = 5)
        {
            List<Character> Characters = Monsters;
            Characters.Add(player);
            
            // 우선순위 큐 대신 매 턴 계산
            turnOrderPreview.Clear();
            List<(Character character, double nextTurn)> turnQueue = Characters
                .Select(c => (character: c, nextTurn: 100.0 / c.Speed)) // 속도에 따라 다음 턴 간격 계산
                .ToList();

            // 턴을 5회 계산
            for (int i = 0; i < previewTurns; i++)
            {
                var nextCharacter = turnQueue.OrderBy(t => t.nextTurn).First();
                turnOrderPreview.Add(nextCharacter.character.Name);

                // 다음 턴 시간 증가
                nextCharacter.nextTurn += 100.0 / nextCharacter.character.Speed;

                // 리스트를 업데이트
                turnQueue = turnQueue
                    .Where(t => t.character != nextCharacter.character)
                    .Append(nextCharacter)
                    .OrderBy(t => t.nextTurn)
                    .ToList();
            }       
        }

        // 


    }

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

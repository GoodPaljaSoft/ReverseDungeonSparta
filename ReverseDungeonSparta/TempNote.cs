using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
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

=======
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
>>>>>>> Dev2_YHJ_SJW
}

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

}

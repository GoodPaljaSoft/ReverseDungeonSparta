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
    public List<Character> TurnPreview { get; private set; }
    public List<(Character, double)> turnQueue { get; private set; }

    public TurnManager(List<Character> characters)
    {
        Characters = characters;
        TurnPreview = new List<Character>();
        turnQueue = new List<(Character, double)>();
        turnQueue = Characters
    .Select(c => (c, 100.0 / c.Speed))
    .ToList();
    }


    public void CalculateTurnPreview(int previewTurns = 5)
    {
        TurnPreview.Clear();
        var nextCharacter = turnQueue.OrderBy(t => t.Item2).First();
        TurnPreview.Add(nextCharacter.Item1);
        turnQueue = turnQueue
            .Where(t => t.Item1 != nextCharacter.Item1)
            .Append((nextCharacter.Item1, nextCharacter.Item2 + 100.0 / nextCharacter.Item1.Speed))
            .OrderBy(t => t.Item2)
            .ToList();
    }
}

using System.Linq;
using UnityEngine;

public class EnemyPlayerSequential : EnemyPlayer
{
    public CellSequenceContainer container;
    private int _sequenceIndex;
    private int _coordsIndex;

    [SerializeField]
    private bool _randomOrder;
    private readonly System.Random _random = new System.Random();

    public override void Init()
    {
        base.Init();

        if (container != null && _randomOrder)
            ShuffleSequences();
    }

    public override void ResetSelf()
    {
        _sequenceIndex = 0;
        _coordsIndex = 0;

        if (container != null && _randomOrder)
            ShuffleSequences();

        base.ResetSelf();
    }

    protected override Direction GetDirection()
    {
        if (_directionLocked)
            return Direction.Neutral;

        if (_sequenceIndex > container.sequences.Length - 1)
        {
            ResetSequences();
            return Direction.Neutral;
        }

        if (_coordsIndex > container.sequences[_sequenceIndex].coords.Length - 1)
        {
            NextSequence();
            return Direction.Neutral;
        }

        var cellInSequence = grid.GetCellByCoords(container.sequences[_sequenceIndex].coords[_coordsIndex]);

        if (cellInSequence == null)
        {
            Debug.LogWarning(nameof(cellInSequence) + " was null when getting direction.");
            return Direction.Neutral;
        }

        var nextDir = GetNextDirectionByCoords(grid.currentCell, cellInSequence);

        if (nextDir != Direction.Neutral)
            return nextDir;

        if (cellInSequence == grid.currentCell)
            _coordsIndex++;

        return nextDir;
    }

    protected virtual Direction GetNextDirectionByCoords(int myX, int myY, int destX, int destY)
    {
        if (myX < destX)
            return Direction.Right;

        if (myX > destX)
            return Direction.Left;

        if (myY < destY)
            return Direction.Up;

        if (myY > destY)
            return Direction.Down;

        return Direction.Neutral;
    }

    protected virtual Direction GetNextDirectionByCoords(Cell myCell, Cell destCell)
    {
        return GetNextDirectionByCoords(myCell.xCoord, myCell.yCoord, destCell.xCoord, destCell.yCoord);
    }

    private void NextSequence()
    {
        Debug.Log("Cell sequence finished. Going to next sequence in collection.");
        _coordsIndex = 0;
        _sequenceIndex++;
    }

    private void ResetSequences()
    {
        Debug.Log("Cell sequence collection finished. Resetting.");
        _sequenceIndex = 0;
    }

    private void ShuffleSequences()
    {
        var shuffled = container.sequences.OrderBy(x => _random.Next()).ToArray();
        container.sequences = shuffled;
    }
}

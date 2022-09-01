using UnityEngine;

public class EnemyPlayerSequential : EnemyPlayer
{
    public CellSequenceContainer container;
    private int _sequenceIndex;
    private int _coordsIndex;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ResetSelf()
    {
        _sequenceIndex = 0;
        _coordsIndex = 0;
        base.ResetSelf();
    }

    protected override Direction GetDirection()
    {
        if (_sequenceIndex > container.sequences.Length - 1)
        {
            Debug.Log("Cell sequence collection finished. Resetting.");
            _sequenceIndex = 0;
            return Direction.Neutral;
        }

        if (_coordsIndex > container.sequences[_sequenceIndex].coords.Length - 1)
        {
            Debug.Log("Cell sequence finished. Re-sequencing.");
            _coordsIndex = 0;
            _sequenceIndex++;
            return Direction.Neutral;
        }

        var cellInSequence = grid.GetCellByCoords(container.sequences[_sequenceIndex].coords[_coordsIndex]);

        if (cellInSequence == null)
        {
            Debug.LogWarning(nameof(cellInSequence) + " was null when getting direction.");
            return Direction.Neutral;
        }

        if (grid.currentCell.xCoord < cellInSequence.xCoord)
            return Direction.Right;

        if (grid.currentCell.xCoord > cellInSequence.xCoord)
            return Direction.Left;

        if (grid.currentCell.yCoord < cellInSequence.yCoord)
            return Direction.Up;

        if (grid.currentCell.yCoord > cellInSequence.yCoord)
            return Direction.Down;

        if (cellInSequence == grid.currentCell)
            _coordsIndex++;

        return Direction.Neutral;
    }
}

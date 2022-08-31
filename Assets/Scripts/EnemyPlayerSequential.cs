using UnityEngine;

public class EnemyPlayerSequential : EnemyPlayer
{
    public CellSequence sequence;
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
        _coordsIndex = 0;
        base.ResetSelf();
    }

    protected override Direction GetDirection()
    {
        if (_coordsIndex > sequence.coords.Length - 1)
        {
            Debug.Log("Cell sequence finished.");
            _coordsIndex = 0;
            return Direction.Neutral;
        }

        var cellInSequence = grid.GetCellByCoords(sequence.coords[_coordsIndex]);

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

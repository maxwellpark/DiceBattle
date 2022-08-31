using UnityEngine;

public class EnemyPlayerSequential : EnemyPlayer
{
    public CellSequence sequence;
    private Cell CellInSequence => sequence.cells[_cellIndex];
    
    private int _cellIndex;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override Direction GetDirection()
    {
        if (CellInSequence == grid.currentCell)
            _cellIndex++;

        if (_cellIndex > sequence.cells.Length - 1)
        {
            Debug.Log("Cell sequence finished.");
            _cellIndex = 0;
            return Direction.Neutral;
        }

        if (grid.currentCell.xCoord < CellInSequence.xCoord)
            return Direction.Right;

        if (grid.currentCell.xCoord > CellInSequence.xCoord)
            return Direction.Left;

        if (grid.currentCell.yCoord < CellInSequence.yCoord)
            return Direction.Up;

        if (grid.currentCell.yCoord < CellInSequence.yCoord)
            return Direction.Down;

        return Direction.Neutral;
    }
}

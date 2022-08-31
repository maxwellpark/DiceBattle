using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Cell the player is currently in 
    [HideInInspector]
    public Cell currentCell;
    [HideInInspector]
    public Cell previousCell;
    public Cell startingCell;

    public Cell topLeftCell;
    public Cell topMiddleCell;
    public Cell topRightCell;

    public Cell middleLeftCell;
    public Cell middleMiddleCell;
    public Cell middleRightCell;

    public Cell bottomLeftCell;
    public Cell bottomMiddleCell;
    public Cell bottomRightCell;

    public List<Cell> cellCollection;

    private void Awake()
    {
        cellCollection = GetCellCollection();
        currentCell = startingCell;
        previousCell = currentCell;
    }

    /// <summary>
    /// Returns the current cell after updating it on the grid 
    /// </summary>
    /// <param name="direction"></param>
    public virtual Cell UpdateGrid(Direction direction)
    {
        if (currentCell == null)
            return null;

        var neighbour = currentCell.GetNeighbourByDirection(direction);

        if (neighbour != null && neighbour != currentCell)
        {
            previousCell = currentCell;
            currentCell = neighbour;
        }
        return currentCell;
    }

    public void UpdateGrid(Cell cell)
    {
        if (cell == null || cell == currentCell)
            return;

        previousCell = currentCell;
        currentCell = cell;
    }

    public List<Cell> GetCellCollection()
    {
        return new List<Cell>
        {
            topLeftCell,
            topMiddleCell,
            topRightCell,
            middleLeftCell,
            middleMiddleCell,
            middleRightCell,
            bottomLeftCell,
            bottomMiddleCell,
            bottomRightCell
        };
    }

    public Cell GetCellByCoords((int, int) coords)
    {
        foreach (var cell in cellCollection)
        {
            if (cell.xCoord == coords.Item1 && cell.yCoord == coords.Item2)
                return cell;
        }
        return null;
    }

    public Cell GetCellByCoords(CoordPair coords)
    {
        foreach (var cell in cellCollection)
        {
            if (cell.xCoord == coords.x && cell.yCoord == coords.y)
                return cell;
        }
        return null;
    }

    public bool IsBarrierOnColumn(int yCoord)
    {
        foreach (var cell in cellCollection)
        {
            if (cell.yCoord == yCoord && cell.hasBarrier)
                return true;
        }
        return false;
    }

    public bool IsBarrierOnRow(int xCoord)
    {
        foreach (var cell in cellCollection)
        {
            if (cell.yCoord == xCoord && cell.hasBarrier)
                return true;
        }
        return false;
    }
}

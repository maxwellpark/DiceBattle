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

    private void Awake()
    {
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
}

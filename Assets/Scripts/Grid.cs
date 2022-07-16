using UnityEngine;

public class Grid : MonoBehaviour
{
    // Cell the player is currently in 
    [HideInInspector]
    public Cell activeCell;

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
        activeCell = startingCell;
    }

    public Cell UpdateGrid(float x, float y)
    {
        if (activeCell == null)
            return null;

        var direction = GetDirection(x, y);
        var neighbour = activeCell.GetNeighbour(direction);

        if (neighbour != null)
            activeCell = neighbour;

        return activeCell;
    }

    private Direction GetDirection(float x, float y)
    {
        if (x == 1)
            return Direction.Right;

        if (x == -1)
            return Direction.Left;

        if (y == 1)
            return Direction.Up;

        if (y == -1)
            return Direction.Down;

        return Direction.Neutral;
    }
}

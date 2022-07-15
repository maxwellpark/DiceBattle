using UnityEngine;

public class Grid : MonoBehaviour
{
    // Cell the player is currently in 
    public Cell activeCell;

    public Cell topLeftCell;
    public Cell topMiddleCell;
    public Cell topRightCell;

    public Cell middleLeftCell;
    public Cell middleMiddleCell;
    public Cell middleRightCell;

    public Cell bottomLeftCell;
    public Cell bottomMiddleCell;
    public Cell bottomRightCell;

    private void Update()
    {
        if (activeCell == null)
            return;

        var direction = GetDirection();
        var neighbour = activeCell.GetNeighbour(direction);

        if (neighbour != null)
            activeCell = neighbour;
    }

    private Direction GetDirection()
    {
        // Look for right/left 
        var xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis == 1)
            return Direction.Right;

        if (xAxis == -1)
            return Direction.Left;

        // Look for up/down 
        var yAxis = Input.GetAxisRaw("Vertical");

        if (yAxis == 1)
            return Direction.Up;

        if (yAxis == -1)
            return Direction.Down;

        return Direction.Neutral;
    }
}

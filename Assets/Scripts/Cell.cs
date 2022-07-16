using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Down, Left, Right, Neutral
}

public class Cell : MonoBehaviour
{
    // Neighbours 
    public Cell upNeighbour;
    public Cell downNeighbour;
    public Cell leftNeighbour;
    public Cell rightNeighbour;

    public List<Cell> availableNeighbours;

    public int xCoord;
    public int yCoord;

    private void Start()
    {
        availableNeighbours = GetAvailableNeighbours();
    }

    public Cell GetNeighbourByDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Up => upNeighbour,
            Direction.Down => downNeighbour,
            Direction.Left => leftNeighbour,
            Direction.Right => rightNeighbour,
            _ => null,
        };
    }

    public List<Cell> GetAvailableNeighbours()
    {
        var neighbours = new List<Cell>();

        if (upNeighbour != null)
            neighbours.Add(upNeighbour);

        if (downNeighbour != null)
            neighbours.Add(downNeighbour);

        if (leftNeighbour != null)
            neighbours.Add(leftNeighbour);

        if (rightNeighbour != null)
            neighbours.Add(rightNeighbour);

        return neighbours;
    }
}

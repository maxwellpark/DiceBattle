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

    public Cell GetNeighbour(Direction direction)
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
}

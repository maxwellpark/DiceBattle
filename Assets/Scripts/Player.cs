using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid grid;

    [SerializeField]
    private float _yOffset = 1f;

    protected virtual void Start()
    {
        MovePlayer(grid.activeCell.transform.position);
    }

    protected virtual void Update()
    {
        var direction = GetDirection();
        var cell = grid.UpdateGrid(direction);

        if (cell != null)
            MovePlayer(cell.transform.position);
    }

    protected virtual Direction GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            return Direction.Up;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            return Direction.Down;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            return Direction.Left;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            return Direction.Right;

        return Direction.Neutral;
    }

    public void MovePlayer(Vector3 destination)
    {
        transform.position = new Vector3(destination.x, destination.y + _yOffset, destination.z);
    }
}

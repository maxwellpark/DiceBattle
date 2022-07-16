using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Grid grid;
    public event UnityAction onPlayerMove;

    [SerializeField]
    private float _yOffset = 1f;

    protected virtual void Start()
    {
        MovePlayer(grid.currentCell.transform.position);
    }

    protected virtual void Update()
    {
        var direction = GetDirection();

        if (direction == Direction.Neutral)
            return;

        var cell = grid.UpdateGrid(direction);

        if (cell != null)
        {
            MovePlayer(cell.transform.position);
            onPlayerMove?.Invoke();
        }
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

    protected void MovePlayer(Vector3 destination)
    {
        transform.position = new Vector3(destination.x, destination.y + _yOffset, destination.z);
    }

    public virtual void ResetSelf()
    {
        gameObject.SetActive(true);
        GoToRandomCell();
    }

    public void GoToRandomCell()
    {
        var index = Random.Range(0, grid.cellCollection.Count);
        var cell = grid.cellCollection[index];
        grid.UpdateGrid(cell);
        MovePlayer(cell.transform.position);
    }
}

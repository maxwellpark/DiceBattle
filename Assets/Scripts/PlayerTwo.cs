using UnityEngine;
using UnityEngine.Events;

public class PlayerTwo : MonoBehaviour
{
    public Grid grid;
    public event UnityAction onPlayerMove;

    [SerializeField]
    private float _yOffset = 1f;

    public float moveDelta;

    protected virtual void Start()
    {
        MovePlayer(grid.currentCell.transform.position);
    }

    protected virtual void Update()
    {
        moveDelta += Time.deltaTime;
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
        if (Input.GetKeyDown(KeyCode.I))
            return Direction.Up;

        if (Input.GetKeyDown(KeyCode.K) )
            return Direction.Down;

        if (Input.GetKeyDown(KeyCode.J))
            return Direction.Left;

        if (Input.GetKeyDown(KeyCode.L) )
            return Direction.Right;

        return Direction.Neutral;
    }

    protected void MovePlayer(Vector3 destination)
    {
        transform.position = new Vector3(destination.x, destination.y + _yOffset, destination.z);
        moveDelta = 0f;
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

    protected virtual void RegisterEvents()
    {

    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Grid grid;
    public event UnityAction onPlayerMove;

    [SerializeField]
    private float _yOffset = 1f;
    [SerializeField]
    private float _moveDuration = 0.25f;
    [SerializeField]
    private float _moveBuffer = 0.2f;

    protected bool _directionLocked;
    public float moveDelta;

    public virtual void Init()
    {
        var gridObj = GameObject.FindWithTag("PlayerGrid");
        if (gridObj == null)
            Debug.LogError("Could not find PlayerGrid (by tag)");
        else
            grid = gridObj.GetComponent<Grid>();
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
            var destination = new Vector3(
                cell.transform.position.x, cell.transform.position.y + _yOffset, cell.transform.position.z);

            StartCoroutine(MovePlayerSmooth(transform.position, destination));
            onPlayerMove?.Invoke();
        }
    }

    protected virtual Direction GetDirection()
    {
        if (_directionLocked)
            return Direction.Neutral;

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
        var offsetDest = new Vector3(destination.x, destination.y + _yOffset, destination.z); // Add y offset
        transform.position = offsetDest;
        moveDelta = 0f;
    }

    protected IEnumerator MovePlayerSmooth(Vector3 startPosition, Vector3 destination)
    {
        _directionLocked = true;
        var elapsed = 0f;

        while (elapsed < _moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, destination, elapsed / _moveDuration);
            elapsed += Time.deltaTime;

            // Allow input buffering
            if (elapsed >= _moveBuffer)
                _directionLocked = false;

            yield return null;
        }
        transform.position = destination;
        _directionLocked = false;
    }

    public virtual void ResetSelf()
    {
        _directionLocked = false;
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

    private void OnValidate()
    {
        _moveBuffer = Mathf.Min(_moveDuration, _moveBuffer);
    }
}

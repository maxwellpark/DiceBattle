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

    public bool directionLocked;
    public float moveDelta;

    private AudioSource _audioSource;

    protected virtual string GridTag { get; } = "PlayerGrid";

    public virtual void Init()
    {
        var gridObj = GameObject.FindWithTag(GridTag);
        if (gridObj == null)
            Debug.LogError($"Could not find grid '{GridTag}' by tag");
        else
            grid = gridObj.GetComponent<Grid>();
        MovePlayer(grid.currentCell.transform.position);
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        moveDelta += Time.deltaTime;
        var direction = GetDirection();

        if (directionLocked || direction == Direction.Neutral)
            return;

        var cell = grid.UpdateGrid(direction);

        if (cell != null)
        {
            var destination = new Vector3(
                cell.transform.position.x, cell.transform.position.y + _yOffset, cell.transform.position.z);

            StartCoroutine(MovePlayerSmooth(transform.position, destination));
            onPlayerMove?.Invoke();
            // TODO: Add audio clips 
            //_audioSource.Play();
        }
    }

    protected virtual Direction GetDirection()
    {
        if (directionLocked)
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
        directionLocked = true;
        var elapsed = 0f;

        while (elapsed < _moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, destination, elapsed / _moveDuration);
            elapsed += Time.deltaTime;

            // Allow input buffering
            if (elapsed >= _moveBuffer)
                directionLocked = false;

            yield return null;
        }
        directionLocked = false;
    }

    public virtual void PrepareForRound()
    {
        directionLocked = true;
        gameObject.SetActive(true);
        GoToRandomCell();
    }

    public virtual void StartRound()
    {
        directionLocked = false;
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

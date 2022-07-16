using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private float _yOffset = 1f;

    [SerializeField]
    private float _moveDelay = 0.05f;

    private bool _canMove;

    private void Start()
    {
        MovePlayer(grid.activeCell.transform.position);
    }

    private void Update()
    {
        if (!_canMove)
            return;

        var xAxis = Input.GetAxisRaw("Horizontal");
        var yAxis = Input.GetAxisRaw("Vertical");

        if (xAxis == 0 && yAxis == 0)
            return;

        var cell = grid.UpdateGrid(xAxis, yAxis);

        if (cell != null)
            MovePlayer(cell.transform.position);
    }

    public void MovePlayer(Vector3 destination)
    {
        transform.position = new Vector3(destination.x, destination.y + _yOffset, destination.z);
        StartCoroutine(DelayMovement());
    }

    private IEnumerator DelayMovement()
    {
        _canMove = false;
        yield return new WaitForSeconds(_moveDelay);
        _canMove = true;
    }
}

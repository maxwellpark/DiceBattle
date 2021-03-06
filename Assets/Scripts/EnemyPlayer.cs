using System;
using System.Collections;
using UnityEngine;

public class EnemyPlayer : Player
{
    [SerializeField]
    private float _moveDelayInSeconds;

    private bool _canMove = true;
    private GameObject _playerObj;
    private Player _player;
    private readonly float _moveDeltaThreshold = 1f;
    private readonly float _timeout = 2f;
    private bool _barrierPref;

    protected override void Start()
    {
        _playerObj = GameObject.FindWithTag("Player");

        if (_playerObj == null)
            throw new Exception("Could not find player object.");

        _player = _playerObj.GetComponent<Player>();
        onPlayerMove += () => StartCoroutine(DelayMovement());
        //_player.onPlayerMove += () => StartCoroutine(DelayMovement());
        base.Start();
    }

    protected override void Update()
    {
        if (moveDelta >= _timeout)
            Realign();

        moveDelta += Time.deltaTime; // To cover downtime 
        if (!_canMove)
            return;

        base.Update();
    }

    protected override Direction GetDirection()
    {
        var playerCell = _player.grid.currentCell;
        var enemyCell = grid.currentCell;

        if (playerCell == null || enemyCell == null)
            return Direction.Neutral;

        // Stick to barrier row if one exists and barrier is preferred, rather than following
        if (_barrierPref)
        {
            if (enemyCell.hasBarrier)
                return Direction.Neutral;

            if (grid.IsBarrierOnRow(enemyCell.xCoord))
                return Direction.Right;

            if (grid.IsBarrierOnColumn(enemyCell.yCoord))
                return Direction.Neutral;

            if (grid.IsBarrierOnColumn(enemyCell.yCoord + 1))
                return Direction.Up;

            if (grid.IsBarrierOnColumn(enemyCell.yCoord - 1))
                return Direction.Down;
        }

        if (playerCell.yCoord > enemyCell.yCoord)
            return Direction.Up;

        if (playerCell.yCoord < enemyCell.yCoord)
            return Direction.Down;

        // Vary x axis movement 
        if (_player.moveDelta >= _moveDeltaThreshold)
        {
            // Stick to barrier column if one exists
            if (grid.IsBarrierOnRow(enemyCell.xCoord))
                return Direction.Right;

            var rand = UnityEngine.Random.Range(0, 2);
            return rand == 0 ? Direction.Left : Direction.Right;
        }
        return Direction.Neutral;
    }

    private IEnumerator DelayMovement()
    {
        _canMove = false;
        yield return new WaitForSeconds(_moveDelayInSeconds);
        _canMove = true;
    }

    public override void ResetSelf()
    {
        base.ResetSelf();
        _barrierPref = false;
        _canMove = true;
    }

    protected override void RegisterEvents()
    {
        // Prefer barriers when health is low 
        EnemyShooting.onLowHealth += () => _barrierPref = true;
    }

    private void Realign()
    {
        Cell currentCell = default;

        foreach (var cell in grid.cellCollection)
        {
            if (cell.transform.position.z == transform.position.z)
            {
                currentCell = cell;
                break;
            }
        }

        if (currentCell == null)
            return;

        grid.UpdateGrid(currentCell);
    }
}

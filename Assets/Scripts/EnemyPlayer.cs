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

    protected override void Start()
    {
        _playerObj = GameObject.FindWithTag("Player");

        if (_playerObj == null)
            throw new Exception("Could not find player object.");

        _player = _playerObj.GetComponent<Player>();
        onPlayerMove += () => StartCoroutine(DelayMovement());
        base.Start();
    }

    protected override void Update()
    {
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

        if (playerCell.yCoord > enemyCell.yCoord)
            return Direction.Up;

        if (playerCell.yCoord < enemyCell.yCoord)
            return Direction.Down;

        return Direction.Neutral;
    }

    private IEnumerator DelayMovement()
    {
        _canMove = false;
        yield return new WaitForSeconds(_moveDelayInSeconds);
        _canMove = true;
    }
}

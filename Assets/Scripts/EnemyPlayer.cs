using System;
using UnityEngine;

public class EnemyPlayer : Player
{
    [SerializeField]
    private float _moveDelayInSeconds;

    private bool _canMove;
    private GameObject _playerObj;
    private Player _player;

    protected override void Start()
    {
        _playerObj = GameObject.FindWithTag("Player");

        if (_playerObj == null)
            throw new Exception("Could not find player object.");

        _player = _playerObj.GetComponent<Player>();
        base.Start();
    }

    protected override Direction GetDirection()
    {
        var playerCell = _player.grid.activeCell;
        var enemyCell = grid.activeCell;

        if (playerCell == null || enemyCell == null)
            return Direction.Neutral;

        if (playerCell.yCoord > enemyCell.yCoord)
            return Direction.Up;

        if (playerCell.yCoord < enemyCell.yCoord)
            return Direction.Down;

        return Direction.Neutral;
    }
}

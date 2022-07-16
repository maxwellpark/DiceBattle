using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private PlayerShooting _playerShooting;

    private EnemyPlayer _enemy;
    private EnemyShooting _enemyShooting;

    private BarrierManager _barrierManager;

    private int _currentRound;
    private int _playerRoundsWon;
    private int _enemyRoundsWon;

    private void Awake()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
            throw new System.Exception("Player not found");

        _player = playerObj.GetComponent<Player>();
        _playerShooting = playerObj.GetComponent<PlayerShooting>();

        var enemyObj = GameObject.FindWithTag("Enemy");
        if (enemyObj == null)
            throw new System.Exception("Enemy not found");

        _enemy = enemyObj.GetComponent<EnemyPlayer>();
        _enemyShooting = enemyObj.GetComponent<EnemyShooting>();
        _barrierManager = FindObjectOfType<BarrierManager>();

        RegisterEvents();
    }

    private void Start()
    {
        // For testing
        NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }

    public void NewGame(int player1Roll, int player2Roll)
    {
        Debug.Log("Setting up new game...");
        _currentRound = 0;
        _playerRoundsWon = 0;
        _enemyRoundsWon = 0;
        NewRound(player1Roll, player2Roll);
    }

    public void NewRound(int player1Roll, int player2Roll)
    {
        _currentRound++;
        Debug.Log("Setting up new round...");
        Debug.Log("Player 1 roll = " + player1Roll);
        Debug.Log("Player 2 roll = " + player2Roll);

        // Setup round based on dice roll values
        _playerShooting.SetupShooting(player1Roll);
        _enemyShooting.SetupShooting(player2Roll);

        // Subtract 6 from roll to get no. of barriers
        _barrierManager.SetupBarriers(6 - player1Roll, 6 - player2Roll);

        _player.ResetSelf();
        _enemy.ResetSelf();
    }

    public void RoundComplete()
    {
        Debug.Log("Round " + _currentRound + " complete");
        _playerShooting.ClearBullets();
        _playerShooting.gameObject.SetActive(false);
        _enemyShooting.gameObject.SetActive(false);

        if (_playerRoundsWon >= 2)
        {
            GameComplete(true);
        }
        else if (_enemyRoundsWon >= 2)
        {
            GameComplete(false);
        }
        else
        {
            // For testing
            NewRound(Random.Range(1, 7), Random.Range(1, 7));
        }
    }

    public void GameComplete(bool player1Wins)
    {
        if (player1Wins)
        {
            Debug.Log("Player 1 wins 3 round game");
        }
        else
        {
            Debug.Log("Player 2 wins 3 round game");
        }
    }

    private void RegisterEvents()
    {
        _playerShooting.onDeath += () =>
        {
            _enemyRoundsWon++;
            // For testing
            RoundComplete();
        };
        _enemyShooting.onDeath += () =>
        {
            _playerRoundsWon++;
            // For testing
            RoundComplete();
        };
    }

    private void Update()
    {
        // For testing 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
            NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public PlayerShooting playerShooting;

    public EnemyPlayer enemy;
    public EnemyShooting enemyShooting;

    private BarrierManager _barrierManager;

    private int _currentRound;
    private int _playerRoundsWon;
    private int _enemyRoundsWon;

    private void Awake()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
            throw new System.Exception("Player not found");

        player = playerObj.GetComponent<Player>();
        playerShooting = playerObj.GetComponent<PlayerShooting>();

        var enemyObj = GameObject.FindWithTag("Enemy");
        if (enemyObj == null)
            throw new System.Exception("Enemy not found");

        enemy = enemyObj.GetComponent<EnemyPlayer>();
        enemyShooting = enemyObj.GetComponent<EnemyShooting>();
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
        playerShooting.SetupShooting(player1Roll);
        enemyShooting.SetupShooting(player2Roll);

        // Subtract 6 from roll to get no. of barriers
        _barrierManager.SetupBarriers(6 - player1Roll, 6 - player2Roll);

        player.ResetSelf();
        enemy.ResetSelf();
    }

    public void RoundComplete()
    {
        Debug.Log("Round " + _currentRound + " complete");
        playerShooting.ClearBullets();
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);

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
        enemyShooting.onDeath += () =>
        {
            Debug.Log("Player 1 wins round " + _currentRound);
            _playerRoundsWon++;
            // For testing
            RoundComplete();
        };

        playerShooting.onDeath += () =>
        {
            Debug.Log("Player 2 wins round " + _currentRound);
            _enemyRoundsWon++;
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

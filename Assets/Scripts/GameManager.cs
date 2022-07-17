using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private NewRoundsUI _newRoundsUI;
    private BulletDisplayUI _bulletDisplayUI;

    public Player player;
    public PlayerShooting playerShooting;

    public EnemyPlayer enemy;
    public EnemyShooting enemyShooting;

    private BarrierManager _barrierManager;

    public GameObject diceContainer;
    public DiceAnimController player1Dice;
    public DiceAnimController player2Dice;

    private int _currentRound;
    private int _playerRoundsWon;
    private int _enemyRoundsWon;

    public event UnityAction onNewGame;
    public event UnityAction onNewRound;
    public event UnityAction onGameComplete;

    private void Awake()
    {
        _newRoundsUI = FindObjectOfType<NewRoundsUI>();
        _bulletDisplayUI = FindObjectOfType<BulletDisplayUI>();

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
        Init();
    }

    public void NewGame()
    {
        Debug.Log("Setting up new game...");
        _currentRound = 0;
        _playerRoundsWon = 0;
        _enemyRoundsWon = 0;
        _newRoundsUI.ShowNewGameUI(false);
        onNewGame?.Invoke();
        NewRound();
    }

    public void NewRound()
    {
        var player1Roll = player1Dice.RollDice(out float duration1);
        var player2Roll = player2Dice.RollDice(out float duration2);
        StartCoroutine(WaitForDiceAndStartRound(DiceAnimController.animWaitTimeInSeconds, player1Roll, player2Roll));
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

        ToggleObjects(true);
        _newRoundsUI.ShowNewRoundUI(false);
        player.ResetSelf();
        enemy.ResetSelf();
        onNewRound?.Invoke();
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
            ToggleObjects(false);
            _newRoundsUI.ShowNewRoundUI(true);
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
        _currentRound = 0;
        _newRoundsUI.ShowNewGameUI(true);
        onGameComplete?.Invoke();
    }

    private void RegisterEvents()
    {
        enemyShooting.onDeath += () =>
        {
            Debug.Log("Player 1 wins round " + _currentRound);
            _playerRoundsWon++;
            RoundComplete();
        };

        playerShooting.onDeath += () =>
        {
            Debug.Log("Player 2 wins round " + _currentRound);
            _enemyRoundsWon++;
            RoundComplete();
        };
    }

    private void Init()
    {
        // Setup UI 
        _newRoundsUI.newGameBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newGameBtn.onClick.AddListener(NewGame);
        _newRoundsUI.newRoundBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newRoundBtn.onClick.AddListener(NewRound);
        _newRoundsUI.ShowNewGameUI(true);
        ToggleObjects(false);
    }

    private void ToggleObjects(bool active)
    {
        player.grid.gameObject.SetActive(active);
        player.gameObject.SetActive(active);
        enemy.grid.gameObject.SetActive(active);
        enemy.gameObject.SetActive(active);
        if (!active)
            _bulletDisplayUI.ClearTexts();
        //diceContainer.SetActive(active);
        player1Dice.gameObject.SetActive(active);
        player2Dice.gameObject.SetActive(active);
    }

    public IEnumerator WaitForDiceAndStartRound(float duration, int p1Roll, int p2Roll)
    {
        yield return new WaitForSeconds(duration);
        NewRound(p1Roll, p2Roll);
    }

    private void Update()
    {
        //// For testing 
        //if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        //    NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }
}

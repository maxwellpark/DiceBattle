using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private NewRoundsUI _newRoundsUI;
    private ScoreUI _scoreUI;
    private BulletDisplayUI _bulletDisplayUI;

    public Player player;
    public PlayerShooting playerShooting;

    public EnemyPlayer enemy;
    public EnemyShooting enemyShooting;

    private DiceManager _diceManager;
    private BarrierManager _barrierManager;
    private MenuTransitionManager _menuTransitionManager;

    private int _currentRound;
    private int _playerRoundsWon;
    private int _enemyRoundsWon;
    public static bool inBattle;

    [SerializeField]
    private MenuTransitionData _battleEndTransitionData;

    public static event UnityAction onNewBattle;
    public static event UnityAction onNewRound;
    public static event UnityAction onRoundComplete;
    public static event UnityAction onBattleComplete;

    private void Awake()
    {
        _newRoundsUI = FindObjectOfType<NewRoundsUI>();
        _scoreUI = FindObjectOfType<ScoreUI>();
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
        _diceManager = FindObjectOfType<DiceManager>();
        _barrierManager = FindObjectOfType<BarrierManager>();
        _menuTransitionManager = FindObjectOfType<MenuTransitionManager>();

        RegisterEvents();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Init();
    }

    public void NewBattle()
    {
        Debug.Log("Setting up new battles...");
        _currentRound = 0;
        _playerRoundsWon = 0;
        _enemyRoundsWon = 0;
        _newRoundsUI.ShowNewGameUI(false);
        onNewBattle?.Invoke();
        NewRound();
    }

    public void NewRound()
    {
        var p1Roll = Random.Range(1, 7);
        var p2Roll = Random.Range(1, 7);
        _newRoundsUI.ShowNewRoundUI(false);
        StartCoroutine(WaitForDiceAndStartRound(p1Roll, p2Roll));
    }

    public void NewRound(int player1Roll, int player2Roll)
    {
        inBattle = true;
        _currentRound++;
        Debug.Log("Setting up new round...");
        Debug.Log("Player 1 roll = " + player1Roll);
        Debug.Log("Player 2 roll = " + player2Roll);

        _scoreUI.UpdateUI(_playerRoundsWon, _enemyRoundsWon, _currentRound);

        // Setup round based on dice roll values
        playerShooting.SetupShooting(player1Roll);
        enemyShooting.SetupShooting(player2Roll);

        // Subtract 6 from roll to get no. of barriers
        _barrierManager.SetupBarriers(6 - player1Roll, 6 - player2Roll);

        player.ResetSelf();
        enemy.ResetSelf();
        onNewRound?.Invoke();
        ToggleObjects(true);
    }

    public void RoundComplete()
    {
        inBattle = false;
        Debug.Log("Round " + _currentRound + " complete");
        playerShooting.ClearBullets();
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        _scoreUI.UpdateUI(_playerRoundsWon, _enemyRoundsWon, _currentRound);

        if (_playerRoundsWon >= 2)
        {
            BattleComplete(true);
        }
        else if (_enemyRoundsWon >= 2)
        {
            BattleComplete(false);
        }
        else
        {
            ToggleObjects(false);
            _newRoundsUI.ShowNewRoundUI(true);
        }
        onRoundComplete?.Invoke();
    }

    public void BattleComplete(bool player1Wins)
    {
        var text = player1Wins ? "Player 1 " : "Player 2 ";
        text += "wins the 3 round battle!";
        Debug.Log(text);
        _scoreUI.UpdateRoundText(text);
        _newRoundsUI.ShowNewGameUI(true);
        _currentRound = 0;
        onBattleComplete?.Invoke();
        _menuTransitionManager.Transition(_battleEndTransitionData);
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

        CountDown.onCountDownEnd += () =>
        {
            Debug.Log("Time limit reached. The round was a draw.");
            RoundComplete();
        };
        DestroyTimer.onDestroy += () => inBattle = true;
    }

    private void Init()
    {
        // Setup UI 
        _newRoundsUI.newGameBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newGameBtn.onClick.AddListener(NewBattle);
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
    }

    public IEnumerator WaitForDiceAndStartRound(int p1Roll, int p2Roll)
    {
        inBattle = false;
        _diceManager.CreateDice(p1Roll, p2Roll);
        while (!inBattle)
        {
            yield return null;
        }
        NewRound(p1Roll, p2Roll);
    }

    private void Update()
    {
        //// For testing 
        //if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        //    NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }
}

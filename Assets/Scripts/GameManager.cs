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

    public static int currentRound;
    public static int playerRoundsWon;
    public static int enemyRoundsWon;
    public static bool inBattle;

    [SerializeField]
    private MenuTransitionData _battleEndTransitionData;

    public static event UnityAction onNewBattle;
    public static event UnityAction onNewRound;
    public static event UnityAction onRoundComplete;
    public static event UnityAction onBattleComplete;
    public static event UnityAction<bool> onBattleCompleteFlag;

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
        currentRound = 0;
        playerRoundsWon = 0;
        enemyRoundsWon = 0;
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
        currentRound++;
        Debug.Log("Setting up new round...");
        Debug.Log("Player 1 roll = " + player1Roll);
        Debug.Log("Player 2 roll = " + player2Roll);

        _scoreUI.UpdateUI(playerRoundsWon, enemyRoundsWon, currentRound);

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
        Debug.Log("Round " + currentRound + " complete");
        playerShooting.ClearBullets();
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        _scoreUI.UpdateUI(playerRoundsWon, enemyRoundsWon, currentRound);

        if (playerRoundsWon >= 2)
        {
            BattleComplete(true);
        }
        else if (enemyRoundsWon >= 2)
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
        text += "wins the best of 3 battle!";
        Debug.Log(text);
        _scoreUI.UpdateRoundText(text);
        _newRoundsUI.ShowNewGameUI(true);
        currentRound = 0;
        onBattleComplete?.Invoke();
        onBattleCompleteFlag?.Invoke(player1Wins);
        _menuTransitionManager.Transition(_battleEndTransitionData);
    }

    private void RegisterEvents()
    {
        enemyShooting.onDeath += () =>
        {
            playerRoundsWon++;
            RoundComplete();
            _scoreUI.UpdateRoundTextForEndOfRound();
        };

        playerShooting.onDeath += () =>
        {
            enemyRoundsWon++;
            RoundComplete();
            _scoreUI.UpdateRoundTextForEndOfRound();
        };

        // Round is drawn if count down event fires
        CountDown.onCountDownEnd += () =>
        {
            RoundComplete();
            _scoreUI.UpdateRoundTextForEndOfRound(true);
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

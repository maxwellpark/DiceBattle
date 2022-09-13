using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
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
    private CharacterManager _characterManager;

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

    protected override void Awake()
    {
        base.Awake();
    }

    public void NewBattle()
    {
        Debug.Log("Setting up new battle...");
        currentRound = 0;
        playerRoundsWon = 0;
        enemyRoundsWon = 0;
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
        _scoreUI.ResetUI();
        _newRoundsUI.ShowNewBattleUI(false);
        currentRound = 0;
        onBattleComplete?.Invoke();
        ToggleObjects(false);
        _menuTransitionManager.Transition(_battleEndTransitionData);
    }

    private void RegisterBattleEvents()
    {
        enemyShooting.onDeath += OnEnemyDeath;
        playerShooting.onDeath += OnPlayerDeath;
    }

    private void UnRegisterBattleEvents()
    {
        enemyShooting.onDeath -= OnEnemyDeath;
        playerShooting.onDeath -= OnPlayerDeath;
    }

    private void OnEnemyDeath()
    {
        playerRoundsWon++;
        RoundComplete();
        _scoreUI.UpdateRoundTextForEndOfRound(true);
    }

    private void OnPlayerDeath()
    {
        enemyRoundsWon++;
        RoundComplete();
        _scoreUI.UpdateRoundTextForEndOfRound(false);
    }

    private void OnCountDownEnd()
    {
        // Round is drawn if count down event fires
        RoundComplete();
        _scoreUI.UpdateRoundTextForDraw();
    }

    private void OnDestroyTimerEnd()
    {
        inBattle = true;
    }

    private void Init()
    {
        // Setup UI 
        _newRoundsUI.newGameBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newGameBtn.onClick.AddListener(NewBattle);
        _newRoundsUI.newRoundBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newRoundBtn.onClick.AddListener(NewRound);
        _newRoundsUI.ShowNewBattleUI(false);
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

    private void FindReferences()
    {
        _newRoundsUI = FindObjectOfType<NewRoundsUI>();
        _scoreUI = FindObjectOfType<ScoreUI>();
        _bulletDisplayUI = FindObjectOfType<BulletDisplayUI>();

        _diceManager = FindObjectOfType<DiceManager>();
        _barrierManager = FindObjectOfType<BarrierManager>();
        _menuTransitionManager = FindObjectOfType<MenuTransitionManager>();
        _characterManager = FindObjectOfType<CharacterManager>();
    }

    private void FindPlayerReferences()
    {
        // Get player refs 
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
            playerShooting = playerObj.GetComponent<PlayerShooting>();
        }
        else
        {
            Debug.LogWarning("Player not found");
        }

        // Get enemy refs
        var enemyObj = GameObject.FindWithTag("Enemy");
        if (enemyObj != null)
        {
            enemy = enemyObj.GetComponent<EnemyPlayer>();
            enemyShooting = enemyObj.GetComponent<EnemyShooting>();
        }
        else
        {
            Debug.LogWarning("Enemy not found");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        if (scene.name == "BattleScene")
        {
            FindReferences();
            _characterManager.CreateChars();
            FindPlayerReferences();
            RegisterBattleEvents();
            player.Init();
            enemy.Init();
            Init();
            NewBattle();
        }
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
        // For testing 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            //NewBattle();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CountDown.onCountDownEnd += OnCountDownEnd;
        DestroyTimer.onDestroy += OnDestroyTimerEnd;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        CountDown.onCountDownEnd -= OnCountDownEnd;
        DestroyTimer.onDestroy -= OnDestroyTimerEnd;
    }
}

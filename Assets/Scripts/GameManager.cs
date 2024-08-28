using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private NewRoundsUI _newRoundsUI;
    private ScoreUI _scoreUI;
    private BulletDisplayUI _bulletDisplayUI;
    private HealthUI _healthUI;

    public Player player;
    public PlayerShooting playerShooting;

    public EnemyPlayer enemy;
    public EnemyShooting enemyShooting;

    private DiceManager _diceManager;
    private BarrierManager _barrierManager;
    private MenuTransitionManager _menuTransitionManager;
    private CharacterManager _characterManager;

    private CountDown _battleCountDown;
    private PreRoundCountDown _preRoundCountDown;

    public static int currentRound;
    public static int playerRoundsWon;
    public static int enemyRoundsWon;

    public static bool waitingForDice;
    public static bool inBattle;

    [SerializeField]
    private MenuTransitionData _battleEndTransitionData;

    public static event UnityAction onNewBattle;
    public static event UnityAction onNewRound;
    public static event UnityAction onPreRound;
    public static event UnityAction onRoundComplete;
    public static event UnityAction onBattleComplete;

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
        StartCoroutine(WaitForDiceThenPrepareRound(p1Roll, p2Roll));
    }

    public void PrepareRound(int player1Roll, int player2Roll)
    {
        inBattle = false;
        currentRound++;
        Debug.Log("Preparing new round...");
        Debug.Log("Player 1 roll = " + player1Roll);
        Debug.Log("Player 2 roll = " + player2Roll);

        _scoreUI.UpdateUI(playerRoundsWon, enemyRoundsWon, currentRound);

        // Setup round based on dice roll values
        playerShooting.SetupShooting(player1Roll, CharacterManager.p1CharData.bulletPrefab);
        enemyShooting.SetupShooting(player2Roll, CharacterManager.p2CharData.bulletPrefab);

        // Subtract 6 from roll to get no. of barriers
        _barrierManager.SetupBarriers(6 - player1Roll, 6 - player2Roll);

        player.PrepareForRound();
        enemy.PrepareForRound();

        playerShooting.CanShoot = false;
        enemyShooting.CanShoot = false;

        ToggleBackgroundObjs(true);
        onPreRound?.Invoke();
    }

    public void StartRound()
    {
        Debug.Log("Starting round " + currentRound);
        inBattle = true;

        player.StartRound();
        enemy.StartRound();

        playerShooting.CanShoot = true;
        enemyShooting.CanShoot = true;
        onNewRound?.Invoke();
    }

    public void RoundComplete()
    {
        inBattle = false;
        Debug.Log("Round " + currentRound + " complete");
        playerShooting.ClearBullets();
        TogglePlayers(false);
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
            ToggleBackgroundObjs(false);
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
        ToggleBackgroundObjs(false);
        _menuTransitionManager.Transition(_battleEndTransitionData);
    }

    private void RegisterBattleEvents()
    {
        enemyShooting.onDeath += OnEnemyDeath;
        playerShooting.onDeath += OnPlayerDeath;

        _battleCountDown.onCountDownEnd += OnBattleCountDownEnd;
        _preRoundCountDown.onPreCountDownEnd += OnPreRoundCountDownEnd;
    }

    private void UnRegisterBattleEvents()
    {
        enemyShooting.onDeath -= OnEnemyDeath;
        playerShooting.onDeath -= OnPlayerDeath;

        _battleCountDown.onCountDownEnd -= OnBattleCountDownEnd;
        _preRoundCountDown.onPreCountDownEnd -= OnPreRoundCountDownEnd;
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

    private void OnBattleCountDownEnd()
    {
        // Round is drawn if count down event fires
        RoundComplete();
        _scoreUI.UpdateRoundTextForDraw();
    }

    private void OnPreRoundCountDownEnd()
    {
        StartRound();
    }

    private void OnDestroyTimerEnd()
    {
        waitingForDice = false;
    }

    private void Init()
    {
        // Setup UI 
        _newRoundsUI.newGameBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newGameBtn.onClick.AddListener(NewBattle);
        _newRoundsUI.newRoundBtn.onClick.RemoveAllListeners();
        _newRoundsUI.newRoundBtn.onClick.AddListener(NewRound);
        _newRoundsUI.ShowNewBattleUI(false);
        ToggleBackgroundObjs(false);
        TogglePlayers(false);
    }

    private void ToggleBackgroundObjs(bool active)
    {
        player.grid.gameObject.SetActive(active);
        enemy.grid.gameObject.SetActive(active);
        if (!active)
            _bulletDisplayUI.ClearTexts();
        _healthUI.SetActive(active);
    }

    private void TogglePlayers(bool active)
    {
        player.gameObject.SetActive(active);
        enemy.gameObject.SetActive(active);
    }

    private void FindReferences()
    {
        _newRoundsUI = FindObjectOfType<NewRoundsUI>();
        _scoreUI = FindObjectOfType<ScoreUI>();
        _bulletDisplayUI = FindObjectOfType<BulletDisplayUI>();
        _healthUI = FindObjectOfType<HealthUI>();

        _diceManager = FindObjectOfType<DiceManager>();
        _barrierManager = FindObjectOfType<BarrierManager>();
        _menuTransitionManager = FindObjectOfType<MenuTransitionManager>();
        _characterManager = FindObjectOfType<CharacterManager>();

        var battleCd = GameObject.FindWithTag("BattleCountDown");
        if (battleCd != null)
            _battleCountDown = battleCd.GetComponent<CountDown>();

        var preRoundCd = GameObject.FindWithTag("PreRoundCountDown");
        if (preRoundCd != null)
            _preRoundCountDown = preRoundCd.GetComponent<PreRoundCountDown>();
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
            _healthUI.Init();
            _bulletDisplayUI.Init();
            Init();
            NewBattle();
        }
    }

    public IEnumerator WaitForDiceThenPrepareRound(int p1Roll, int p2Roll)
    {
        waitingForDice = true;
        _diceManager.CreateDice(p1Roll, p2Roll);
        while (waitingForDice)
        {
            yield return null;
        }
        PrepareRound(p1Roll, p2Roll);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DestroyTimer.onDestroy += OnDestroyTimerEnd;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        DestroyTimer.onDestroy -= OnDestroyTimerEnd;
    }

    private void OnDestroy()
    {
        //UnRegisterBattleEvents();
    }
}

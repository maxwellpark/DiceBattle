using UnityEngine;

public class BulletDisplayUI : Singleton<BulletDisplayUI>
{
    [SerializeField]
    private BulletDisplayText _player1Text;
    [SerializeField]
    private BulletDisplayText _player2Text;

    private PlayerShooting _playerShooting;
    private EnemyShooting _enemyShooting;

    protected override void Awake()
    {
        base.Awake();
        _playerShooting = FindObjectOfType<PlayerShooting>();
        _enemyShooting = FindObjectOfType<EnemyShooting>();
        RegisterEvents();
    }

    private void Start()
    {
        if (_player1Text == null)
        {
            var p1TextObj = GameObject.FindGameObjectWithTag("Player1BulletText");
            if (p1TextObj != null)
                _player1Text = p1TextObj.GetComponent<BulletDisplayText>();
        }

        if (_player2Text == null)
        {
            var p2TextObj = GameObject.FindGameObjectWithTag("Player2BulletText");
            if (p2TextObj != null)
                _player2Text = p2TextObj.GetComponent<BulletDisplayText>();
        }
    }

    private void UpdateText(BulletDisplayText text, PlayerShooting shooting, bool reloading = false)
    {
        if (shooting == null)
            return;

        text.UpdateText(shooting.magSize, shooting.shotsRemaining, reloading);
    }

    public void ToggleTexts(bool active)
    {
        _player1Text.gameObject.SetActive(active);
        _player2Text.gameObject.SetActive(active);
    }

    public void ClearTexts()
    {
        _player1Text.ClearText();
        _player2Text.ClearText();
    }

    private void RegisterEvents()
    {
        _playerShooting.onShoot += () =>
        {
            UpdateText(_player1Text, _playerShooting);
        };
        _playerShooting.onReloadStart += () =>
        {
            UpdateText(_player1Text, _playerShooting, true);
        };
        _playerShooting.onReloadEnd += () =>
        {
            UpdateText(_player1Text, _playerShooting);
        };
        _enemyShooting.onShoot += () =>
        {
            UpdateText(_player2Text, _enemyShooting);
        };
        _enemyShooting.onReloadStart += () =>
        {
            UpdateText(_player2Text, _enemyShooting, true);
        };
        _enemyShooting.onReloadEnd += () =>
        {
            UpdateText(_player2Text, _enemyShooting);
        };
        GameManager.onNewRound += OnNewRoundHandler;
        GameManager.onRoundComplete += OnRoundCompleteHandler;
        GameManager.onBattleComplete += OnBattleCompleteHandler;
    }

    private void OnNewRoundHandler()
    {
        UpdateText(_player1Text, _playerShooting);
        UpdateText(_player2Text, _enemyShooting);
        ToggleTexts(true);
    }

    private void OnRoundCompleteHandler()
    {
        ToggleTexts(false);
    }

    private void OnBattleCompleteHandler()
    {
        ToggleTexts(false);
    }

    private void UnRegisterEvents()
    {
        GameManager.onNewRound -= OnNewRoundHandler;
        GameManager.onRoundComplete -= OnRoundCompleteHandler;
        GameManager.onBattleComplete -= OnBattleCompleteHandler;
    }

    private void OnDestroy()
    {
        UnRegisterEvents();
    }
}

using UnityEngine;

public class BulletDisplayUI : MonoBehaviour
{
    [SerializeField]
    private BulletDisplayText _player1Text;
    [SerializeField]
    private BulletDisplayText _player2Text;

    private PlayerShooting _playerShooting;
    private EnemyShooting _enemyShooting;
    private GameManager _gameManager;

    private void Start()
    {
        _playerShooting = FindObjectOfType<PlayerShooting>();
        _enemyShooting = FindObjectOfType<EnemyShooting>();
        _gameManager = FindObjectOfType<GameManager>();
        RegisterEvents();
    }

    private void UpdateText(BulletDisplayText text, PlayerShooting shooting, bool reloading = false)
    {
        text.UpdateText(shooting.magSize, shooting.shotsRemaining, reloading);
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
        _gameManager.onNewRound += () =>
        {
            UpdateText(_player1Text, _playerShooting);
            UpdateText(_player2Text, _enemyShooting);
        };
    }
}


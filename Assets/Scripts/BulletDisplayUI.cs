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

    private void UpdateText(BulletDisplayText text, PlayerShooting shooting)
    {
        text.UpdateText(shooting.magSize, shooting.shotsRemaining);
    }

    private void RegisterEvents()
    {
        _playerShooting.onShoot += () =>
        {
            UpdateText(_player1Text, _playerShooting);
        };
        _playerShooting.onReload += () =>
        {
            UpdateText(_player1Text, _playerShooting);
        };
        _enemyShooting.onShoot += () =>
        {
            UpdateText(_player2Text, _enemyShooting);
        };
        _enemyShooting.onReload += () =>
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

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerShooting _playerShooting;
    private EnemyShooting _enemyShooting;
    private BarrierManager _barrierManager;

    private void Awake()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
            throw new System.Exception("Player not found");

        _playerShooting = playerObj.GetComponent<PlayerShooting>();

        var enemyObj = GameObject.FindWithTag("Enemy");
        if (enemyObj == null)
            throw new System.Exception("Enemy not found");

        _enemyShooting = enemyObj.GetComponent<EnemyShooting>();

        var barrierManagerObj = GameObject.FindWithTag("BarrierManager");
        _barrierManager = barrierManagerObj.GetComponent<BarrierManager>();
    }

    private void Start()
    {
        // For testing
        var playerRoll = Random.Range(1, 7);
        var enemyRoll = Random.Range(1, 7);

        // Setup game based on dice roll values
        _playerShooting.SetupShooting(playerRoll);
        _enemyShooting.SetupShooting(enemyRoll);
        _barrierManager.SetupBarriers(6 - playerRoll, 6 - enemyRoll);
    }
}

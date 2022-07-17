using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerShooting _playerShooting;
    private EnemyShooting _enemyShooting;

    public float initialPlayerBullets;

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
    }

    private void Start()
    {
        // For testing
        var playerRoll = Random.Range(1, 7);
        var enemyRoll = Random.Range(1, 7);

        _playerShooting.SetupShooting(playerRoll);
        _enemyShooting.SetupShooting(enemyRoll);

        Debug.Log("Roll"+playerRoll);
        initialPlayerBullets=playerRoll;



    }
}

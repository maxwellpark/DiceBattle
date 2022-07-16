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
        _barrierManager = GameObject.FindObjectOfType<BarrierManager>();
    }

    private void Start()
    {
        // For testing
        NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }

    public void NewGame(int player1Roll, int player2Roll)
    {
        Debug.Log("Setting up new game...");
        Debug.Log("Player 1 roll = " + player1Roll);
        Debug.Log("Player 2 roll = " + player2Roll);

        // Setup game based on dice roll values
        _playerShooting.SetupShooting(player1Roll);
        _enemyShooting.SetupShooting(player2Roll);

        // Subtract 6 from roll to get no. of barriers
        _barrierManager.SetupBarriers(6 - player1Roll, 6 - player2Roll);
    }

    private void Update()
    {
        // For testing 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
            NewGame(Random.Range(1, 7), Random.Range(1, 7));
    }
}

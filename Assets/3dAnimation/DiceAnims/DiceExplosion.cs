using UnityEngine;

public class DiceExplosion : MonoBehaviour
{
    public GameObject explodingDice;
    public GameObject previousDice;

    public void AddDiceExplosion()
    {
        Instantiate(explodingDice, transform.position, Quaternion.identity);
        Destroy(previousDice);
    }
}

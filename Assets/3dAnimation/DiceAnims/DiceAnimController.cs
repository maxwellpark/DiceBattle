using UnityEngine;

public class DiceAnimController : MonoBehaviour
{
    public Animator diceAnim;
    public int diceValue;
    public int state;
    public static float animWaitTimeInSeconds = 2f;

    void Awake()
    {
        diceAnim = GetComponent<Animator>();
    }

    public int RollDice(out float animDuration)
    {
        diceValue = Random.Range(1, 7);

        var info = diceAnim.GetCurrentAnimatorStateInfo(state);
        animDuration = info.length;
        return diceValue;
    }

    void Update()
    {
        SetAnimState(diceValue);
    }

    void SetAnimState(int value)
    {
        state = value;
        diceAnim.SetInteger("diceNumber", state);
    }
}

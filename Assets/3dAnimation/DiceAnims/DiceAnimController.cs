using UnityEngine;

public class DiceAnimController : MonoBehaviour
{
    public Animator diceAnim;
    public int diceValue;
    public int state;
    public static float animWaitTimeInSeconds = 2f;
    private DestroyTimer _destroyTimer;

    void Awake()
    {
        diceAnim = GetComponent<Animator>();
        //_destroyTimer = GetComponent<DestroyTimer>();
        //_destroyTimer.onDestroy += () => Destroy(gameObject);
    }

    public int RollDice(out float animDuration)
    {
        diceValue = Random.Range(1, 7);

        if (diceAnim == null)
            throw new System.Exception("Dice Animator was null.");

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

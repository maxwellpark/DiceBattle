using UnityEngine;

public class DiceAnimController : MonoBehaviour
{
    public Animator diceAnim;
    public int diceValue;
    public int state;
    public static float animWaitTimeInSeconds = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        diceAnim = GetComponent<Animator>();
    }

    public int RollDice(out float animDuration)
    {
        diceValue = Random.Range(1, 7);
        SetAnimState(diceValue);
        var info = diceAnim.GetCurrentAnimatorStateInfo(state);
        animDuration = info.length;
        return diceValue;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetAnimState(int value)
    {
        state = value;
        diceAnim.SetInteger("diceNumber", state);
    }
}

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

    public float AnimateRollDice()
    {
        if (diceAnim == null)
        {
            // Todo: Stop destroying the Dice Anim or re-create it.
            //throw new System.Exception("Dice Animator was null.");
            return -1f;
        }

        var info = diceAnim.GetCurrentAnimatorStateInfo(state);
        return info.length;
    }

    void Update()
    {
        SetAnimState(diceValue);
    }

    void SetAnimState(int value)
    {
        state = value;
        if (diceAnim == null)
            return;
        diceAnim.SetInteger("diceNumber", state);
    }
}

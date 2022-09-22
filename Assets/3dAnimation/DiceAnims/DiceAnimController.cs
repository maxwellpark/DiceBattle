using UnityEngine;

public class DiceAnimController : MonoBehaviour
{
    public Animator diceAnim;
    public int diceValue;
    public int state;

    void Awake()
    {
        diceAnim = GetComponent<Animator>();
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

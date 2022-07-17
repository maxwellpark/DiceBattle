using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAnimController : MonoBehaviour
{

    public Animator _diceAnim;
    public int _diceValue;
    // Start is called before the first frame update
    void Start()
    {
        _diceAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (_diceValue == 1)
        {
            _diceAnim.SetInteger("diceNumber", 1);
        }

        if (_diceValue == 2)
        {
            _diceAnim.SetInteger("diceNumber", 2);
        }

        if (_diceValue == 3)
        {
            _diceAnim.SetInteger("diceNumber", 3);
        }

        if (_diceValue == 4)
        {
            _diceAnim.SetInteger("diceNumber", 4);
        }

        if (_diceValue == 5)
        {
            _diceAnim.SetInteger("diceNumber", 5);
        }

        if (_diceValue == 6)
        {
            _diceAnim.SetInteger("diceNumber", 6);
        }


    }
}

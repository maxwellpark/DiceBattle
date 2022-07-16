using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{

    public float myFinalCountdown=30;
    public Text countDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myFinalCountdown-=0.1f* Time.deltaTime;
        countDisplay.text= "Time"+ myFinalCountdown;
        
    }
}

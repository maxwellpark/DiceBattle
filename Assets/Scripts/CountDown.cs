using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{

    public float myFinalCountdown=10;
    public float myTest;
    public PlayerShooting _playerShooting;

    public Text countDisplay;

     public Text countAmunitionPlayer1;
      public Text countAmunitionPlayer2;
    // Start is called before the first frame update
    void Start()
    {
        _playerShooting.GetComponent<PlayerShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        myFinalCountdown-=1f* Time.deltaTime;
        countDisplay.text= "Time"+ myFinalCountdown;

        countAmunitionPlayer1.text=_playerShooting._shotsRemaining.ToString();

     
      
       

       
        
        
    }
}

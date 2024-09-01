using UnityEngine;

public class AnimatorControllerForTrail : MonoBehaviour
{
    private Player _myPlayer;
    private PlayerShooting _myShooting;
    private PlayerVelocity _pVelocity;

    public Animator _myAnim;
  
    // Start is called before the first frame update
    void Start()
    {
        _myPlayer = GetComponentInParent<Player>();
        _myShooting = GetComponentInParent<PlayerShooting>();
        _pVelocity = GetComponentInParent<PlayerVelocity>();    
    }

    // Update is called once per frame
    void Update()
    {
   


        if (!_myPlayer.directionLocked)
        {
            if (_pVelocity.velocityX==0f) 
            { 
                _myAnim.SetBool("goingDown", false);
                _myAnim.SetBool("goingUps", false); 
            }



            if (_pVelocity.velocityX>0)
            {
                _myAnim.SetBool("goingUps", true);
                _myAnim.SetBool("goingDown", false);
            }
         
                
            if (_pVelocity.velocityX < 0)
            {

                _myAnim.SetBool("goingDown", true);
                _myAnim.SetBool("goingUps", false);

            }

            if (_pVelocity.velocityZ == 0f)
            {
                _myAnim.SetBool("goingRight", false);
                _myAnim.SetBool("goingLeft", false);
            }


            if (_pVelocity.velocityZ>0)
            {
                _myAnim.SetBool("goingRight", true);
                _myAnim.SetBool("goingLeft", false);
            }


            if (_pVelocity.velocityZ < 0)
            {

                _myAnim.SetBool("goingLeft", true);
                _myAnim.SetBool("goingRight", false);
            }
          
        }

        if (_myShooting.CanShoot && _myShooting.shotsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _myAnim.SetTrigger("shooting");
        }
    }
}

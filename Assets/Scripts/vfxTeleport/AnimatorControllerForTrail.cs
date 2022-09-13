using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerForTrail : MonoBehaviour
{

    public Animator _myAnim;
    public float realBlendy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(realBlendy<=0)
        {
            realBlendy=0;
        }

         
        if(Input.GetKey(KeyCode.W))
        {
           
            _myAnim.SetBool("goingUps",true);    
        }else if(!Input.GetKey(KeyCode.W))
        {_myAnim.SetBool("goingUps",false);   }

         
        if(Input.GetKey(KeyCode.S))
        {
           
            _myAnim.SetBool("goingDown",true);    
        }else if(!Input.GetKey(KeyCode.D))
        {_myAnim.SetBool("goingDown",false);   }
        


       
        if(Input.GetKey(KeyCode.D))
        {
           
            _myAnim.SetBool("goingRight",true);    
        }else if(!Input.GetKey(KeyCode.D))
        {_myAnim.SetBool("goingRight",false);   }
       

        if(Input.GetKey(KeyCode.A))
        {
           
            _myAnim.SetBool("goingLeft",true);    
        }else if(!Input.GetKey(KeyCode.A))
        {_myAnim.SetBool("goingLeft",false);   }
        
        
    }
}

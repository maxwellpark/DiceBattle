using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierShaderControll : MonoBehaviour
{
    // Start is called before the first frame update
    public Material mat;

    public MeshRenderer myRendere;
    public int barrierState;

    private void Start()
    {
        myRendere.GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        //one hit
        if (barrierState == 0)
        {

            myRendere.material.SetFloat("_Enabledistortion", 0f);
            myRendere.material.SetFloat("_Globalopacity", 0.8f);
            myRendere.material.SetColor("_Maincolor", Color.cyan);



        }




        //one hit
        if (barrierState == 1)
        {
            myRendere.material.SetFloat("_Enabledistortion", 0.2f - mat.GetFloat("_Enabledistortion"));
            myRendere.material.SetFloat("_Globalopacity", 0.7f);
            myRendere.material.SetFloat("_Distortionspeed", 0.5f);

        }

        //one hit
        if (barrierState == 2)
        {
            myRendere.material.SetFloat("_Enabledistortion", 0.4f - mat.GetFloat("_Enabledistortion"));
            myRendere.material.SetFloat("_Globalopacity", 0.6f);
            myRendere.material.SetColor("_Maincolor", Color.red);
            myRendere.material.SetFloat("_Distortionspeed", 1f);

        }

        //almost destroyed
        if (barrierState == 3)
        {
            myRendere.material.SetFloat("_Enabledistortion", 0.8f - mat.GetFloat("_Enabledistortion"));
            myRendere.material.SetFloat("_Globalopacity", 0.5f);
            myRendere.material.SetFloat("_Distortionspeed", 2f);
        }


    }

}

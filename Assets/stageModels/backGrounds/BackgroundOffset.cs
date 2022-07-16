using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    public float scroolX;
    public float scroolY;
    

    // Update is called once per frame
    void Update()
    {

        float offsetX=Time.time*scroolX;
        float offsetY=Time.time*scroolY;
        GetComponent<Renderer>().material.mainTextureOffset=new Vector2(offsetX,offsetY);
    }
}

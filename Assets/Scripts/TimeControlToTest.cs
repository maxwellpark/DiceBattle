using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlToTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        { Time.timeScale = 0.2f; }

        if (Input.GetKey(KeyCode.Alpha9))
        { Time.timeScale = 1f; }
    }
}

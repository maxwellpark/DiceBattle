using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public float nextSceneCountdown;
    public int currentScene;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.Space))
        {
            LoadBattleAI("Tutorial Scene");
            currentScene = 1;
        }

        DontDestroyOnLoad(this.gameObject); 


        
        if(Input.GetKeyUp(KeyCode.Space)&& currentScene==1)
        {
            LoadBattleAI("ReloadingTestScene");
           
        }



        
    }



        public void LoadBattleAI(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }




}

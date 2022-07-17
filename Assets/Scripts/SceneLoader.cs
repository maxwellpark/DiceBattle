using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string tutorialSceneName;
    public string titleSceneName;
    public string arenaSceneName;

    public float nextSceneCountdown;
    public int currentScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var scene = SceneManager.GetActiveScene();
            if (scene.name == tutorialSceneName)
                SceneManager.LoadScene(titleSceneName);
            else if (scene.name == titleSceneName)
                SceneManager.LoadScene(arenaSceneName);
        }

        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    LoadBattleAI("Tutorial Scene");
        //    currentScene = 1;
        //}


        
        //if(Input.GetKeyUp(KeyCode.Space)&& currentScene==1)
        //{
        //    LoadBattleAI("ReloadingTestScene");
           
        //}



        
    }



        public void LoadBattleAI(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }




}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string tutorialSceneName;
    public string titleSceneName;
    public string arenaSceneName;

    public float nextSceneCountdown;
    public int currentScene;

    private MusicManager _musicManager;

    private void Awake()
    {
        _musicManager = FindObjectOfType<MusicManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _musicManager.PlayMusic(MusicState.Menu);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var scene = SceneManager.GetActiveScene();

            if (scene.name == tutorialSceneName)
            {
                SceneManager.LoadScene(titleSceneName);
            }
            else if (scene.name == titleSceneName)
            {
                SceneManager.LoadScene(arenaSceneName);
                _musicManager.PlayMusic(MusicState.Battle);
            }
            else
            {
                Debug.LogWarning("Current scene name not recognised");
            }
        }
    }

    public void LoadScene(string scenename)
    {
        Debug.Log("Scene name to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }
}

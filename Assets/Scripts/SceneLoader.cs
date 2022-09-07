using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public string tutorialSceneName;
    public string titleSceneName;
    public string arenaSceneName;

    public float nextSceneCountdown;
    public int currentScene;

    private MusicManager _musicManager;

    protected override void Awake()
    {
        base.Awake();
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

            if (scene.name == arenaSceneName)
                return;

            if (scene.name == tutorialSceneName)
            {
                LoadScene(titleSceneName);
            }
            else if (scene.name == titleSceneName)
            {
                LoadScene(arenaSceneName);
                _musicManager.PlayMusic(MusicState.Battle);
            }
            else
            {
                Debug.LogWarning("Current scene name not recognised");
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Scene name to load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}

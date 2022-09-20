using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionManager : Singleton<MenuTransitionManager>
{
    private MusicManager _musicManager;
    private string _previousSceneName;

    protected override void Awake()
    {
        base.Awake();
        _previousSceneName = SceneManager.GetActiveScene().name;
        _musicManager = FindObjectOfType<MusicManager>();
        DontDestroyOnLoad(gameObject);
    }

    public void Transition(MenuTransitionData data)
    {
        Transition(data.sceneName, data.music);
    }

    public void Transition(string sceneName, MusicState music)
    {
        _previousSceneName = SceneManager.GetActiveScene().name;

        if (!string.IsNullOrWhiteSpace(sceneName))
            SceneManager.LoadScene(sceneName);

        if (music != MusicState.None)
            _musicManager.PlayMusic(music);
    }

    public void ToPreviousScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == _previousSceneName)
        {
            Debug.LogWarning("Cannot go to previous scene as current scene matches previous scene");
            return;
        }
        var music = _musicManager.GetStateByScene(_previousSceneName);
        Transition(_previousSceneName, music);
    }
}

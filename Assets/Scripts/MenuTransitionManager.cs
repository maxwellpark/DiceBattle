using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionManager : Singleton<MenuTransitionManager>
{
    private MusicManager _musicManager;
    private Scene _previousScene;

    protected override void Awake()
    {
        base.Awake();
        _previousScene = SceneManager.GetActiveScene();
        _musicManager = FindObjectOfType<MusicManager>();
        DontDestroyOnLoad(gameObject);
    }

    public void Transition(MenuTransitionData data)
    {
        Transition(data.sceneName, data.music);
    }

    public void Transition(string sceneName, MusicState music)
    {
        _previousScene = SceneManager.GetActiveScene();

        if (!string.IsNullOrWhiteSpace(sceneName))
            SceneManager.LoadScene(sceneName);

        if (music != MusicState.None)
            _musicManager.PlayMusic(music);
    }

    public void ToPreviousScene()
    {
        var currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == _previousScene.name)
        {
            Debug.LogWarning("Cannot go to previous scene as current scene matches previous scene");
            return;
        }
        var music = _musicManager.GetStateByScene(_previousScene.name);
        Transition(_previousScene.name, music);
    }
}

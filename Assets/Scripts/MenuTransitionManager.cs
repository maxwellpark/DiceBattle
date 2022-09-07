using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionManager : Singleton<MenuTransitionManager>
{
    private MusicManager _musicManager;

    protected override void Awake()
    {
        base.Awake();
        _musicManager = FindObjectOfType<MusicManager>();
        DontDestroyOnLoad(gameObject);
    }

    public void Transition(MenuTransitionData data)
    {
        if (!string.IsNullOrWhiteSpace(data.sceneName))
            SceneManager.LoadScene(data.sceneName);

        if (data.music != MusicState.None)
            _musicManager.PlayMusic(data.music);
    }
}

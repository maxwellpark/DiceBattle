using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransitionManager : MonoBehaviour
{
    private MusicManager _musicManager;

    private void Awake()
    {
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

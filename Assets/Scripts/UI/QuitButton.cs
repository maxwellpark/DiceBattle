using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    private void Start()
    {
#if UNITY_WEBGL
        Destroy(gameObject);
#endif
        _quitButton = GetComponent<Button>();
        _quitButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        if (Application.isPlaying)
        {
            Application.Quit();
        }
#endif
    }
}

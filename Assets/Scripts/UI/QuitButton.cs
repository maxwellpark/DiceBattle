using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    // Start is called before the first frame update
    void Start()
    {
        _quitButton = GetComponent<Button>();
        _quitButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Debug.Log("Quitting game...");
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (Application.isPlaying)
        {
            Application.Quit();
        }
        else
        {
            Debug.LogError("Quit mode not supported");
        }
    }
}

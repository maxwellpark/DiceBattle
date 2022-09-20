using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button _button;
    private MenuTransitionManager _transManager;

    private void Start()
    {
        _transManager = FindObjectOfType<MenuTransitionManager>();
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        _transManager.ToPreviousScene();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _container;

    [SerializeField]
    private GameObject _tutorial;

    [SerializeField]
    private Button _tutorialBtn;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialBtn.onClick.RemoveAllListeners();
        _tutorialBtn.onClick.AddListener(() => ShowTutorial(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_container.activeSelf)
            {
                ShowPauseMenu(!_container.activeSelf);
            }
            else if (_tutorial.activeSelf)
            {
                ShowTutorial(false);
            }
            else
            {
                ShowPauseMenu(false);
            }
        }
    }

    private void ShowPauseMenu(bool active)
    {
        _container.SetActive(active);
        _tutorial.SetActive(false);
        Time.timeScale = active ? 0 : 1;
    }

    private void ShowTutorial(bool active)
    {
        _container.SetActive(!active);
        _tutorial.SetActive(active);
    }
}

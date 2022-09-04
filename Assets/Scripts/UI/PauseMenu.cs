using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _container;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowPauseMenu(!_container.activeSelf);
    }

    private void ShowPauseMenu(bool active)
    {
        _container.SetActive(active);
        Time.timeScale = active ? 0 : 1;
    }
}

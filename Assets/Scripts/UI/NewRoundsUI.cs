using UnityEngine;
using UnityEngine.UI;

public class NewRoundsUI : MonoBehaviour
{
    public GameObject newGameContainer;
    public GameObject newRoundContainer;

    public Button newGameBtn;
    public Button newRoundBtn;

    private void Awake()
    {
        newGameContainer.SetActive(false);
        newRoundContainer.SetActive(false);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(newGameContainer);
        DontDestroyOnLoad(newRoundContainer);
        DontDestroyOnLoad(newGameBtn);
        DontDestroyOnLoad(newRoundBtn);
    }

    public void ShowNewBattleUI(bool active)
    {
        newGameContainer.SetActive(active);
    }

    public void ShowNewRoundUI(bool active)
    {
        newRoundContainer.SetActive(active);
    }
}

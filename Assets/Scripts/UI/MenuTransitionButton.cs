using UnityEngine;
using UnityEngine.UI;

public class MenuTransitionButton : MonoBehaviour
{
    [SerializeField]
    private MenuTransitionData _data;

    private MenuTransitionManager _manager;
    private Button _button;

    private void Awake()
    {
        _manager = FindObjectOfType<MenuTransitionManager>();
        _button = GetComponent<Button>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _manager.Transition(_data));
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectUI : MonoBehaviour
{
    [SerializeField]
    private CharacterDataContainer _container;
    [SerializeField]
    private GameObject _btnPrefab;

    private CharacterManager _charManager;

    public void CreateButtons()
    {
        foreach (var @char in _container.chars)
        {
            var obj = Instantiate(_btnPrefab, transform);
            var text = obj.GetComponentInChildren<TMP_Text>();
            text.text = @char.charName;

            var btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => SelectChar(@char));
        }
    }

    private void SelectChar(CharacterData data)
    {
        _charManager.p1CharData = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        _charManager = FindObjectOfType<CharacterManager>();
        CreateButtons();
    }
}

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
    private MenuTransitionManager _menuTransManager;

    [SerializeField]
    private MenuTransitionData _transData;

    public void CreateButtons()
    {
        foreach (var @char in _container.chars)
        {
            var obj = Instantiate(_btnPrefab, transform);
            var text = obj.GetComponentInChildren<TMP_Text>();
            //text.text = @char.charName;
            text.text = @char.prefab.name;

            var btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => SelectChar(@char));
        }
    }

    private void SelectChar(CharacterData data)
    {
        _charManager.p1CharData = data;
        _menuTransManager.Transition(_transData);

        // Remove if implementing human P2
        SelectEnemyChar();
    }

    // Start is called before the first frame update
    void Start()
    {
        _charManager = FindObjectOfType<CharacterManager>();
        _menuTransManager = FindObjectOfType<MenuTransitionManager>();
        CreateButtons();
    }

    private void SelectEnemyChar()
    {
        CharacterData enemyChar = default;

        foreach (var @char in _container.chars)
        {
            if (@char != _charManager.p1CharData)
            {
                enemyChar = @char;
                break;
            }
        }

        if (enemyChar == null)
            Debug.LogError("Could not set enemy char data");

        _charManager.p2CharData = enemyChar;
    }
}

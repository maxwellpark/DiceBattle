using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharSelectUI : MonoBehaviour
{
    [SerializeField]
    private CharacterDataContainer _playerCharContainer;
    [SerializeField]
    private EnemyCharacterDataContainer _enemyCharContainer;
    [SerializeField]
    private GameObject _btnPrefab;

    private CharacterManager _charManager;
    private MenuTransitionManager _menuTransManager;

    [SerializeField]
    private MenuTransitionData _transData;

    public void CreateButtons()
    {
        foreach (var @char in _playerCharContainer.chars)
        {
            var obj = Instantiate(_btnPrefab, transform);
            var text = obj.GetComponentInChildren<TMP_Text>();
            //text.text = @char.charName; // Use custom name if set 
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

        // Pick enemy once player has chosen theirs to exclude theirs
        // Remove if mode is 2 player 
        SelectEnemyChar();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _charManager = FindObjectOfType<CharacterManager>();
        _menuTransManager = FindObjectOfType<MenuTransitionManager>();
        CreateButtons();
    }

    private void SelectEnemyChar()
    {
        var availableEnemyChars = new List<CharacterData>();

        foreach (var @char in _enemyCharContainer.chars)
        {
            // Don't select the same char as the player
            if (@char.prefab.name != _charManager.p1CharData.prefab.name)
            {
                availableEnemyChars.Add(@char);
            }
        }

        if (availableEnemyChars.Count <= 0)
            Debug.LogError("Could not set enemy char data. No chars available.");

        var index = Random.Range(0, availableEnemyChars.Count);
        var enemyChar = availableEnemyChars[index];

        if (enemyChar == null)
            throw new Exception("Could not select enemy char data from available list.");

        _charManager.p2CharData = enemyChar;
    }
}

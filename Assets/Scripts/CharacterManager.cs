using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : Singleton<CharacterManager>
{
    public static CharacterData p1CharData;
    public static CharacterData p2CharData;

    [SerializeField]
    private EnemyCharacterDataContainer _enemyCharContainer;

    [SerializeField]
    private MenuTransitionData _transData;

    public void CreateChars()
    {
        CreateP1Char();
        CreateP2Char();
    }

    public GameObject CreateP1Char()
    {
        var obj = Instantiate(p1CharData.prefab);
        return obj;
    }

    public GameObject CreateP2Char()
    {
        var obj = Instantiate(p2CharData.prefab);
        return obj;
    }

    public void SelectPlayerChar(CharacterData @char)
    {
        p1CharData = @char;
        MenuTransitionManager.instance.Transition(_transData);

        // Pick enemy once player has chosen theirs to exclude theirs
        SelectEnemyChar();
    }

    private void SelectEnemyChar()
    {
        var availableEnemyChars = new List<CharacterData>();

        foreach (var @char in _enemyCharContainer.chars)
        {
            // Don't select the same char as the player
            if (@char.prefab.name != p1CharData.prefab.name)
            {
                availableEnemyChars.Add(@char);
            }
        }

        if (availableEnemyChars.Count <= 0)
            Debug.LogError("Could not set enemy char data. No chars available.");

        var index = Random.Range(0, availableEnemyChars.Count);
        var enemyChar = availableEnemyChars[index];

#pragma warning disable IDE0270 // Use coalesce expression
        if (enemyChar == null)
            throw new Exception("Could not select enemy char data from available list.");
#pragma warning restore IDE0270 // Use coalesce expression

        p2CharData = enemyChar;
    }
}

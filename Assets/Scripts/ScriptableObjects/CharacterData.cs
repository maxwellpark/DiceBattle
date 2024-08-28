using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string charName;
    public GameObject prefab;
    public GameObject bulletPrefab;

    private void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(charName) && prefab != null)
        {
            charName = prefab.name;
        }
    }
}

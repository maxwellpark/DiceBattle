using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataContainer", menuName = "ScriptableObjects/CharacterDataContainer", order = 1)]
public class CharacterDataContainer : ScriptableObject
{
    public CharacterData[] chars;
}

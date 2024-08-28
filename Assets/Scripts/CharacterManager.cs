using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public static CharacterData p1CharData;
    public static CharacterData p2CharData;

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
}

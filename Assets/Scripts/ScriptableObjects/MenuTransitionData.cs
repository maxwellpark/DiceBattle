using UnityEngine;

[CreateAssetMenu(fileName = "MenuTransitionData", menuName = "ScriptableObjects/MenuTransitionData", order = 1)]
public class MenuTransitionData : ScriptableObject
{
    public string sceneName;
    public MusicState music;
}

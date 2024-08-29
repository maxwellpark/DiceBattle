using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuTransitionData", menuName = "ScriptableObjects/MenuTransitionData", order = 1)]
public class MenuTransitionData : ScriptableObject
{
    public string sceneName;
    public string[] sceneNames;
    public MusicState music;

    private void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(sceneName) && sceneNames.Any())
        {
            sceneName = sceneNames[0];
        }
    }
}

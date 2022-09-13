using UnityEngine;

[CreateAssetMenu(fileName = "CellSequenceContainer", menuName = "ScriptableObjects/CellSequenceContainer", order = 1)]
public class CellSequenceContainer : ScriptableObject
{
    public CellSequence[] sequences;
}

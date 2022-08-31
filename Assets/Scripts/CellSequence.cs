using UnityEngine;

[CreateAssetMenu(fileName = "CellSequence", menuName = "ScriptableObjects/CellSequence", order = 1)]
public class CellSequence : ScriptableObject
{
    public Cell[] cells;
}
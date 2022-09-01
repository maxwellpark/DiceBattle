using System;
using UnityEngine;

[Serializable]
public struct CoordPair
{
    public float x;
    public float y;
}

[CreateAssetMenu(fileName = "CellSequence", menuName = "ScriptableObjects/CellSequence", order = 1)]
public class CellSequence : ScriptableObject
{
    public Cell[] cells;
    public CoordPair[] coords;
}
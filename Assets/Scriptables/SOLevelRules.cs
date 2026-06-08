using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Rules Data",menuName = "Scriptables/Level Rule Data")]
public class SOLevelRules : ScriptableObject
{
    [Range(1,6)]public int canonSlots;
    public List<Row> rows = new List<Row>();
    public List<CanonRowData> canons = new List<CanonRowData>();

    public int levelHeight => rows.Count;
    public int levelBlocks = 120;
}
[System.Serializable]
public class Row
{
    public BlockType[] blocks;
    [Range(1,4)]public int layerLevel = 1;
}
[System.Serializable]
public class CanonRowData
{
    public CanonData[] canonDatas;
}

[System.Serializable]
public class CanonData
{
    public BlockType canonType;
    public bool isMistery;
    public int canonAmmo;
}
public enum BlockType
{
    None,
    Yellow,
    Red,
    Blue,
    Green,
    Orange
}


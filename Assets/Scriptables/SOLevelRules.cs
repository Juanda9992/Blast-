using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Rules Data",menuName = "Scriptables/Level Rule Data")]
public class SOLevelRules : ScriptableObject
{
    public Color[] levelColors;
    public int canonSlots;
    public List<Row> rows = new List<Row>();
    public List<CanonRowData> canons = new List<CanonRowData>();

    public int levelHeight => rows.Count;
    public int levelBlocks = 120;

    [SerializeField] private Row testingRow;
    [ContextMenu("Add row")]
    private void InsertRow()
    {
        rows.Add(testingRow);
    }
}
[System.Serializable]
public class Row
{
    public BlockType[] blocks;
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
    public int canonAmmo;
}
public enum BlockType
{
    None,
    Yellow,
    Red
}


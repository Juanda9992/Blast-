
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level Rules Data",menuName = "Scriptables/Level Rule Data")]
public class SOLevelRules : ScriptableObject
{
    public Color[] levelColors;
    public List<Row> rows = new List<Row>();


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

public enum BlockType
{
    None,
    Yellow,
    Red
}


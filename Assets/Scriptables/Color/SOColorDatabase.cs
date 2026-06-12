using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorDatanase",menuName = "Scriptables/Color/Color Database")]
public class SOColorDatabase : ScriptableObject
{
    public List<ColorData> allColors;

    public Color GetColorByBlockType(BlockType blockType)
    {
        return allColors.Find(x=>x.blockType == blockType).color;
    }
    [System.Serializable]
    public class ColorData
    {
        public BlockType blockType;
        public Color color;
    }
}

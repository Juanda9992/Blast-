using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    [SerializeField] private SOColorDatabase colorDatabase;
    void Awake()
    {
        instance = this;
    }

    public Color GetColorByBlockType(BlockType blockType)
    {
        return colorDatabase.GetColorByBlockType(blockType);
    }
}

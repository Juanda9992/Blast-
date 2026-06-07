using DG.Tweening;
using UnityEngine;

public class 
ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private BlockType blockType;

    [SerializeField] private Renderer _render;

    [SerializeField] private Rigidbody rb;
    public Vector2Int blockCoordinates;
    private int blockHeight;
    public void SetUpBlock(BlockType type, Vector2Int coordinates,int doubleLayer)
    {
        blockCoordinates = coordinates;
        blockType = type;
        Color color = new Color(0,0,0);
        switch(blockType)
        {
            case BlockType.None:
                gameObject.SetActive(false);
                break;
            case BlockType.Yellow:
                color = Color.yellow;
                break;
            case BlockType.Red:
                color = Color.red;
                break;
            case BlockType.Blue:
                color = Color.blue;
                break;
            case BlockType.Green:
                color = Color.green;
                break;
            case BlockType.Orange:
                color = new Color(1,0.6f,0,1);
                break;
        }
        _render.material.color = color;
        blockHeight = doubleLayer;
    }

    public void UpdateBlockPos()
    {
        transform.DOLocalMoveZ(blockCoordinates.y,0.2f);
    }

    public BlockType GetBlockType()
    {
        return blockType;
    }
    public int GetObstacleHeight()
    {
        return blockHeight;
    }
}

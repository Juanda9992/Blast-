using UnityEngine;

public class 
ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private BlockType blockType;

    [SerializeField] private Renderer _render;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2Int blockCoordinates;
    public void SetUpBlock(BlockType type, Vector2Int coordinates)
    {
        blockCoordinates = coordinates;
        blockType = type;
        Color color = new Color(0,0,0);
        switch(blockType)
        {
            case BlockType.Yellow:
                color = Color.yellow;
                break;
            case BlockType.Red:
                color = Color.red;
                break;
        }
        _render.material.color = color;
    }
    void FixedUpdate()
    {
        //rb.velocity = new Vector3(0,0,-5);
    }

    public BlockType GetBlockType()
    {
        return blockType;
    }

    public Vector2Int GetBlockCoordinates()
    {
        return blockCoordinates;
    }
}

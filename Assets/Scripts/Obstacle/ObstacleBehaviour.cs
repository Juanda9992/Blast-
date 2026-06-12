using System.Collections;
using DG.Tweening;
using UnityEngine;

public class 
ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private BlockType blockType;

    [SerializeField] private Renderer _render;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float destroyTime;
    public Vector2Int blockCoordinates;
    private int blockHeight;
    public bool isTargeted = false;
    public void SetUpBlock(BlockType type, Vector2Int coordinates,int doubleLayer)
    {
        blockCoordinates = coordinates;
        blockType = type;
        _render.material.color = ColorManager.instance.GetColorByBlockType(type);
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

    public void SetObjectToDestroy()
    {
        _render.transform.DOScale(0,destroyTime).OnComplete(()=>StartCoroutine(SetObjectInactive())).SetDelay(0.1f);
    }

    private IEnumerator SetObjectInactive()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

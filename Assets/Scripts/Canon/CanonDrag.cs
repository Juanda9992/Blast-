using UnityEngine;
using DG.Tweening;
using System.Collections;
public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float anchorTime;
    private CanonSlot desiredPlatform;
    private Vector3 initialPos;
    private ObstacleDataBase obstacleDataBase;
    private BlockType canonType;
    public void SetUpCanon(BlockType blockType)
    {
        initialPos = transform.localPosition;
        SetUpVisuals(blockType);
        obstacleDataBase = ObstacleDataBase.instance;
    }
    private void SetUpVisuals(BlockType blockType)
    {
        if (blockType == BlockType.Yellow)
        {
            _renderer.material.color = Color.yellow;
        }
        else if(blockType == BlockType.Red)
        {
            _renderer.material.color = Color.red;
        }
    }
    public void OnCanonClicked()
    {
        CanonSlot slot = CanonSlotManager.instance.GetFreeCanonSpace();
        if (slot != null)
        {
            transform.DOMove(slot.transform.position, anchorTime);
        }
    }

    public void DestroyBlocks()
    {
        StartCoroutine("DestroyBlocksCoroutine");
    }

    private IEnumerator DestroyBlocksCoroutine()
    {
        for (int i = 0; i < obstacleDataBase.GetObstaclesInLevel(); i++)
        {
            ObstacleBehaviour gatheredObstacleByColor = obstacleDataBase.GetFirstObstacleByColor(canonType);
            if (gatheredObstacleByColor != null)
            {
                Destroy(gatheredObstacleByColor);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanonSlot"))
        {
            other.GetComponent<CanonSlot>().AttachCanon(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CanonSlot"))
        {
            if (desiredPlatform != null)
            {
                desiredPlatform.AttachCanon(null);
                desiredPlatform = null;
            }
        }
    }

}

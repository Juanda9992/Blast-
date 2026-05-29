using UnityEngine;
using DG.Tweening;
using System.Collections;
public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float anchorTime;
    [SerializeField] private float shootSpeed;
    [SerializeField] private int canonAmmo;
    private CanonSlot desiredPlatform;
    private ObstacleDataBase obstacleDataBase;
    private BlockType canonType;
    public void SetUpCanon(CanonData canonData)
    {
        SetUpVisuals(canonData.canonType);
        obstacleDataBase = ObstacleDataBase.instance;
        canonType = canonData.canonType;
        canonAmmo = canonData.canonAmmo;
    }
    private void SetUpVisuals(BlockType blockType)
    {
        if (blockType == BlockType.Yellow)
        {
            _renderer.material.color = Color.yellow;
        }
        else if (blockType == BlockType.Red)
        {
            _renderer.material.color = Color.red;
        }
    }
    public void OnCanonClicked()
    {
        CanonSlot slot = CanonSlotManager.instance.GetFreeCanonSpace();
        if (slot != null)
        {
            transform.DOMove(slot.transform.position, anchorTime).OnComplete(DestroyBlocks);
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
                gatheredObstacleByColor.gameObject.SetActive(false);
                canonAmmo--;
                if(canonAmmo <= 0)
                {
                    yield break;
                }
            }
            yield return new WaitForSeconds(shootSpeed);
        }
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

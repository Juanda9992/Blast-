using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;
public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float anchorTime;
    [SerializeField] private float shootSpeed;
    [SerializeField] private int canonAmmo;
    [SerializeField] private TextMeshProUGUI ammoText;
    private CanonSlot desiredPlatform;
    private ObstacleDataBase obstacleDataBase;
    private BlockType canonType;
    private bool canShoot = false;
    public void SetUpCanon(CanonData canonData)
    {
        SetUpVisuals(canonData.canonType);
        obstacleDataBase = ObstacleDataBase.instance;
        canonType = canonData.canonType;
        canonAmmo = canonData.canonAmmo;
        ammoText.text = canonAmmo.ToString();
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
        else if(blockType == BlockType.Blue)
        {
            _renderer.material.color = Color.blue;
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
        while(true)
        {
            yield return new WaitUntil(()=>BlockAvaliableOnFirstRow());
            ObstacleBehaviour[] obstaclesFirstRow = obstacleDataBase.GetobstaclesFirstRow();
            for (int i = 0; i < obstaclesFirstRow.Length; i++)
            {
                if (obstaclesFirstRow[i] == null)
                {
                    continue;
                }

                if (obstaclesFirstRow[i].GetBlockType() != canonType)
                {
                    continue;
                }

                canonAmmo--;
                ammoText.text = canonAmmo.ToString();

                obstacleDataBase.DestroyBlock(obstaclesFirstRow[i]);
                if (canonAmmo <= 0)
                {
                    yield return transform.DOScale(0, 0.3f).OnComplete(() => Destroy(gameObject)).SetDelay(0.2f).WaitForCompletion();
                }
                yield return new WaitForSeconds(shootSpeed);
            }
            yield return new WaitForSeconds(0.07f);
        }
    }

    private bool BlockAvaliableOnFirstRow()
    {
        ObstacleBehaviour[] obstaclesFirstRow = obstacleDataBase.GetobstaclesFirstRow();
        bool result = false;
        for (int i = 0; i < obstaclesFirstRow.Length; i++)
        {
            if (obstaclesFirstRow[i] ==null)
            {
                continue;
            }

            if(obstaclesFirstRow[i].GetBlockType() == canonType)
            {
                result = true;
            }
        }

        return result;
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

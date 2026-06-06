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

    public Vector2Int canonCoordinates;
    private CanonSlot desiredCanon = null;
    private ObstacleDataBase obstacleDataBase;
    [SerializeField] private BlockType canonType;
    public void SetUpCanon(CanonData canonData,Vector2Int coordinates)
    {
        SetUpVisuals(canonData.canonType);
        obstacleDataBase = ObstacleDataBase.instance;
        canonType = canonData.canonType;
        canonAmmo = canonData.canonAmmo;
        ammoText.text = canonAmmo.ToString();
        canonCoordinates = coordinates;
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
        else if(blockType == BlockType.Green)
        {
            _renderer.material.color = Color.green;
        }
    }
    public void OnCanonClicked()
    {
        if(canonCoordinates.y != 0)
        {
            return;
        }
        desiredCanon = CanonSlotManager.instance.GetFreeCanonSpace();
        if (desiredCanon != null)
        {
            desiredCanon.AttachCanon(this);
            CanonCoordinatesManager.instance.AttachCanon(this);
            transform.DOMove(desiredCanon.transform.position, anchorTime).OnComplete(DestroyBlocks);
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
                    yield return transform.DOScale(0, 0.3f).OnComplete(() => 
                    {
                        desiredCanon.AttachCanon(null);
                        Destroy(gameObject);
                    }).SetDelay(0.2f).WaitForCompletion();
                }
                yield return new WaitForSeconds(shootSpeed);
            }
            yield return new WaitForSeconds(0.06f);
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

    public void UpdateCanonPos()
    {
        transform.DOLocalMoveZ(canonCoordinates.y * -2,0.2f);
    }
    public BlockType GetCanonType()
    {
        return canonType;
    }
}

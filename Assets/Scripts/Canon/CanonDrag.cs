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
    private bool isMysterious;
    public void SetUpCanon(CanonData canonData, Vector2Int coordinates)
    {
        obstacleDataBase = ObstacleDataBase.instance;
        canonType = canonData.canonType;
        canonAmmo = canonData.canonAmmo;
        isMysterious = canonData.isMistery;
        ammoText.text = canonAmmo.ToString();
        canonCoordinates = coordinates;
        SetUpVisuals();
    }
    private void SetUpVisuals()
    {
        if (canonCoordinates.y != 0 && isMysterious)
        {
            _renderer.material.color = Color.gray;
            return;
        }
        if (canonType == BlockType.Yellow)
        {
            _renderer.material.color = Color.yellow;
        }
        else if (canonType == BlockType.Red)
        {
            _renderer.material.color = Color.red;
        }
        else if (canonType == BlockType.Blue)
        {
            _renderer.material.color = Color.blue;
        }
        else if (canonType == BlockType.Green)
        {
            _renderer.material.color = Color.green;
        }
        else if (canonType == BlockType.Orange)
        {
            _renderer.material.color = new Color(1, 0.6f, 0, 1);
        }
    }
    public void OnCanonClicked()
    {
        if (canonCoordinates.y != 0)
        {
            return;
        }
        desiredCanon = CanonSlotManager.instance.GetFreeCanonSpace();
        if (desiredCanon != null)
        {
            desiredCanon.AttachCanon(this);
            transform.DOMove(desiredCanon.transform.position, anchorTime).OnComplete(DestroyBlocks);
            CanonCoordinatesManager.instance.AttachCanon(this);
        }
    }

    public void DestroyBlocks()
    {
        StartCoroutine("DestroyBlocksCoroutine");
    }

    public void UpdateCannonAmmo(int ammo)
    {
        canonAmmo = ammo;
        ammoText.text = canonAmmo.ToString();
    }

    private IEnumerator DestroyBlocksCoroutine()
    {
        while (desiredCanon != null)
        {
            yield return new WaitUntil(() => BlockAvaliableOnFirstRow());
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

                int ammo = obstaclesFirstRow[i].GetObstacleHeight();
                canonAmmo -= ammo;
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
            if (obstaclesFirstRow[i] == null)
            {
                continue;
            }

            if (obstaclesFirstRow[i].GetBlockType() == canonType)
            {
                result = true;
            }
        }

        return result;
    }

    public void UpdateCanonPos()
    {
        transform.DOLocalMoveZ(canonCoordinates.y * -2, 0.2f);
        SetUpVisuals();
    }

    public BlockType GetCanonType()
    {
        return canonType;
    }

    public int GetCannonAmmo()
    {
        return canonAmmo;
    }
}

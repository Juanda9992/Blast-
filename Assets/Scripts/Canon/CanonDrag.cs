using UnityEngine;
using DG.Tweening;
public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float anchorTime;
    private CanonSlot desiredPlatform;
    private Vector3 initialPos;

    public void SetUpCanon(int index)
    {
        initialPos = transform.localPosition;
        SetUpVisuals(index);
    }
    private void SetUpVisuals(int index)
    {
        _renderer.material.color = LevelRulesManager.instance.GetLevelRules().levelColors[index];
    }
    public void OnCanonClicked()
    {
        CanonSlot slot = CanonSlotManager.instance.GetFreeCanonSpace(); 
        if(slot != null)
        {
            transform.DOMove(slot.transform.position,anchorTime);
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

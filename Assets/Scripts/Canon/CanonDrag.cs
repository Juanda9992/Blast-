using UnityEngine;
using DG.Tweening;
public class CanonDrag : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    [SerializeField] private float dragSpeed = 20;
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

    public void MoveCanon(Vector3 pos)
    {
        pos.z += 5;
        transform.localPosition = Vector3.Lerp(transform.localPosition,pos,Time.deltaTime * dragSpeed);
    }

    public void ReleaseCanon()
    {
        if(desiredPlatform == null || !desiredPlatform.isEmpty)
        {
            transform.DOLocalMove(initialPos,anchorTime);
        }
        else
        {
            desiredPlatform.AttachCanon(this);
            transform.DOMove(desiredPlatform.transform.position,anchorTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CanonSlot"))
        {
            desiredPlatform = other.GetComponent<CanonSlot>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("CanonSlot"))
        {
            desiredPlatform = null;
        }
    }

}

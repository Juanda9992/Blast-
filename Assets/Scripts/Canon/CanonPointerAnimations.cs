using DG.Tweening;
using UnityEngine;

public class CanonPointerAnimations : MonoBehaviour
{
    [SerializeField] private float animationTime;
    void OnMouseEnter()
    {
        transform.DOScale(1.1f, animationTime);
    }

    void OnMouseExit()
    {
        transform.DOScale(1, animationTime);
    }
}

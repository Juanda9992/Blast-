using UnityEngine;

public class CanonSlot : MonoBehaviour
{
    public bool isEmpty
    {
        get {return currentCanon == null;}
    }
    private CanonDrag currentCanon;
    public void AttachCanon(CanonDrag canonDrag)
    {
        currentCanon = canonDrag;
    }
}

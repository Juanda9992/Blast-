using UnityEngine;

public class CanonSlot : MonoBehaviour
{
    public bool isEmpty
    {
        get {return currentCanon == null;}
    }
    [SerializeField]private CanonDrag currentCanon;
    public void AttachCanon(CanonDrag canonDrag)
    {
        currentCanon = canonDrag;
    }

    public BlockType AttachedCanonType()
    {
        return isEmpty ? BlockType.None : currentCanon.GetCanonType();
    }

    public CanonDrag GetCurrentCanon()
    {
        return currentCanon;
    }
}

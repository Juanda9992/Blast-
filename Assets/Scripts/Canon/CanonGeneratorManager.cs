using UnityEngine;

public class CanonGeneratorManager : MonoBehaviour
{
    [Header("Positioning Settings")]
    [SerializeField] private Transform canonSlotsParent;
    [SerializeField] private Transform canonDragParent;
    [SerializeField] private int cannonsToCreate;

    [SerializeField] private float avaliableSpace = 6;
    [SerializeField] private float canonSlotParentZPos;
    [SerializeField] private float canonDragParentZPos;
    [Header("Prefab Settings")]
    [SerializeField] private CanonSlot canonPlatformPrefab;
    [SerializeField] private CanonDrag canonDragPrefab;

    private void Start()
    {
        float spaceBetweenObjects = avaliableSpace / cannonsToCreate;

        float totalWidth = spaceBetweenObjects * (cannonsToCreate - 1);

        float offset = totalWidth / 2;

        for (int i = 0; i < cannonsToCreate; i++)
        {
            CanonSlot canonSlot = Instantiate(canonPlatformPrefab, canonSlotsParent);
            canonSlot.transform.localPosition = new Vector3((spaceBetweenObjects * i) - offset, 0, 0);


            CanonDrag canonDrag = Instantiate(canonDragPrefab, canonDragParent);
            canonDrag.transform.localPosition = new Vector3((spaceBetweenObjects * i) - offset, 0, 0);
            canonDrag.SetUpCanon(i);
        }

        canonSlotsParent.transform.position = new Vector3(0,0,canonSlotParentZPos);
        canonDragParent.transform.position = new Vector3(0,0,canonDragParentZPos);
    }
}

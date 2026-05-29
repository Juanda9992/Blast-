using UnityEngine;

public class CanonGeneratorManager : MonoBehaviour
{
    [Header("Positioning Settings")]
    [SerializeField] private Transform canonSlotsParent;
    [SerializeField] private Transform canonDragParent;

    [SerializeField] private float avaliableSpace = 6;
    [SerializeField] private float canonSlotParentZPos;
    [SerializeField] private float canonDragParentZPos;
    [Header("Prefab Settings")]
    [SerializeField] private CanonSlot canonPlatformPrefab;
    [SerializeField] private CanonDrag canonDragPrefab;

    private void Start()
    {
        SOLevelRules levelRules = LevelRulesManager.instance.GetLevelRules();

        float spaceBetweenObjects = avaliableSpace / levelRules.canonSlots;

        float totalWidth = spaceBetweenObjects * (levelRules.canonSlots - 1);

        float offset = totalWidth / 2;

        for (int i = 0; i < levelRules.canonSlots; i++)
        {
            CanonSlot canonSlot = Instantiate(canonPlatformPrefab, canonSlotsParent);
            canonSlot.transform.localPosition = new Vector3((spaceBetweenObjects * i) - offset, 0, 0);
            CanonSlotManager.instance.SetCanonInSlot(i, canonSlot);
        }

        for (int i = 0; i < levelRules.canons.Count; i++)
        {
            for (int j = 0; j < levelRules.canons[i].canonDatas.Length; j++)
            {
                CanonDrag canonDrag = Instantiate(canonDragPrefab, canonDragParent);
                canonDrag.transform.localPosition = new Vector3((spaceBetweenObjects * j) - offset, 0, 0);
                canonDrag.SetUpCanon(levelRules.canons[i].canonDatas[j].canonType);
            }
        }
        canonSlotsParent.transform.position = new Vector3(0, 0, canonSlotParentZPos);
        canonDragParent.transform.position = new Vector3(0, 0, canonDragParentZPos);
    }
}

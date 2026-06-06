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
        canonSlotsParent.transform.position = new Vector3(0, 0, canonSlotParentZPos);

        GenerateCanons(levelRules);
    }

    private void GenerateCanons(SOLevelRules levelRules)
    {       
        float canonsRowSpace = 2;
        for (int i = 0; i < levelRules.canons.Count; i++)
        {
            float avaliabelCanonSpace = 5.5f;
            float SpaceBetweenCanons = avaliabelCanonSpace / levelRules.canons[i].canonDatas.Length;
            float canonsWidth = SpaceBetweenCanons * (levelRules.canons[i].canonDatas.Length-1);
            float canonOffset = canonsWidth / 2;

            for (int j = 0; j < levelRules.canons[i].canonDatas.Length; j++)
            {
                Vector2Int coordinates = new Vector2Int(j,i);
                CanonDrag canonDrag = Instantiate(canonDragPrefab, canonDragParent);
                canonDrag.transform.localPosition = new Vector3((SpaceBetweenCanons * j) - canonOffset, 0, 0 - (i * canonsRowSpace));
                canonDrag.SetUpCanon(levelRules.canons[i].canonDatas[j],coordinates);

                CanonCoordinatesManager.instance.AddCanonToDatabase(coordinates,canonDrag);
            }
        }
        canonDragParent.transform.position = new Vector3(0, 0, canonDragParentZPos);
    }
}

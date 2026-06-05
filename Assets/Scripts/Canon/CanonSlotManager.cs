using UnityEngine;

public class CanonSlotManager : MonoBehaviour
{
    public static CanonSlotManager instance;
    [SerializeField] private CanonSlot[] slotsInLevel;

    void Awake()
    {
        instance = this;
    }
    private void SetUpSlotsArray()
    {
        slotsInLevel = new CanonSlot[LevelRulesManager.instance.GetLevelRules().canonSlots];   
    }
    public void SetCanonInSlot(int index, CanonSlot canonSlot)
    {
        if(slotsInLevel.Length == 0)
        {
            SetUpSlotsArray();
        }
        slotsInLevel[index] = canonSlot;
    }

    public CanonSlot GetFreeCanonSpace()
    {
        for(int i =0;i<slotsInLevel.Length;i++)
        {
            if(slotsInLevel[i].isEmpty)
            {
                return slotsInLevel[i];
            }
        }
        return null;
    }
}

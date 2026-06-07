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
        if (slotsInLevel.Length == 0)
        {
            SetUpSlotsArray();
        }
        slotsInLevel[index] = canonSlot;
    }

    public void OnCanonMovedToSlot()
    {
        if (CheckCanonsOfSameColor())
        {
            ClearCanons();
        }
    }
    private void ClearCanons()
    {
        int middleCanon = Mathf.RoundToInt(slotsInLevel.Length / 2);
        int ammo = 0;
        for (int i = 0; i < slotsInLevel.Length; i++)
        {
            CanonDrag canon = slotsInLevel[i].GetCurrentCanon();
            ammo += canon.GetCannonAmmo();
        }
        slotsInLevel[middleCanon].GetCurrentCanon().UpdateCannonAmmo(ammo);


        for (int i = 0; i < slotsInLevel.Length; i++)
        {
            if (i != middleCanon)
            {
                Destroy(slotsInLevel[i].GetCurrentCanon().gameObject);
                slotsInLevel[i].AttachCanon(null);
            }
        }

    }

    private bool CheckCanonsOfSameColor()
    {
        bool result = true;
        for (int i = 0; i < slotsInLevel.Length; i++)
        {
            if (slotsInLevel[i].isEmpty)
            {
                return false;
            }

            if (slotsInLevel[i].GetCurrentCanon().GetCanonType() != slotsInLevel[0].GetCurrentCanon().GetCanonType())
            {
                result = false;
            }
        }
        return result;
    }

    public CanonSlot GetFreeCanonSpace()
    {
        for (int i = 0; i < slotsInLevel.Length; i++)
        {
            if (slotsInLevel[i].isEmpty)
            {
                return slotsInLevel[i];
            }
        }
        return null;
    }

}

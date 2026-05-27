using UnityEngine;

public class LevelRulesManager : MonoBehaviour
{
    public static LevelRulesManager instance;
    [SerializeField] private SOLevelRules levelRules;

    void Awake()
    {
        instance = this;
    }

    public SOLevelRules GetLevelRules()
    {
        return levelRules;
    }
}

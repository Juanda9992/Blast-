using UnityEngine;
using UnityEngine.UI;

public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    private int blocksDestroyed = 0;
    // Start is called before the first frame update
    void Start()
    {
        progressBar.maxValue = LevelRulesManager.instance.GetLevelRules().levelBlocks;
        progressBar.value = blocksDestroyed;
    }

    private void UpdateBlockCount()
    {
        blocksDestroyed++;
        progressBar.value = blocksDestroyed;

        if(progressBar.value == progressBar.maxValue)
        {
        }

    }
    void OnEnable()
    {
        ObstacleDataBase.OnBlockDestroyed += UpdateBlockCount;
    }
    void OnDisable()
    {
        ObstacleDataBase.OnBlockDestroyed -= UpdateBlockCount;
    }
}

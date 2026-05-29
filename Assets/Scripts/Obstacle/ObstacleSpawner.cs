using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerParent;
    [SerializeField] private ObstacleBehaviour obstaclePrefab;
    [SerializeField] private int startXCoor =-5;
    [SerializeField] private int startYCoor =-5;
    [SerializeField] private LevelRulesManager levelRulesManager;

    void Start()
    {
        SpawnBlocks();
    }
    public void SpawnBlocks()
    {
        StartCoroutine(SpawnBlocksCoroutine());
    }
    private IEnumerator SpawnBlocksCoroutine()
    {
        SOLevelRules rules = levelRulesManager.GetLevelRules();

        for(int i = 0; i< rules.rows.Count;i++)
        {
            for(int j = 0; j<rules.rows[i].blocks.Length;j++)
            {
                ObstacleBehaviour obstacle = Instantiate(obstaclePrefab,spawnerParent);

                float xPos = startXCoor + (j+0.5f);
                obstacle.transform.localPosition = new Vector3(startXCoor + (0.9f*j),0,startYCoor+i);
                obstacle.SetUpBlock(rules.rows[i].blocks[j]);
            }
            yield return null;
        }
    }
}

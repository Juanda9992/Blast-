using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerParent;
    [SerializeField] private ObstacleBehaviour obstaclePrefab;
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
        float blockXSize = 0.9f;
        float totalWidth = blockXSize * 10;
        float offset = totalWidth / 2;
        for(int i = 0; i< rules.rows.Count;i++)
        {
            for(int j = 0; j<rules.rows[i].blocks.Length;j++)
            {
                ObstacleBehaviour obstacle = Instantiate(obstaclePrefab,spawnerParent);
                ObstacleDataBase.instance.AddObstacleToDB(obstacle);
                float xPos = (j * blockXSize) - offset + (blockXSize / 2);
                obstacle.transform.localPosition = new Vector3(xPos,0,startYCoor+i);
                obstacle.SetUpBlock(rules.rows[i].blocks[j]);
            }
            yield return null;
        }
    }
}

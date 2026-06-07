using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerParent;
    [SerializeField] private ObstacleBehaviour obstaclePrefab;
    [SerializeField] private int startYCoor = -5;
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

        int counter = 0;
        for (int i = 0; i < rules.rows.Count; i++)
        {
            for (int j = 0; j < rules.rows[i].blocks.Length; j++)
            {
                Vector2Int coordinates = new Vector2Int(j, i);
                ObstacleBehaviour obstacle = Instantiate(obstaclePrefab, spawnerParent);
                ObstacleDataBase.instance.AddObstacleToDB(obstacle, coordinates);
                float xPos = (j * blockXSize) - offset + (blockXSize / 2);
                obstacle.transform.localPosition = new Vector3(xPos, 0, startYCoor + i);


                obstacle.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * rules.rows[i].layerLevel, transform.localScale.z);

                obstacle.SetUpBlock(rules.rows[i].blocks[j], coordinates, rules.rows[i].layerLevel);

                obstacle.name = "Obstacle_" + counter;
                counter++;
            }
            yield return null;
        }
    }
}

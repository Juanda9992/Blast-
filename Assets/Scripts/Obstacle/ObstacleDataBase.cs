using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDataBase : MonoBehaviour
{
    public static ObstacleDataBase instance;
    [SerializeField] private Dictionary<Vector2Int,ObstacleBehaviour> obstacleBehaviours = new Dictionary<Vector2Int, ObstacleBehaviour>();
    void Awake()
    {
        instance = this;
    }
    public void AddObstacleToDB(ObstacleBehaviour obstacle,Vector2Int coordinates)
    {
        obstacleBehaviours.Add(coordinates,obstacle);
    }

    public ObstacleBehaviour[] GetobstaclesFirstRow()
    {
        ObstacleBehaviour[] firstRow = new ObstacleBehaviour[10];
        for(int i = 0; i< 10;i++)
        {
            Vector2Int coordinates = new Vector2Int(i,0);
            firstRow[i] = obstacleBehaviours[coordinates];
        }

        Debug.Log(firstRow.Length);
        return firstRow;
    }

    public void DestroyBlock(ObstacleBehaviour obstacleBehaviour)
    {
        obstacleBehaviours[obstacleBehaviour.GetBlockCoordinates()] = null;
        Destroy(obstacleBehaviour.gameObject);
    }

    public int GetObstaclesInLevel()
    {
        return obstacleBehaviours.Count;
    }
}

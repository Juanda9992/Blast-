using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDataBase : MonoBehaviour
{
    public static ObstacleDataBase instance;
    [SerializeField] private List<ObstacleBehaviour> obstacleBehaviours = new List<ObstacleBehaviour>();
    void Awake()
    {
        instance = this;
    }
    public void AddObstacleToDB(ObstacleBehaviour obstacle)
    {
        obstacleBehaviours.Add(obstacle);
    }

    public ObstacleBehaviour GetFirstObstacleByColor(BlockType blockType)
    {
        for(int i = 0; i< obstacleBehaviours.Count;i++)
        {
            if(obstacleBehaviours[i].GetBlockType() == blockType)
            {
                ObstacleBehaviour gatheredObstacle = obstacleBehaviours[i];
                obstacleBehaviours.RemoveAt(i);
                return gatheredObstacle;
            }
        }
        return null;
    }

    public int GetObstaclesInLevel()
    {
        return obstacleBehaviours.Count;
    }
}

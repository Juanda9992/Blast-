using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleDataBase : MonoBehaviour
{
    public static event Action OnBlockDestroyed;
    public static ObstacleDataBase instance;
    [SerializeField] private Dictionary<Vector2Int, ObstacleBehaviour> obstacleBehaviours = new Dictionary<Vector2Int, ObstacleBehaviour>();
    [SerializeField] private List<Vector2Int> coords = new List<Vector2Int>();
    [SerializeField] private List<ObstacleBehaviour> obstacles = new List<ObstacleBehaviour>();
    void Awake()
    {
        instance = this;
    }
    public void AddObstacleToDB(ObstacleBehaviour obstacle, Vector2Int coordinates)
    {
        obstacleBehaviours.Add(coordinates, obstacle);
    }

    public ObstacleBehaviour[] GetobstaclesFirstRow()
    {
        ObstacleBehaviour[] firstRow = new ObstacleBehaviour[10];
        for (int i = 0; i < 10; i++)
        {
            Vector2Int coordinates = new Vector2Int(i, 0);
            firstRow[i] = obstacleBehaviours[coordinates];
        }
        return firstRow;
    }

    public void DestroyBlock(ObstacleBehaviour obstacleBehaviour)
    {
        obstacleBehaviours[obstacleBehaviour.blockCoordinates] = null;
        MoveColumns(obstacleBehaviour.blockCoordinates.x);
        Destroy(obstacleBehaviour.gameObject);

        OnBlockDestroyed?.Invoke();
    }

    private void MoveColumns(int columnIndex)
    {
        int height = LevelRulesManager.instance.GetLevelRules().levelHeight;
        Vector2Int coordinates = new Vector2Int(columnIndex, 0);
        for (int i = 0; i < height; i++)
        {
            coordinates.y = i;

            if (obstacleBehaviours[coordinates] != null) //The column that the block was destroyed is null, so we move 1 row up and set the new row to the next one
            {
                Vector2Int newCoordinates = coordinates;
                newCoordinates.y -= 1;
                ObstacleBehaviour prevObstacle = obstacleBehaviours[coordinates];

                obstacleBehaviours[coordinates] = null;
                obstacleBehaviours[newCoordinates] = prevObstacle;

                prevObstacle.blockCoordinates = newCoordinates;
                prevObstacle.UpdateBlockPos();


            }
        }
        coords = obstacleBehaviours.Keys.ToList();
        obstacles = obstacleBehaviours.Values.ToList();

    }
}

using System.Collections.Generic;
using UnityEngine;

public class CanonCoordinatesManager : MonoBehaviour
{
    public Dictionary<Vector2Int,CanonDrag> canonsDictionary = new Dictionary<Vector2Int, CanonDrag>();
    public static CanonCoordinatesManager instance;

    void Awake()
    {
        instance = this;
    }

    public void AddCanonToDatabase(Vector2Int coordinates,CanonDrag canonDrag)
    {
        canonsDictionary.Add(coordinates,canonDrag);
    }

    public void AttachCanon(CanonDrag canonDrag)
    {
        canonsDictionary[canonDrag.canonCoordinates] = null;
        MoveCanons(canonDrag.canonCoordinates.x);
    }

    private void MoveCanons(int columnIndex)
    {
        Vector2Int coordinates = new Vector2Int(columnIndex,0);

        for(int i = 0; i< LevelRulesManager.instance.GetLevelRules().canons.Count;i++)
        {
            coordinates.y = i;

            if(canonsDictionary[coordinates] != null)
            {
                CanonDrag prevCanon = canonsDictionary[coordinates];

                canonsDictionary[coordinates - Vector2Int.down] = prevCanon;
                canonsDictionary[coordinates] = null;

                prevCanon.canonCoordinates = coordinates - Vector2Int.up;

                prevCanon.UpdateCanonPos();
            }
        }
    }
}

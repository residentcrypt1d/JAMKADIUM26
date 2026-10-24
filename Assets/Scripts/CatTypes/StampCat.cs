using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StampCat : Cat
{
    public void StampCatSpecial(){

    }

    public override List<Vector3Int> GetValidMoves()
    {
        var toReturn = LevelManager.instance.walkableTiles.Select(t => t.gridPosition).ToList();
        toReturn.Remove(gridPos);
        return toReturn;
    }
}

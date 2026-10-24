using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public override List<Vector3Int> GetInkPatternFromMovement(Vector3Int destination)
    {
        return new List<Vector3Int>(){destination};
    }

    public override async Task MoveTo(Vector3Int destination)
    {
        throw new System.NotImplementedException();
    }
}

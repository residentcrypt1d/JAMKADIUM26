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

    protected override void PassedIntoNewCell(Vector3Int newCell)
    {
        return;
    }

    protected override void ArrivedAtDestination(Vector3Int destination)
    {
        WalkableTile tile = LevelManager.instance.tilemap.GetInstantiatedObject(destination).GetComponent<WalkableTile>();
        tile.painted = true;
        tile.paintedBy = CatType.Stamp;
    }
}

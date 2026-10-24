using UnityEngine;
using System.Collections.Generic;

public class InkCatBehavior : Cat
{
    public void InkCatSpecial(){

    }

    public override List<Vector3Int> GetValidMoves(){
        return null;
    }

    private void GetTilesRecursive(Vector3Int coord, List<Vector3Int> visited, List<Vector3Int> list, int depth)
    {
        depth++;
        visited.Add(coord);

        //LevelManager.instance._tilemap.Tiles
        
    }
}

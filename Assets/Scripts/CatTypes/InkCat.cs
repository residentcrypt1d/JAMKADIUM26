using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

public class InkCat : Cat
{
    private const int MAX_JUMP_DIST = 3;
    
    public void InkCatSpecial(){

    }

    public override List<Vector3Int> GetValidMoves()
    {
        List<Vector3Int> outList = new List<Vector3Int>();
        GetTilesRecursive(gridPos, new Dictionary<Vector3Int, int>(), outList, 0);
        return outList;
    }

    public override List<Vector3Int> GetInkPatternFromMovement(Vector3Int destination)
    {
        return new List<Vector3Int>(){destination, destination + Vector3Int.right, destination + Vector3Int.up, destination + Vector3Int.left, destination + Vector3Int.down};
    }

    protected override void PassedIntoNewCell(Vector3Int newCell)
    {
        return;
    }

    protected override void ArrivedAtDestination(Vector3Int destination)
    {
        var inkTargets = GetInkPatternFromMovement(gridPos);
        inkTargets.ForEach(t =>
        {
            WalkableTile tile = LevelManager.instance.tilemap.GetInstantiatedObject(t)?.GetComponent<WalkableTile>();
            if (tile)
            {
                tile.painted = true;
                tile.paintedBy = CatType.Ink;
            }
        });
    }

    private void GetTilesRecursive(Vector3Int coord, Dictionary<Vector3Int, int> visitedDepths, List<Vector3Int> list, int depth)
    {
        if (visitedDepths.TryGetValue(coord, out int existingDepth))
        {
            if (existingDepth > depth)
            {
                visitedDepths[coord] = depth;

                if (existingDepth == MAX_JUMP_DIST)
                {
                    list.Remove(coord);
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            visitedDepths.Add(coord, depth);
        }

        if (LevelManager.instance.tilemap.GetTile(coord) is Tile tile)
        {
            if (tile.gameObject.TryGetComponent(out WalkableTile walkable) && depth == MAX_JUMP_DIST)
            {
                list.Add(coord);
            }
        }

        if (depth >= MAX_JUMP_DIST) return;
        
        Vector3Int[] neighbors = {coord + Vector3Int.right, coord + Vector3Int.up, coord + Vector3Int.left, coord + Vector3Int.down};

        foreach (Vector3Int p in neighbors)
        {
            if (visitedDepths.TryGetValue(p, out int storedDepth) && storedDepth <= depth + 1) continue;
            
            GetTilesRecursive(p, visitedDepths, list, depth + 1);
        }
        
    }
}

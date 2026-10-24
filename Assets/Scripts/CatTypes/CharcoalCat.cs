using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CharcoalCat : Cat
{
    public void CharcoalCatSpecial(){

    }

    public override List<Vector3Int> GetValidMoves(){
        List<Vector3Int> toReturn = new List<Vector3Int>();
        toReturn.Add(CharcoalHelper(new Vector3Int(1,0,0), gridPos));
        toReturn.Add(CharcoalHelper(new Vector3Int(-1,0,0), gridPos));
        toReturn.Add(CharcoalHelper(new Vector3Int(0,1,0), gridPos));
        toReturn.Add(CharcoalHelper(new Vector3Int(0,-1,0), gridPos));

        while (toReturn.Contains(gridPos)){
            toReturn.Remove(gridPos);
        }

        return toReturn;
    }

    public override List<Vector3Int> GetInkPatternFromMovement(Vector3Int destination)
    {
        Vector3Int offset = destination - gridPos;
        int squares = (int)offset.magnitude;
        Vector3Int direction = offset / squares;

        List<Vector3Int> toReturn = new List<Vector3Int>();
        
        for (int i = 1; i <= squares; i++)
        {
            toReturn.Add(gridPos + (i * direction));
        }

        return toReturn;
    }

    public override async Task MoveTo(Vector3Int destination)
    {
        throw new System.NotImplementedException();
    }

    private Vector3Int CharcoalHelper(Vector3Int dir, Vector3Int pos){
        if (LevelManager.instance.tilemap.GetTile(pos+dir) == null){
            return pos;
        }

        return CharcoalHelper(dir, pos+dir);
    }
}

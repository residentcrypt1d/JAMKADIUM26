using UnityEngine;
using System.Collections.Generic;

public class CharcoalCat : Cat
{
    public void CharcoalCatSpecial(){

    }

    public override List<Vector3Int> GetValidMoves(){
        List<Vector3Int> toReturn = new List<Vector3Int>();
        toReturn.Add(CharcoalHelper(toReturn, new Vector3Int(1,0,0), gridPos));
        toReturn.Add(CharcoalHelper(toReturn, new Vector3Int(-1,0,0), gridPos));
        toReturn.Add(CharcoalHelper(toReturn, new Vector3Int(0,1,0), gridPos));
        toReturn.Add(CharcoalHelper(toReturn, new Vector3Int(0,-1,0), gridPos));

        while (toReturn.Contains(gridPos)){
            toReturn.Remove(gridPos);
        }

        return toReturn;
    }

    private Vector3Int CharcoalHelper(List<Vector3Int> list, Vector3Int dir, Vector3Int pos){
        if (LevelManager.instance.tilemap.GetTile(pos+dir) == null){
            return pos;
        }

        return CharcoalHelper(list, dir, pos+dir);
    }
}

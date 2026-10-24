using UnityEngine;
using System.Collections.Generic;

public abstract class Cat : MonoBehaviour
{
    public Vector3Int gridPos;
    public WalkableTile currTile;

    public abstract List<Vector3Int> GetValidMoves();
}
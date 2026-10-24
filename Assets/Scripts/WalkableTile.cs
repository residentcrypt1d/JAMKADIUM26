using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTile : MonoBehaviour
{
    public bool painted;
    public CatType paintedBy;
    
    public List<Cat> occupants;
    public Sprite sprite;
    public Vector3Int gridPosition;
    public Tile tile;

    protected void Start()
    {
        gridPosition = LevelManager.instance.tilemap.WorldToCell(transform.position);
        tile = LevelManager.instance.tilemap.GetTile(gridPosition) as Tile;
        if (tile == null)
        {
            Debug.LogWarning("I don't have a tile? Who spawned me?!", this);
        }
    }

    public bool CanMoveTo()
    {
        return occupants.Count < 1;
    }

    public void AddOccupant(Cat newOccupant)
    {
        occupants.Add(newOccupant);
    }

    public void RemoveOccupant(Cat leavingOccupant)
    {
        if (!occupants.Contains(leavingOccupant))
        {
            Debug.LogError($"{leavingOccupant.name} tried to leave a tile that it did not occupy.");
        }

        occupants.Remove(leavingOccupant);
    }
}
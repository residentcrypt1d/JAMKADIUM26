using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTile : MonoBehaviour
{
    public bool painted = false;
    public List<Cat> occupants;
    public Sprite sprite;
    public Vector3Int gridPosition;
    public Tile tile;

    private void Start()
    {
        gridPosition = LevelManager.instance.tilemap.WorldToCell(transform.position);
        tile = LevelManager.instance.tilemap.GetTile(gridPosition) as Tile;
    }

    public bool CanMoveTo()
    {
        return occupants.Count < 1;
    }

    public void MoveTo(Cat newOccupant)
    {
        occupants.Add(newOccupant);
    }

    public void Leave(Cat leavingOccupant)
    {
        if (!occupants.Contains(leavingOccupant))
        {
            Debug.LogError($"{leavingOccupant.name} tried to leave a tile that it did not occupy.");
        }

        occupants.Remove(leavingOccupant);
    }
    
}
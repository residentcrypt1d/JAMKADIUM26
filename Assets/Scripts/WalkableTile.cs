using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTile : TileBase
{
    public bool painted = false;
    public List<Cat> occupants;

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
            Debug.LogError($"{leavingOccupant.name} treid to leave a tile that it did not occupy.");
        }

        occupants.Remove(leavingOccupant);
    }
    
}
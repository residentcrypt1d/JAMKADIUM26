using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public abstract class Cat : MonoBehaviour
{
    public Vector3Int gridPos;
    public WalkableTile currTile;

    public abstract List<Vector3Int> GetValidMoves();

    public abstract List<Vector3Int> GetInkPatternFromMovement(Vector3Int destination);
    
    public abstract Task MoveTo(Vector3Int destination);
    

    #region debug
    private InputAction debugClick;
    
    private void Start()
    {
        debugClick = InputSystem.actions.FindAction("Click");
        debugClick.performed += DebugMovement;
    }
    
    public void DebugMovement(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.value;
        // print(mousePos);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        gridPos = LevelManager.instance.tilemap.WorldToCell(mousePos);
        List<Vector3Int> validMoves = GetValidMoves();
        List<Vector3Int> inkPositions = new List<Vector3Int>();
        foreach (Vector3Int v in validMoves)
        {
            inkPositions.AddRange(GetInkPatternFromMovement(v));
        }
        
        foreach (WalkableTile w in LevelManager.instance.walkableTiles)
        {
            if (w.tile == null)
            {
                Debug.LogWarning("Tile Missing");
                continue;
            }
            
            if(validMoves.Contains(w.gridPosition) && inkPositions.Contains(w.gridPosition))
                LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.purple);
            else if (validMoves.Contains(w.gridPosition))
               LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.red);
            else if(inkPositions.Contains(w.gridPosition))
            {
                LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.blue);
            }
            else
                LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.white);
        }
        
        
    }
    #endregion
}
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public abstract class Cat : MonoBehaviour
{
    public Vector3Int gridPos;
    public WalkableTile currTile;

    public abstract List<Vector3Int> GetValidMoves();

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
        
        foreach (WalkableTile w in LevelManager.instance.walkableTiles)
        {
            if (w.tile == null)
            {
                Debug.LogWarning("Tile Missing");
                continue;
            }
            
            if (validMoves.Contains(w.gridPosition))
               LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.red);
            else
                LevelManager.instance.tilemap.SetColor(w.gridPosition, Color.white);
        }
        
        
    }
}
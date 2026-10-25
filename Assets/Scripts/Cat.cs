using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public abstract class Cat : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public Vector3Int gridPos
    {
        get
        {
            return currTile.gridPosition;
        }
    }
    public WalkableTile currTile;

    public abstract List<Vector3Int> GetValidMoves();

    public abstract List<Vector3Int> GetInkPatternFromMovement(Vector3Int destination);

    [SerializeField] private float transitionTime = 1.0f;
    
    public async Task MoveTo(Vector3Int destination)
    {
        currTile.RemoveOccupant(this);
        
        Vector3 startPos = transform.position;
        Vector3 endPos = LevelManager.instance.tilemap.GetCellCenterWorld(destination);

        int numberOfFrames = Mathf.CeilToInt(transitionTime / Time.deltaTime);

        Vector3Int previousGridPos = gridPos;
        
        for (int i = 0; i < numberOfFrames; i++)
        {
            transform.position = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(i / (float)numberOfFrames));
            var currentGridPos = LevelManager.instance.tilemap.WorldToCell(transform.position);
            if (currentGridPos != previousGridPos)
            {
                PassedIntoNewCell(currentGridPos);
                previousGridPos = currentGridPos;
            }
            await Awaitable.NextFrameAsync();
        }

        currTile = LevelManager.instance.tilemap.GetInstantiatedObject(destination).GetComponent<WalkableTile>();
        currTile.AddOccupant(this);
        ArrivedAtDestination(gridPos);
        transform.position = endPos;
    }

    protected abstract void PassedIntoNewCell(Vector3Int newCell);
    
    protected abstract void ArrivedAtDestination(Vector3Int destination);

    #region debug

    /*
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
    */

    #endregion
}

public enum CatType
{
    Ink,
    Charcoal,
    Stamp
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private GameObject[] levels;
    public List<WalkableTile> walkableTiles = new List<WalkableTile>();
    [HideInInspector] public Tilemap tilemap;
    private int currLevel = 0;
    private int paintedTiles = 0;

    private InputAction click;
    
    [HideInInspector] public Cat selectedCat;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        click = InputSystem.actions.FindAction("Click");
    }

    void Start()
    {
        LoadLevel(currLevel);
    }

    
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.value;
        // print(mousePos);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellPos = tilemap.WorldToCell(mousePos);
        
        // print(cellPos);

        // _tilemap.SetColor(cellPos, Color.red);

        WalkableTile tile = tilemap.GetInstantiatedObject(cellPos)?.GetComponent<WalkableTile>();
        if (tile)
        {
            if (click.WasPressedThisFrame())
            {
                ProcessClick(tile);
            }

            /*if(tile && !tile.painted)
            {
                Debug.Log("AUHGUHWEHFHG");
                tile.painted = true;
                tilemap.SetColor(cellPos, Color.blue);
                paintedTiles++;
            }*/
        }
        
        VisualUpdate(cellPos);
    }

    private void ProcessClick(WalkableTile walkableTile)
    {
        print($"Clicked {walkableTile.gameObject.name}");
        
        print($"\t{walkableTile.occupants}");
        if (walkableTile.occupants.Count > 0)
        {
            if (selectedCat && walkableTile.occupants.Contains(selectedCat)) selectedCat = null;
            else if (walkableTile.occupants.Count == 1) selectedCat = walkableTile.occupants[0];
            else selectedCat = walkableTile.occupants.First(c => c is not InkCat);
        }

        if (!selectedCat)
        {
            print($"\tNo cat selected, returning.");
            return;
        }
        
        
        print($"\t{selectedCat} is selected.");
        
        var validMoves = selectedCat.GetValidMoves();
        if (validMoves.Contains(walkableTile.gridPosition))
        {
            selectedCat.MoveTo(walkableTile.gridPosition);
        }
        
    }

    
    private void VisualUpdate(Vector3Int mousedOverGrid)
    {
        List<Vector3Int> validMoves = new List<Vector3Int>();
        List<Vector3Int> inkPositions = new List<Vector3Int>();
        
        if (selectedCat)
        {
            validMoves = selectedCat.GetValidMoves();


            if (validMoves.Contains(mousedOverGrid))
            {
                inkPositions.AddRange(selectedCat.GetInkPatternFromMovement(mousedOverGrid));
            }
        }

        foreach (WalkableTile w in walkableTiles)
        {
            if(validMoves.Contains(w.gridPosition) && inkPositions.Contains(w.gridPosition))
                tilemap.SetColor(w.gridPosition, Color.purple);
            else if (validMoves.Contains(w.gridPosition))
                tilemap.SetColor(w.gridPosition, Color.red);
            else if(inkPositions.Contains(w.gridPosition))
            {
                tilemap.SetColor(w.gridPosition, Color.blue);
            }
            else if(w.painted)
                tilemap.SetColor(w.gridPosition, Color.darkSlateGray);
            else
                tilemap.SetColor(w.gridPosition, Color.white);
        }
    }
    
    public void LoadLevel(int levelNum)
    {
        if(levelNum >= levels.Length)
        {
            Debug.LogError("Level " + levelNum + " does not exist.");
            return;
        }
        
        if(tilemap)
        {
            Destroy(tilemap.gameObject);
        }
        tilemap = Instantiate(levels[levelNum], transform).GetComponent<Tilemap>();
        if(!tilemap)
        {
            Debug.LogError("Level " + levelNum + " GameObject does not have a tilemap.");
            return;
        }

        walkableTiles = GetComponentsInChildren<WalkableTile>().ToList();
    }

    public void CheckCompletion()
    {
        if(paintedTiles >= walkableTiles.Count)
        {
            Debug.Log("You're winner!");
            currLevel++;
            LoadLevel(currLevel);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private GameObject selectionMapPrefab;
    [SerializeField] private GameObject paintMapPrefab;
    public List<WalkableTile> walkableTiles = new List<WalkableTile>();
    [HideInInspector] public Tilemap tilemap;
    public int currLevel = 0;

    private InputAction click;
    
    [HideInInspector] public Cat selectedCat;

    private Tilemap selectionMap;
    private Tilemap paintMap;
    private int currMoves = 0;
    public UnityEvent<int> onMoveMade = new();
    public UnityEvent<int> onGameOver = new();

    [SerializeField] private Tile cursorTile;
    [SerializeField] private Tile canSelectTile;
    [SerializeField] private Tile selectedCatTile;
    [SerializeField] private Tile potentialDestinationTile;
    [SerializeField] private Tile potentialPaintTile;
    
    [SerializeField] private Tile[] inkTiles;
    [SerializeField] private Tile charcoalTile;
    [SerializeField] private Tile stampTile;
    
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
        selectionMap = Instantiate(selectionMapPrefab, transform).GetComponent<Tilemap>();
        paintMap = Instantiate(paintMapPrefab, transform).GetComponent<Tilemap>();
        
        if(SceneLoader.instance?.levelLoaded > -1)
        {
            currLevel = SceneLoader.instance.levelLoaded - 1;
        }
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
            if (click.WasPressedThisFrame() && !Cat.CatMoving)
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

    private async Task ProcessClick(WalkableTile walkableTile)
    {
        if (walkableTile.occupants.Count > 0)
        {
            if (selectedCat && walkableTile.occupants.Contains(selectedCat)) selectedCat = null;
            else if (walkableTile.occupants.Count == 1) selectedCat = walkableTile.occupants[0];
            else selectedCat = walkableTile.occupants.First(c => c is not InkCat);
        }

        if (!selectedCat)
        {
            return;
        }

        var validMoves = selectedCat.GetValidMoves();
        if (validMoves.Contains(walkableTile.gridPosition))
        {
            await selectedCat.MoveTo(walkableTile.gridPosition);
            currMoves++;
            onMoveMade.Invoke(currMoves);
            CheckCompletion();
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
            if (selectedCat && w.occupants.Contains(selectedCat))
            {
                print("This is the selected cat tile");
                selectionMap.SetTile(w.gridPosition, selectedCatTile);
            }
            else if (w.occupants.Count > 0)
            {
                selectionMap.SetTile(w.gridPosition, canSelectTile);
            }
            else if (validMoves.Contains(w.gridPosition))
            {
                selectionMap.SetTile(w.gridPosition, potentialDestinationTile);
            }
            else if(inkPositions.Contains(w.gridPosition))
            {
                selectionMap.SetTile(w.gridPosition, potentialPaintTile);
            }
            else
            {
                selectionMap.SetTile(w.gridPosition, null);
            }
            
            if(w.painted)
            {
                switch (w.paintedBy)
                {
                    case CatType.Ink:
                        print(w.gridPosition);
                        paintMap.SetTile(w.gridPosition, inkTiles[((w.gridPosition.x + w.gridPosition.y) * 17) % inkTiles.Length]);
                        break;
                    case CatType.Charcoal:
                        paintMap.SetTile(w.gridPosition, charcoalTile);
                        break;
                    case CatType.Stamp:
                        paintMap.SetTile(w.gridPosition, stampTile);
                        break;
                }
            }
            else
                paintMap.SetTile(w.gridPosition, null);
        }
    }
    
    public void LoadLevel(int levelNum)
    {
        if(levelNum >= GameHandler.instance.levels.Length)
        {
            Debug.LogError("Level " + levelNum + " does not exist.");
            return;
        }
        
        if(tilemap)
        {
            Destroy(tilemap.gameObject);
        }
        tilemap = Instantiate(GameHandler.instance.levels[levelNum].levelPrefab, transform).GetComponent<Tilemap>();

        tilemap.transform.localPosition = Vector3.zero;
        selectionMap.transform.localPosition = Vector3.zero;
        paintMap.transform.localPosition = Vector3.zero;
        
        if(!tilemap)
        {
            Debug.LogError("Level " + levelNum + " GameObject does not have a tilemap.");
            return;
        }

        walkableTiles = GetComponentsInChildren<WalkableTile>().ToList();
        
        CameraAutoResize.ResizeTo(tilemap.localBounds.center, tilemap.localBounds);
        
        selectionMap.ClearAllTiles();
        paintMap.ClearAllTiles();
    }

    public void CheckCompletion()
    {
        int paintedTiles = 0;
        foreach(WalkableTile tile in walkableTiles)
        {
            if(tile.painted)
            {
                paintedTiles++;
            }
        }

        if(paintedTiles >= walkableTiles.Count)
        {
            onGameOver.Invoke(currMoves);
            GameHandler.instance.RecordScore(currLevel, currMoves);
        }
    }
}

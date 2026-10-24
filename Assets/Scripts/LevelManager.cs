using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private GameObject[] levels;
    private List<WalkableTile> walkableTiles = new List<WalkableTile>();
    public Tilemap _tilemap;
    private int currLevel = 0;
    
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
    }

    void Start()
    {
        LoadLevel(currLevel);
    }
    
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.value;
        print(mousePos);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellPos = _tilemap.WorldToCell(mousePos);
        
        print(cellPos);
        Debug.DrawLine(transform.position, _tilemap.GetCellCenterWorld(cellPos));

        WalkableTile tile = _tilemap.GetTile<WalkableTile>(cellPos);
        if(tile && !tile.painted)
        {
            tile.painted = true;
            _tilemap.SetColor(cellPos, Color.red);
        }
    }

    public void LoadLevel(int levelNum)
    {
        // if(levelNum >= levels.Length)
        // {
        //     Debug.LogError("Level " + levelNum + " does not exist.");
        //     return;
        // }
        
        // if(_tilemap)
        // {
        //     Destroy(_tilemap.gameObject);
        // }
        // _tilemap = Instantiate(levels[levelNum], transform).GetComponent<Tilemap>();
        // if(!_tilemap)
        // {
        //     Debug.LogError("Level " + levelNum + " GameObject does not have a tilemap.");
        //     return;
        // }
        // foreach(TileBase tile in _tilemap.GetTilesBlock(_tilemap.cellBounds))
        // {
        //     if(tile is WalkableTile)
        //     {
        //         walkableTiles.Add();
        //     }
        // }
    }

    public void CheckCompletion()
    {
        
    }
}

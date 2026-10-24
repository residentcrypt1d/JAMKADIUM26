using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class LevelManager : MonoBehaviour
{
    private Tilemap _tilemap;
    
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }
    
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.value;
        print(mousePos);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellPos = _tilemap.WorldToCell(mousePos);
        
        print(cellPos);
        Debug.DrawLine(transform.position, _tilemap.GetCellCenterWorld(cellPos));
        
        _tilemap.SetColor(cellPos, Color.red);
    }
}

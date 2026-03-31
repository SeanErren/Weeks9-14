using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilemapStuff : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform cheese;

    public Tile flowers;

    public CinemachineImpulseSource impulseSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //Vector3Int only accepts whole numbers because the tilemap doesn't have fractions
        //Get the cell in the tilemap under the mouse pos
        Vector3Int highlightedCell = tilemap.WorldToCell(mousePos);
        //Get the center of the cell under the mouse
        //tilemaps don't like Vector3
        Vector3 cellCenter = tilemap.GetCellCenterWorld(highlightedCell);
        Debug.Log(highlightedCell);
        cheese.position = cellCenter;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            tilemap.SetTile(highlightedCell, flowers);
            impulseSource.GenerateImpulse();
        }
    }
}

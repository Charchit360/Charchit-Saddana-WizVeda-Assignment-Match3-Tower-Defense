using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public sealed class Tile : MonoBehaviour
{
    public int x;      // location of tile on x
    public int y;      // location of tile on y
    private Item _item;   // Item that the tile contains

    public Item item
    {
        get => _item;
        set
        {
            if (_item == value) return;  // If the item is already set to the value, do nothing
            _item = value;                // Set the item to the new value
            icon.sprite = _item.sprite;  // Update the icon sprite to the new item's sprite 
        }
    }


    public Image icon;
    public Button button;    // Each tile will be interactive through touch 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tile Left=> x>0 ?Board.Instance.Tiles[x - 1, y]:null; // Get the tile to the left of the current tile
    public Tile Right => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null; // Get the tile to the right of the current tile
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1] : null; // Get the tile above the current tile
    public Tile Bottom => y < Board.Instance.Height-1 ? Board.Instance.Tiles[x, y + 1] : null; // Get the tile below the current tile

    public Tile[] Neighbours => new[]
    {
        Left,Top,Right, Bottom
    };

    void Start()
    {
        button.onClick.AddListener(() => Board.Instance.Select(this));
    }
    public List<Tile> GetConnectedTiles(List<Tile> exclude = null)
    {
        var result = new List<Tile> { this,};
        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            exclude.Add(this);
        }
        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.item != item)
                continue;
            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }
        return result;

        
    }
    
}

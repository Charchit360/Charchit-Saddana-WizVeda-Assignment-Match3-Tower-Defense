using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    private void Awake() => Instance = this;    //Intializes the singleton instance of Board
    public Row[] rows;                          // Each board will contain rows
    public Tile[,] Tiles { get; private set; }   // 2D array of tiles  
    public int Width => Tiles.GetLength(0);      // Number of tiles in the x direction 6 in this case
    public int Height => Tiles.GetLength(1);     // Number of tiles in the y direction 6 in this case

    private readonly List<Tile> _selection = new List<Tile>();

    private const float TweenDuration = 0.25f; // Duration for the tweening animations


    private bool isProcessing = false;

    public int Upgrades = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.main.UpdateUpgrades(Upgrades); // Initialize the UI with the current upgrade count
        Tiles = new Tile[rows.Max(Row => Row.Tiles.Length), rows.Length];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var tile = rows[y].Tiles[x]; // Get the tile from the current row at position x
                tile.x = x; tile.y = y; // Set the tile's x and y coordinates
                tile.item = ItemsDatabase.Items[UnityEngine.Random.Range(0, ItemsDatabase.Items.Length)]; // Assign a random item to the tile
                Tiles[x, y] = tile;          // Assign each tile to the 2D array

            }
        }
    }

    void Update()
    {
        if (!isProcessing && CanPop())
        {
            isProcessing = true;
            PopAndContinue();
        }
    }


    private async void PopAndContinue()
    {
        await Pop();
        isProcessing = false;
    }


    public async void Select(Tile tile)
    {
        if (tile == null || _selection.Contains(tile))
            return;

        _selection.Add(tile);

        if (_selection.Count < 2)
            return;

        // Only allow adjacent swaps
        if (Mathf.Abs(_selection[0].x - _selection[1].x) + Mathf.Abs(_selection[0].y - _selection[1].y) != 1)
        {
            _selection.Clear();
            return;
        }

        Debug.Log($"Selected tiles at ({_selection[0].x},{_selection[0].y}) and ({_selection[1].x},{_selection[1].y})");

        await Swap(_selection[0], _selection[1]);
        if (CanPop())
        {
            await Pop();
        }
        else
        {
            // If no pop is possible, swap back
            await Swap(_selection[0], _selection[1]);
        }
        _selection.Clear();
        // Clear selection after swap
    }


    public async Task Swap(Tile tile1, Tile tile2)
    {
        if (tile1 == null || tile2 == null)
            return;

        // Save coordinates
        int x1 = tile1.x, y1 = tile1.y;
        int x2 = tile2.x, y2 = tile2.y;

        // Animate positions with DOTween (0.2s)
        var pos1 = tile1.transform.position;
        var pos2 = tile2.transform.position;

        var seq = DOTween.Sequence();
        seq.Join(tile1.transform.DOMove(pos2, 0.2f))
           .Join(tile2.transform.DOMove(pos1, 0.2f));

        await seq.Play().AsyncWaitForCompletion();

        // After animation: swap them in the Tiles array
        Tiles[x1, y1] = tile2;
        Tiles[x2, y2] = tile1;

        // Update coordinates inside each tile
        tile1.x = x2; tile1.y = y2;
        tile2.x = x1; tile2.y = y1;

        // Make sure their transforms are snapped exactly to the grid
        tile1.transform.position = pos2;
        tile2.transform.position = pos1;

        
        
    }

    private bool CanPop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count()>=2)
                {
                    return true; // If any tile has at least 2 connected tiles, we can pop
                }
                
            }
        }
        return false; // If no tile has at least 2 connected tiles, we cannot pop
    }

    //private async void Pop()
    //{
    //    for(var y = 0; y < Height; y++)
    //    {
    //        for (var x = 0; x < Width; x++)
    //        {
    //            var tile = Tiles[x, y];
    //            var connectedTiles = tile.GetConnectedTiles();
    //            if ((connectedTiles.Skip(1).Count()<2))
    //            {
    //                continue;
    //            }
    //            // Pop the connected tiles

    //            var deflateSequence = DOTween.Sequence();
    //            foreach (var connectedTile in connectedTiles)
    //            {
    //                deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));
    //            }
    //            await deflateSequence.Play().AsyncWaitForCompletion();

    //            LevelManager.main.AddCurrency((tile.item.value*connectedTiles.Count)); // Add currency based on the number of popped tiles

    //            var inFlateSequence = DOTween.Sequence();

    //            foreach (var connectedTile in connectedTiles)
    //            {
    //                connectedTile.item = ItemsDatabase.Items[UnityEngine.Random.Range(0,ItemsDatabase.Items.Length)]; // Assign a new random item to the tile
    //                inFlateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
    //            }
    //            await inFlateSequence.Play().AsyncWaitForCompletion();
    //        }
    //    }
    //}
    private async Task Pop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];
                var connectedTiles = tile.GetConnectedTiles();
                if ((connectedTiles.Skip(1).Count() < 2))
                {
                    continue;
                }

                if (connectedTiles.Count >= 4)
                {
                    Upgrades++;
                    //Debug.Log($"Upgrade increased! Total: {Upgrades}");           // Special case of >=4 tiles
                    UIManager.main.UpdateUpgrades(Upgrades); // Update the UI with the new upgrade count
                }
                // Pop the connected tiles

                var deflateSequence = DOTween.Sequence();
                foreach (var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));
                }
                await deflateSequence.Play().AsyncWaitForCompletion();

                LevelManager.main.AddCurrency((tile.item.value * connectedTiles.Count)); // Add currency based on the number of popped tiles

                var inFlateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.item = ItemsDatabase.Items[UnityEngine.Random.Range(0, ItemsDatabase.Items.Length)]; // Assign a new random item to the tile
                    inFlateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                }
                await inFlateSequence.Play().AsyncWaitForCompletion();
            }
        }
    }




}


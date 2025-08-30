using UnityEngine;

[CreateAssetMenu(menuName = "Match3/Item")]
public class Item : ScriptableObject
{
    public int value;     // Score value of Tile
    public Sprite sprite; // Color of Tile
    
}

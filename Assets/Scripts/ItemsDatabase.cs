using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static Item[] Items { get; private set; } // Array to hold all items in the database

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]private static void Initialize()=> Items = Resources.LoadAll<Item>("Items/"); // Load all items from the Resources folder

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

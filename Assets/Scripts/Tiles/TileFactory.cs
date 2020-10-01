using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] List<Tile> tilePrefabs;

    private Dictionary<string, Tile> tileDictionary;

    private void Awake()
    {
        tileDictionary = new Dictionary<string, Tile>();

        foreach (Tile tile in tilePrefabs)
        {
            tileDictionary.Add(tile.TileType, tile);
        }
    }

    public Tile CreateNewTile(string id)
    {
        Tile newTile;

        if(tileDictionary.TryGetValue(id, out newTile))
        {
            return Instantiate(newTile);
        }
        else
        {
            throw new System.Exception("Tile type not found");
        }
    }

    public Tile CreateRandomTile()
    {
        int randomKey = Random.Range(0, tileDictionary.Count - 1);

        return Instantiate(tileDictionary.Values.ElementAt(randomKey));
    }
}

using PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private TileFactory _tileFactory;

    public int gridWidth = 8;
    public int gridHeigh = 8;
    public float gap = 0.0f;

    float hexWidth = 1f;
    float hexHeight = 0.9913f;

    private Tile[,] tiles;

    Vector3 startPosition;

    private void Awake()
    {
        Tile[,] initGrid = new Tile[gridWidth, gridHeigh];
        tiles = initGrid;
    }

    private void Start()
    {
        AddGap();
        GetStartPosition();
        CreateGrid();
    }

    /// <summary>
    ///     Adds a gap between hex tiles
    /// </summary>
    /// <param name="gridPosition">tile position in the grid</param>
    /// <returns>World position for grid position</returns>
    private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    /// <summary>
    ///     Gets world position where grid starts.
    /// </summary>
    private void GetStartPosition()
    {
        float offset = 0f;

        if (gridHeigh / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = -hexHeight * 0.75f * (gridHeigh / 2);

        startPosition = new Vector3(x, 0, z);
    }

    /// <summary>
    ///     Calculates the world position for x and y grid position.
    /// </summary>
    /// <param name="gridPosition">tile position in the grid</param>
    /// <returns>World position for grid position</returns>
    private Vector3 CalculateWorldPosition(Vector2 gridPosition)
    {
        float offset = 0;
        
        // Set offset on odd rows
        if (gridPosition.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPosition.x + gridPosition.x * hexWidth + offset;
        float z = startPosition.z - gridPosition.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    /// <summary>
    ///     Search and link sorrounding tiles as neighbours.
    /// </summary>
    private void CreateGrid()
    {
        for (int y = 0; y < gridHeigh; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Tile hex = _tileFactory.CreateRandomTile();
                Vector2 gridPosition = new Vector2(x, y);
                hex.transform.position = CalculateWorldPosition(gridPosition);
                hex.transform.parent = this.transform;
                hex.X = x;
                hex.Y = y;
                hex.name = hex.name + "Hexagon" + x + "|" + y;

                tiles[x, y] = hex;
                SetNeighbours(hex);
            }
        }
    }


    /// <summary>
    ///     Search and link sorrounding tiles as neighbours.
    /// </summary>
    /// <param name="tile"></param>
    private void SetNeighbours(Tile tile)
    {
        int x = tile.X;
        int y = tile.Y;

        Tile neighbour;

        if(tile != null)
        {
            if (x > 0)
            {
                neighbour = tiles[x - 1, y];
                LinkNeighbours(tile, neighbour);
            }

            if (y > 0)
            {
                // Get top tile
                neighbour = tiles[x, y - 1];
                LinkNeighbours(tile, neighbour);

                //Even row
                if(y % 2 == 0)
                {
                    // Get top left tile
                    if(x > 0)
                    {
                        neighbour = tiles[x - 1, y - 1];
                        LinkNeighbours(tile, neighbour);

                    }
                }
                else
                {
                    // Get top right tile
                    if (x < gridWidth-1)
                    {
                        neighbour = tiles[x + 1, y - 1];
                        LinkNeighbours(tile, neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Set neighbourhood between tiles
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="neighbour"></param>
    private static void LinkNeighbours(Tile tile, Tile neighbour)
    {
        tile.AddNeighbour(neighbour);
        neighbour.AddNeighbour(tile);
    }
}

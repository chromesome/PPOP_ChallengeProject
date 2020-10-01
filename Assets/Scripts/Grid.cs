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

    private Tile[][] tiles;

    Vector3 startPosition;

    private void Start()
    {
        AddGap();
        GetStartPosition();
        CreateGrid();
        SetNeighbours();
    }

    private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    private void GetStartPosition()
    {
        float offset = 0f;

        if (gridHeigh / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = -hexHeight * 0.75f * (gridHeigh / 2);

        startPosition = new Vector3(x, 0, z);
    }

    private Vector3 CalculateWorldPosition(Vector3 gridPosition)
    {
        float offset = 0;
        
        // Set offset on odd rows
        if (gridPosition.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPosition.x + gridPosition.x * hexWidth + offset;
        float z = startPosition.z - gridPosition.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    private void CreateGrid()
    {
        tiles = new Tile[gridWidth][];

        for (int x = 0; x < gridHeigh; x++)
        {
            tiles[x] = new Tile[gridHeigh];
            for (int y = 0; y < gridWidth; y++)
            {
                Tile hex = _tileFactory.CreateRandomTile();
                Vector2 gridPosition = new Vector2(x, y);
                hex.transform.position = CalculateWorldPosition(gridPosition);
                hex.transform.parent = this.transform;
                hex.x = x;
                hex.y = y;
                hex.name = hex.name + "Hexagon" + x + "|" + y;
                tiles[x][y] = hex;
            }
        }
    }

    private void SetNeighbours()
    {
        for (int y = 0; y < gridHeigh; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // TODO check surrounding tiles
            }
        }
    }

}

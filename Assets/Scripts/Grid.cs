using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;

    public int gridWidth = 8;
    public int gridHeigh = 8;

    float hexWidth = 1f;
    float hexHeight = 0.9913f;
    public float gap = 0.0f;

    Vector3 startPosition;

    private void Start()
    {
        AddGap();
        GetStartPosition();
        CreateGrid();
    }

    private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }
    private Vector3 CalculateWorldPosition(Vector3 gridPosition)
    {
        float offset = 0;
        
        // Set offset on odd rows
        if (gridPosition.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPosition.x + gridPosition.x * hexWidth + offset;
        float z = startPosition.z - gridPosition.z * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
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

    private void CreateGrid()
    {
        for (int y = 0; y < gridHeigh; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPosition = new Vector2(x, y);
                hex.position = CalculateWorldPosition(gridPosition);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }

}

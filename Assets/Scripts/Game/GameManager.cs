using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Grid _grid;
    private Tile _startTile;
    private Tile _targetTile;

    private List<Tile> _traversedPath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    ///     A tile was selected.
    /// </summary>
    /// <param name="tile">Selected tile</param>
    public void TileSelected(Tile tile)
    {
        Debug.Log("Selected Start tile: " + tile.name);
        if (_startTile == null)
        {
            _startTile = tile;
            return;
        }

        if (_targetTile == null)
        {
            _targetTile = tile;
        }
        else
        {
            _startTile = _targetTile;
            _targetTile = tile;
            ResetPath();
        }

        if (_startTile != null && _targetTile != null)
        {
            FindPath();
        }
    }

    /// <summary>
    ///     Find closest path from start to target.
    /// </summary>
    private void FindPath()
    {
        IList<PathFinding.IAStarNode> path = PathFinding.AStar.GetPath(_startTile, _targetTile);

        _traversedPath = new List<Tile>();
        foreach (PathFinding.IAStarNode item in path)
        {
            Tile tile = item as Tile;
            if (tile != null)
            {
                tile.Traverse();
                _traversedPath.Add(tile);
            }

        }
    }

    /// <summary>
    ///     Reset traversed path
    /// </summary>
    private void ResetPath()
    {
        foreach (Tile tile in _traversedPath)
        {
            if (_startTile != tile && _targetTile != tile)
            {
                tile.UnSelect();
            }
        }
    }
}

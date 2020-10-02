using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public class Tile : MonoBehaviour, IAStarNode
{
    // public variables
    public bool traversable;

    // private variables
    [SerializeField] string _tileType;
    [SerializeField] int _traverseCost;
    int _x;
    int _y;
    bool _selected;
    private List<Tile> _neighbourTiles;

    // getter and setters
    public string TileType { get => _tileType; }
    public int TraverseCost { get => _traverseCost; }
    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }

    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            foreach(Tile tile in _neighbourTiles)
            {
                if (tile.traversable)
                {
                    yield return tile;
                }
            }

        }
    }

    private void Awake()
    {
        _neighbourTiles = new List<Tile>();
    }

    public float CostTo(IAStarNode neighbour)
    {
        Tile neighbourHex = neighbour as Tile;

        Debug.Assert(neighbourHex != null, "Neighbour is not a Tile");
        if (neighbourHex != null)
        {
            return neighbourHex.TraverseCost;
        }
        else
        {
            throw new System.Exception("Neighbour is not a Tile");
        }
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        Tile targetHex = target as Tile;

        Debug.Assert(targetHex != null, "Neighbour is not a Tile");
        if (targetHex != null)
        {
            int xDistance = Mathf.Abs(this.X - targetHex.X);
            int yDistance = Mathf.Abs(this.Y - targetHex.Y);

            return xDistance + yDistance;
        }
        else
        {
            // TODO: handle gracefully
            throw new System.Exception("Target is not a tile");
        }
    }

    public void AddNeighbour(Tile neighbour)
    {
        _neighbourTiles.Add(neighbour);
    }

    private void OnMouseDown()
    {
        Select();
    }

    /// <summary>
    ///     Set tile as selected.
    /// </summary>
    public void Select()
    {
        if(traversable)
        {
            if(!_selected)
            {
                _selected = true;
                GameManager.instance.TileSelected(this);
            }

            HighlightTile(Color.green);
        }
    }

    /// <summary>
    ///     Tile was traversed, we are going places
    /// </summary>
    public void Traverse()
    {
        if(!_selected)
        {
            HighlightTile(Color.red);
        }
        else
        {
            HighlightTile(Color.green);
        }
    }

    /// <summary>
    ///     Set tile as unselected
    /// </summary>
    public void UnSelect()
    {
        HighlightTile(Color.white);
        _selected = false;
    }

    /// <summary>
    ///     Change tile material color
    /// </summary>
    private void HighlightTile(Color color)
    {
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}

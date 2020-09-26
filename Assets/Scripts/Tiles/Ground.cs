using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public class Ground : Tile, IAStarNode
{
    [SerializeField] private int _traverseCost;
    public int TraverseCost { get => _traverseCost; }

    public IEnumerable<IAStarNode> Neighbours => throw new System.NotImplementedException();

    public float CostTo(IAStarNode neighbour)
    {
        throw new System.NotImplementedException();
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        throw new System.NotImplementedException();
    }
}

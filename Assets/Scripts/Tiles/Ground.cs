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
        Ground neighbourGround = neighbour as Ground;
        if(neighbourGround != null)
        {
            return neighbourGround.TraverseCost;
        }
        else
        {
            //TODO: Excepcion o me estoy perdiendo algo?
            throw new System.Exception("Not a navigable terrain");
        }
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        Ground targetGround = target as Ground;

        int xDistance = Mathf.Abs(this.x - targetGround.x);
        int yDistance = Mathf.Abs(this.y - targetGround.y);

        return xDistance + yDistance;
    }
}

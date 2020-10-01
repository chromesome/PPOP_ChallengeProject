using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private string _tileType;
    public string TileType { get => _tileType; }
    
}

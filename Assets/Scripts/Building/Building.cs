using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableObject
{
    public int price;
    public int XSize = 3;
    public int ZSize = 3;    
    public Renderer Renderer;
    private Color _startColor;

    private void Awake()
    {
        _startColor = Renderer.material.color;   
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < XSize; x++)
            for (int z = 0; z < ZSize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x,0,z) * BuildingPlacer.cellSize, new Vector3(1,0,1) * BuildingPlacer.cellSize);
            }
    }

    public void DisplayUnacceptablePosition()
    {
        Renderer.material.color = Color.red;
    }

    public void DisplayAcceptablePosition()
    {
        Renderer.material.color = _startColor;
    }

}

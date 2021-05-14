using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObstacle: MonoBehaviour
{
    public LayerMask _buildingObstacleLayer;
    private List<Vector2Int> _cells = new List<Vector2Int>();
    private Renderer _renderer;
    private BuildingPlacer _placer;

    private Vector3 _previousPosition, _previousLocalScale;
    private Quaternion _previousRotation;


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _placer = FindObjectOfType<BuildingPlacer>();
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
        _previousLocalScale = transform.localScale;
        UpdateCells();        
    }

    private void Update()
    {
        if (_previousPosition != transform.position
            || _previousRotation != transform.rotation
            || _previousLocalScale != transform.localScale)
        {
            UpdateCells();
            _previousPosition = transform.position;
            _previousRotation = transform.rotation;
            _previousLocalScale = transform.localScale;
        }
    }

    public void UpdateCells()
    {
        RemoveCells();       

        Vector3Int roundedMinPoint, roundedMaxPoint;
        GetRoundedMinMaxPoints(_renderer.bounds, out roundedMinPoint, out roundedMaxPoint);

        for (int x = roundedMinPoint.x; x <= roundedMaxPoint.x; x++)
            for (int z = roundedMinPoint.z; z <= roundedMaxPoint.z; z++)
            {
                if (CheckObstacleIntersectionWithCell(x, z))
                    _cells.Add(new Vector2Int(x, z) * BuildingPlacer.cellSize);
            }

        _cells.ForEach((cell)=> 
                            {
                                if (!_placer.occupiedCells.ContainsKey(cell))
                                    _placer.occupiedCells.Add(cell, this);
                            });
    }

    private void GetRoundedMinMaxPoints(Bounds bounds, out Vector3Int min, out Vector3Int max)
    {
        min = GetRoundedPoint(bounds.min);
        max = GetRoundedPoint(bounds.max);
    }

    private Vector3Int GetRoundedPoint(Vector3 point)
    {
        Vector3 minPoint = new Vector3(point.x, 0, point.z) / BuildingPlacer.cellSize;
        return Vector3Int.RoundToInt(minPoint) * BuildingPlacer.cellSize;
    }

    private bool CheckObstacleIntersectionWithCell(int xCellCoord, int zCellCoord)
    {
        RaycastHit hit;
        return Physics.BoxCast(new Vector3(xCellCoord, transform.position.y - 100f, zCellCoord), Vector3.one * BuildingPlacer.cellSize * 0.5f, 
                                Vector3.up, out hit, Quaternion.identity, 100.1f, _buildingObstacleLayer);
    }

    private void RemoveCells()
    {
        _cells.ForEach((cell) =>
        {
            if (_placer.occupiedCells.ContainsKey(cell) && _placer.occupiedCells[cell] == this)
                _placer.occupiedCells.Remove(cell);
        });
        _cells.Clear();
    }

    private void OnDestroy()
    {
        RemoveCells();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3Int roundedMinPoint, roundedMaxPoint;
        GetRoundedMinMaxPoints(GetComponent<Renderer>().bounds, out roundedMinPoint, out roundedMaxPoint);
        
        for (int x = roundedMinPoint.x; x <= roundedMaxPoint.x; x++)
            for (int z = roundedMinPoint.z; z <= roundedMaxPoint.z; z++)
            {
                if (CheckObstacleIntersectionWithCell(x, z))
                    Gizmos.DrawWireCube(new Vector3(x, 0, z) * BuildingPlacer.cellSize, new Vector3(1, 0, 1) * BuildingPlacer.cellSize);
            }
    }

#endif
}

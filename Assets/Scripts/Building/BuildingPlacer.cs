using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public static int cellSize = 1;
    public Camera mainCamera;
    public Building currentBuilding;
    public Management management;
    public Dictionary<Vector2Int, Building> buildingDict = new Dictionary<Vector2Int, Building>();

    private void Update()
    {
        if (currentBuilding)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            plane.Raycast(ray, out distance);
            Vector3 point = ray.GetPoint(distance) / cellSize;
            Vector3Int buildingPos = Vector3Int.RoundToInt(point) * cellSize;
            currentBuilding.transform.position = buildingPos;
            
            //Build cancelling
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                Resources.Money += currentBuilding.price;
                Destroy(currentBuilding.gameObject);
                currentBuilding = null;                
                management.enabled = true;
                return;
            }

            if (canBuildedHere(buildingPos.x, buildingPos.z, currentBuilding))
            {
                currentBuilding.DisplayAcceptablePosition();
                if (Input.GetMouseButtonDown(0))
                {
                    SetupBuilding(buildingPos.x, buildingPos.z, currentBuilding);
                    currentBuilding = null;
                }
            }
            else 
            {
                currentBuilding.DisplayUnacceptablePosition();
            }
        }
    }

    private void SetupBuilding(int xPos, int zPos, Building building)
    {
        for (int x = 0; x < currentBuilding.XSize; x++)
            for (int z = 0; z < currentBuilding.ZSize; z++)
            {
                buildingDict.Add(new Vector2Int(xPos + x, zPos + z), building);
            }
        management.enabled = true;
    }

    private bool canBuildedHere(int xPos, int zPos, Building building)
    {
        for (int x = 0; x < currentBuilding.XSize; x++)
            for (int z = 0; z < currentBuilding.ZSize; z++)
            {
                if (buildingDict.ContainsKey(new Vector2Int(xPos + x, zPos + z)))
                    return false;
            }
        return true;
    }

    public void CreateBuilding(GameObject buildingPrefab)
    {
        currentBuilding = (Instantiate(buildingPrefab)).GetComponent<Building>();
        management.enabled = false;
    }

}

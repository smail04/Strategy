using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer placer;
    public GameObject buildingPrefab;

    public void TryBuy()
    {
        if (placer.currentBuilding)
            return;

        int price = buildingPrefab.GetComponent<Building>().price;
        if (price <= Resources.Money)
        {
            Resources.Money -= price;
            placer.CreateBuilding(buildingPrefab);
        }
    }
}

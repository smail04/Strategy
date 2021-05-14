using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer placer;
    public GameObject buildingPrefab;
    public Text priceText;

    private void Start()
    {
        priceText.text = buildingPrefab.GetComponent<Building>().price.ToString();
    }

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

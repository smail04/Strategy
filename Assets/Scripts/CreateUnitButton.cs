using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitButton : MonoBehaviour
{
    public Barack currentBuilding;
    public GameObject unitPrefab;

    public void TryBuy()
    {
        int price = unitPrefab.GetComponent<Unit>().price;
        if (price <= Resources.Money)
        {
            Resources.Money -= price;
            currentBuilding.CreateUnit(unitPrefab);
        }
    }
}

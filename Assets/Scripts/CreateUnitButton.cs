using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUnitButton : MonoBehaviour
{
    public Barack currentBuilding;
    public GameObject unitPrefab;
    public Text priceText;

    private void Start()
    {
        priceText.text = unitPrefab.GetComponent<Unit>().price.ToString();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Building
{
    public GameObject UnitsUI;
    public Transform unitSpawnPoint;

    public override void Select()
    {
        base.Select();
        UnitsUI.SetActive(true);
    }

    public override void Unselect()
    {
        base.Unselect();
        UnitsUI.SetActive(false);
    }

    public void CreateUnit(GameObject unit)
    {
        GameObject _unit = Instantiate(unit, unitSpawnPoint.position, Quaternion.identity);
        _unit.GetComponent<Unit>().navMeshAgent.SetDestination(unitSpawnPoint.position + Vector3.right);
    }
}

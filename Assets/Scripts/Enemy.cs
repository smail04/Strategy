using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState 
{
    Idle, 
    WalkToBuilding, 
    WalkToUnit, 
    Attack
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentEnemyState;
    public int health;
    public Building targetBuilding;
    public Unit targetUnit;
    public float distanceToFollow;
    public float distanceToAttack;

    public NavMeshAgent navMeshAgent;
    public float attackPeriod = 1;
    public int damage = 1;

    private float _attackTimer = 0;

    private void Start()
    {
        SetState(EnemyState.WalkToBuilding);

    }

    private void Update()
    {
        if (currentEnemyState == EnemyState.Idle)
        {
            FindClosestUnit();
        }
        else if (currentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestUnit();
            if (targetBuilding == null)
                SetState(EnemyState.Idle);
        }
        else if (currentEnemyState == EnemyState.WalkToUnit)
        {
            if (targetUnit)
            {
                navMeshAgent.SetDestination(targetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, targetUnit.transform.position);
                if (distance > distanceToFollow)
                {
                    SetState(EnemyState.WalkToBuilding);
                }
                if (distance < distanceToAttack)
                {
                    SetState(EnemyState.Attack);
                }
            }
            else
                SetState(EnemyState.WalkToBuilding);
        }
        else if (currentEnemyState == EnemyState.Attack)
        {
            if (targetUnit)
            {

                float distance = Vector3.Distance(transform.position, targetUnit.transform.position);
                if (distance > distanceToAttack)
                {
                    SetState(EnemyState.WalkToUnit);
                }

                _attackTimer += Time.deltaTime;
                if (_attackTimer >= attackPeriod)
                {
                    targetUnit.TakeDamage(damage);
                    _attackTimer = 0;
                } 
            }
            else
                SetState(EnemyState.WalkToBuilding);
        }

    }

    public void SetState(EnemyState newEnemyState)
    {
        currentEnemyState = newEnemyState;
        if (currentEnemyState == EnemyState.Idle)
        {
    
        }
        else if (currentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestBuilding();
            navMeshAgent.SetDestination(targetBuilding.transform.position);
        }
        else if (currentEnemyState == EnemyState.WalkToUnit)
        {

        }
        else if (currentEnemyState == EnemyState.Attack)
        {
            _attackTimer = 0;
        }
    }

    public void FindClosestBuilding()
    {
        Building[] allBuildings = FindObjectsOfType<Building>();

        float minDistance = Mathf.Infinity;

        Building closestBuilding = null;

        foreach (var building in allBuildings)
        {
            float distance = Vector3.Distance(transform.position, building.transform.position);
            if (distance < minDistance)
            {
                closestBuilding = building;
                minDistance = distance;
            }
        }

        targetBuilding = closestBuilding;
    }

    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        float minDistance = Mathf.Infinity;

        Unit closestUnit = null;

        foreach (var unit in allUnits)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance < minDistance)
            {
                closestUnit = unit;
                minDistance = distance;
            }
        }

        if (distanceToFollow > minDistance)
        {
            targetUnit = closestUnit;
            SetState(EnemyState.WalkToUnit);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanceToFollow);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanceToAttack);
    }
#endif

}

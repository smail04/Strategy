using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

public enum UnitState 
{
    Idle, 
    WalkToPoint, 
    WalkToEnemy, 
    Attack
}

public class Knight : Unit
{
    public UnitState currentUnitState;
    public Building targetBuilding;
    public Enemy targetEnemy;
    public float distanceToFollow;
    public float distanceToAttack;

    public float attackPeriod = 1;
    public int damage = 1;

    private float _attackTimer = 0;

    protected override void Start()
    {
        base.Start();
        SetState(UnitState.WalkToPoint);
    }

    private void Update()
    {
        if (currentUnitState == UnitState.Idle)
        {
            FindClosestEnemy();
        }
        else if (currentUnitState == UnitState.WalkToPoint)
        {
            FindClosestEnemy();
        }
        else if (currentUnitState == UnitState.WalkToEnemy)
        {
            if (targetEnemy)
            {
                navMeshAgent.SetDestination(targetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, targetEnemy.transform.position);
                if (distance > distanceToFollow)
                {
                    SetState(UnitState.WalkToPoint);
                }
                if (distance < distanceToAttack)
                {
                    SetState(UnitState.Attack);
                }
            }
            else
                SetState(UnitState.Idle);
        }
        else if (currentUnitState == UnitState.Attack)
        {
            if (targetEnemy)
            {
                navMeshAgent.SetDestination(targetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, targetEnemy.transform.position);
                if (distance > distanceToAttack)
                {
                    SetState(UnitState.WalkToEnemy);
                }

                _attackTimer += Time.deltaTime;
                if (_attackTimer >= attackPeriod)
                {
                    targetEnemy.TakeDamage(damage);
                    _attackTimer = 0;
                } 
            }
            else
                SetState(UnitState.Idle);
        }

    }

    public void SetState(UnitState newUnitState)
    {
        currentUnitState = newUnitState;
        if (currentUnitState == UnitState.Idle)
        {
    
        }
        else if (currentUnitState == UnitState.WalkToPoint)
        {

        }
        else if (currentUnitState == UnitState.WalkToEnemy)
        {

        }
        else if (currentUnitState == UnitState.Attack)
        {
            _attackTimer = 0;
        }
    }

    public void FindClosestEnemy()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        float minDistance = Mathf.Infinity;

        Enemy closestEnemy = null;

        foreach (var enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        if (distanceToFollow > minDistance)
        {
            targetEnemy = closestEnemy;
            SetState(UnitState.WalkToEnemy);
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
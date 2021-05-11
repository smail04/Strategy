using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public int price;
    public int health;
    public NavMeshAgent navMeshAgent;

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        navMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

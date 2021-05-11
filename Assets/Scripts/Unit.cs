using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public int price;
    public int health;
    private int _maxHealth;
    public NavMeshAgent navMeshAgent;
    public GameObject healthBarPrefab;
    private HealthBar _healthBar;
    

    protected override void Start()
    {
        base.Start();
        _maxHealth = health;
        GameObject healthBar = Instantiate(healthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
        
    }

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        navMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        _healthBar.SetHealth(health, _maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(_healthBar.gameObject);
        Destroy(gameObject);
    }
}

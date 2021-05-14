using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicalSpawner : MonoBehaviour
{
    public GameObject spawningPrefab;
    public float period = 1;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > period)
        {
            Instantiate(spawningPrefab, transform.position, transform.rotation);
            _timer = 0;
        }
    }
}

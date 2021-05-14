using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public GameObject coinPrefab;
    public Transform coinSpawner;
    public float coinPeriod = 3;
    private float _timer;
    private AudioSource coinSound;

    protected override void Start()
    {
        base.Start();
        if (coinSpawner)
            coinSound = coinSpawner.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlaced)
        {
            _timer += Time.deltaTime;
            if (_timer > coinPeriod)
            {
                Resources.Money += 1;
                if (coinSound) coinSound.Play();
                GameObject coin = Instantiate(coinPrefab, coinSpawner.position, Camera.main.transform.rotation);
                Destroy(coin, 1);
                _timer = 0;
            }
        }
    }
}

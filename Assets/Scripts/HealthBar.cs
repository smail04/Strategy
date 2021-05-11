using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Transform scaleTransform;
    public Transform target;

    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = Camera.main.transform;        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + Vector3.up * 2;
        transform.rotation = _cameraTransform.rotation;
        //transform.LookAt(_cameraTransform);
    }

    public void Setup(Transform target)
    {
        this.target = target;
    }

    public void SetHealth(int health, int maxHealth)
    {
        float xScale = (float)health / maxHealth;
        scaleTransform.localScale = new Vector3(Mathf.Clamp01(xScale), 1, 1);
    }

}

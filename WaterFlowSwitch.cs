using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowSwitch : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private float flowForce;
    private BuoyancyEffector2D buoyancyEffector2D;

    private void Start()
    {
        buoyancyEffector2D = water.GetComponent<BuoyancyEffector2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buoyancyEffector2D.flowMagnitude = 250;
        }
        else 
        {
            buoyancyEffector2D.flowMagnitude = 0;
        }
    }
}

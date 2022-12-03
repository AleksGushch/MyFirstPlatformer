using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour
{
    private GameObject Interface;
    private InterfaceController interfaceController;

    private void Start()
    {
        Interface = GameObject.Find("Interface");
        interfaceController = Interface.GetComponent<InterfaceController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            interfaceController.DiamondCount(1);
            Destroy(gameObject);
        }
    }
}

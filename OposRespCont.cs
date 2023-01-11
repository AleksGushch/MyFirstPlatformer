using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OposRespCont : MonoBehaviour
{
    [SerializeField] private GameObject opossum;
    [SerializeField] private Transform resp;
    [SerializeField] private float timeToResp;
    private float currentTimeToResp;

    private void Start()
    {
        currentTimeToResp = 0;
    }

    private void Update()
    {
        if (currentTimeToResp >= timeToResp)
        {
            currentTimeToResp = 0;
            OpossumResp();
        }
        else
        {
            currentTimeToResp += Time.deltaTime;
        }
    }

    private void OpossumResp() 
    {
        GameObject opossumClon = Instantiate(opossum, resp.position, Quaternion.identity);
    }
}

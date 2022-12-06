using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleRespawnContoller : MonoBehaviour
{
    [SerializeField] private GameObject eagle;
    [SerializeField] private Transform eagleResp;
    [SerializeField] private float speed, timeToResp, lifetime;
    private float currentTimeToResp, currentLifetime;

    private void Start()
    {
        currentTimeToResp = 0;
    }

    private void Update()
    {
        if (currentTimeToResp >= timeToResp)
        {
            currentTimeToResp = 0;
            EagleResp();
        }
        else 
        {
            currentTimeToResp += Time.deltaTime;
        }
    }

    private void EagleResp() 
    {
        GameObject eagleClon = Instantiate(eagle, eagleResp.position, Quaternion.identity);
        eagleClon.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime) 
        {
            Destroy(eagleClon);
        }
    }
}

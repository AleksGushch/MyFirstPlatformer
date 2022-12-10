using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    [SerializeField] private float timer;
    private float currentTimer;
    private Vector3 ObjPosition;

    private void Start()
    {
        ObjPosition = transform.position;
        currentTimer = 0f;
    }

    private void Update()
    {
        if (Vector3.Distance(ObjPosition, transform.position)>0.16f)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > timer)
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEcontrol : MonoBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private float fireForce;
    [SerializeField] private Transform firePoint;
    private float angle;

    public void Shooting()
    {
        angle = -45f;
        while(angle<=180)
        {
            GameObject fireballClon = Instantiate(fireball, firePoint.position, Quaternion.identity);
            fireballClon.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireForce * Mathf.Cos(Mathf.Deg2Rad*angle),fireForce* Mathf.Sin(Mathf.Deg2Rad*angle));
            fireballClon.GetComponent<SpriteRenderer>().flipX = false;
            fireballClon.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, -angle);
            angle += 45f;
        }
    }
}

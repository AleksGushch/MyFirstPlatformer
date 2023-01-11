using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumRun : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = Vector2.left * speed;
    }
}

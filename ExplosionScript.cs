using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private GameObject clonExplosion;
    [SerializeField] private Transform explosionPosition;
    private bool isActive;

    private void Start()
    {
        isActive = true;
    }

    public void Explosion() 
    {
        if (transform.childCount <5)
        {
            GameObject explosionClon = Instantiate(clonExplosion, explosionPosition.position, Quaternion.identity);
            isActive = false;
        }
    }
    private void Update()
    {
        if (isActive)
        {
            Explosion();
        }
    }
}

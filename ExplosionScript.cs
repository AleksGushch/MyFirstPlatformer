using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private GameObject clonExplosion;
    [SerializeField] private Transform explosionPosition;
    private bool oneClon=true;
    private void Update()
    {
        if ((transform.childCount < 2)&&oneClon)
        {
            GameObject explosionClon = Instantiate(clonExplosion, explosionPosition.position, Quaternion.identity, gameObject.transform);
            oneClon = false;
        }
    }
}

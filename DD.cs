using UnityEngine;

public class DD : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HPenemy hp))
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent(out DamageableObjects dmg)) 
        {
            dmg.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

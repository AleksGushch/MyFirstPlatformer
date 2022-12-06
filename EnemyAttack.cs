using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackZone;
    [SerializeField] private Transform enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject newAttackZone = Instantiate(attackZone, enemy.position, Quaternion.identity);
            newAttackZone.transform.SetParent(enemy);
        }
    }
}

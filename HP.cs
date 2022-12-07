using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float timerForHit;
    private float currentHealth;
    private float hpForInterface;
    private float currentHitTimer;
    private bool isDamage=false;

    public float GetHP => hpForInterface;

    public bool IsAlive => currentHealth > 0;

    public bool IsDamage => isDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;
        isDamage = true;
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        hpForInterface = currentHealth / maxHealth;

        if (IsAlive == false && !gameObject.CompareTag("Player")) 
        {

            Destroy(gameObject);
        }

        if (isDamage)
        {
            if (currentHitTimer <= timerForHit)
            {
                currentHitTimer += Time.deltaTime;
            }
            else
            {
                currentHitTimer = 0;
                isDamage = false;
            }
        }
    }
}

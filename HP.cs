using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private float maxHealth;
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
        IsHit();
        SelfDestroy();
        if (IsAlive == false && !gameObject.CompareTag("Player")) 
        {
            ClearChildren();
            Destroy(gameObject, 1f);
        }
        if (currentHealth > maxHealth)//ХП не может быть больше максимума
            currentHealth = maxHealth;
        hpForInterface = currentHealth / maxHealth;
    }

    private void IsHit() 
    {
        if (isDamage)
        {
            if (currentHitTimer <= 0.3f)
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

    private void ClearChildren() 
    {
        GameObject[] allChildrenObjects = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform) 
        {
            allChildrenObjects[i] = child.gameObject;
            i += 1;
        }
        foreach (GameObject child in allChildrenObjects) 
        {
            Destroy(child.gameObject);
        }
    }

    private void SelfDestroy()
    {
        if (transform.childCount < 1)
        {
            TakeDamage(1000);//перед установкой значения сравни значение урона в дочернем объекте.
            Destroy(gameObject, 1f);
        }
    }
}

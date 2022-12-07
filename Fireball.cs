using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private float fireForce;
    [SerializeField] private Transform firePointFwd;
    [SerializeField] private Transform firePointAft;

    public void Shooting(Vector2 direction)
    {
        if (direction.x < 0)
        {
            GameObject fireballClon = Instantiate(fireball, firePointAft.position, Quaternion.identity);
            fireballClon.GetComponent<Rigidbody2D>().velocity= new Vector2(fireForce * -1, fireballClon.GetComponent<Rigidbody2D>().velocity.y);
            fireballClon.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GameObject fireballClon = Instantiate(fireball, firePointFwd.position, Quaternion.identity);
            fireballClon.GetComponent<Rigidbody2D>().velocity = new Vector2(fireForce * 1, fireballClon.GetComponent<Rigidbody2D>().velocity.y);
            fireballClon.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}

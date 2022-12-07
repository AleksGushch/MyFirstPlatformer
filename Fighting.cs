using UnityEngine;

public class Fighting : MonoBehaviour
{
    [SerializeField] private GameObject swordZone;
    [SerializeField] private Transform SwordZoneFwd;
    [SerializeField] private Transform SwordZoneAft;
    
    public void FightingDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            GameObject swordZoneClon = Instantiate(swordZone, SwordZoneAft.position, Quaternion.identity);
            swordZoneClon.transform.SetParent(SwordZoneAft);
        }
        else
        {
            GameObject swordZoneClon = Instantiate(swordZone, SwordZoneFwd.position, Quaternion.identity);
            swordZoneClon.transform.SetParent(SwordZoneFwd);
        }
    }
}

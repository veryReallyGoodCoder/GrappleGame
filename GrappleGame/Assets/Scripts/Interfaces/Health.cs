using UnityEngine;

public class Health : MonoBehaviour, IPickupable
{
    public int healAmount = 25;


    public void OnPickup(PlayerScript player)
    {
        Debug.Log("Health Pickup: +" + healAmount);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Ammo : MonoBehaviour, IPickupable
{
    public int ammoAmount = 50;

    public void OnPickup(PlayerScript player)
    {
        Debug.Log("Ammo Pickup: +" + ammoAmount);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Ammo : MonoBehaviour, IPickupable
{
    public int ropeIncrease = 50;

    public void OnPickup(PlayerScript player)
    {
        Debug.Log("Rope Pickup: +" + ropeIncrease);
        player.grappleLength += ropeIncrease;
        Destroy(gameObject);
    }
}

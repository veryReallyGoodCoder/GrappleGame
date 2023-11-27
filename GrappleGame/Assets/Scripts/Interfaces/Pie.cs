using UnityEngine;
using UnityEngine.Events;

public class Pie : MonoBehaviour, IPickupable
{

    public UnityEvent End;
    public void OnPickup(PlayerScript player)
    {
        End.Invoke();
        Debug.Log("PIE!");
    }
}

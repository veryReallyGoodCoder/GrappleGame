using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName { get; set; }

    public int damage;

    public int block;

    public virtual void Attack()
    {
        Debug.Log("Attacking with " +  weaponName);
    }

    public virtual void Block()
    {
        Debug.Log("Bloacking with " + weaponName);
    }

}

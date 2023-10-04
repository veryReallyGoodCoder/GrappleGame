using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int weaponDamage;

    public virtual void Attack()
    {
        Debug.Log("Attacking with " +  weaponName);
    }

}

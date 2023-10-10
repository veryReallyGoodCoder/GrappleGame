using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStat", menuName = "WeaponStatData")]

public class Weapons : ScriptableObject
{

    public string weaponName;
    public string weaponType;

    public int weaponDamage;
    public int weaponBlock;

}

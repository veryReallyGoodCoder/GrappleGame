using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatData", menuName = "Character/ EnemyStatData")]

public class EnemyStatData : ScriptableObject
{
    public string enemyName;
    public int enemyDamage;
    public int enemySpeed;
    public int enemyMaxHealth;

    public int enemyRange;
    
}

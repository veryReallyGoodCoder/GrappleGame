using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatData", menuName = "Character/ EnemyStatData")]

public class EnemyStatData : ScriptableObject
{
    public string enemyName;

    public int enemyMaxHealth;
    public int enemyDamage;

    public float enemyWalkSpeed;
    public float enemyRunSpeed;

    public int enemyRange;
    public int attackRange = 3;
    
}

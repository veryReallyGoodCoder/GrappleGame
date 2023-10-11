using UnityEngine;

public class EnemyScript : EnemyAction
{

    public CharacterStatData enemyStats;

    public Transform patrolCenter;

    private int health;

    EnemyAction enemy = new EnemyAction();

    private void Start()
    {
        health = enemyStats.maxHealth;
    }

    private void Update()
    {
        enemy.Patrol(patrolCenter);
    }

}

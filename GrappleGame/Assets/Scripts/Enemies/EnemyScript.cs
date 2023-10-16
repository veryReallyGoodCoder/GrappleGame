using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    public EnemyStatData enemyStats;

    private int health;
    public enum EnemyRank
    {
        Small,
        Medium,
        Large,
        Ranged
    }

    public EnemyRank currentRank;

    EnemyAction enemy;

    [Header("Navigation")]

    public Transform patrolPoint;
    private Vector3 initialPos;


    // Start is called before the first frame update
    void Start()
    {

        SetRank(currentRank);

        health = enemyStats.enemyMaxHealth;
        Debug.Log(currentRank + ", Health: " + health);

        initialPos = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.Patrol(initialPos, patrolPoint.position);
    }

 
    public void SetRank(EnemyRank rank)
    {
        switch (rank)
        {
            case EnemyRank.Small:
                enemy = new SmallEnemy();
                break;
            case EnemyRank.Medium:
                enemy = new MediumEnemy();
                break;
            case EnemyRank.Large:
                enemy = new LargeEnemy();
                break;
            case EnemyRank.Ranged:
                enemy = new RangedEnemy();
                break;
        }
    }
}

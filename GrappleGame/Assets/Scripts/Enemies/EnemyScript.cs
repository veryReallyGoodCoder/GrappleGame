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

    public GameObject patrolPoint;


    // Start is called before the first frame update
    void Start()
    {

        SetRank(currentRank);

        health = enemyStats.enemyMaxHealth;
        Debug.Log(currentRank + ", Health: " + health);

        GameObject point = GameObject.Find("patrolPoint");
        Vector3 destination = point.transform.position;
        //enemy.Patrol(destination);


        void SetRank(EnemyRank rank)
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

    // Update is called once per frame
    void Update()
    {
        //sEnemy.Patrol();

    }

    /*public void SetRank(EnemyRank rank)
    {
        switch (rank)
        {
            case EnemyRank.Small:
                enemy = gameObject.AddComponent<SmallEnemy>();
                break;
            case EnemyRank.Medium:
                enemy = gameObject.AddComponent<MediumEnemy>();
                break;
            case EnemyRank.Large:
                enemy = gameObject.AddComponent<LargeEnemy>();
                break;
            case EnemyRank.Ranged:
                enemy = gameObject.AddComponent<RangedEnemy>();
                break;
        }
    }*/

}

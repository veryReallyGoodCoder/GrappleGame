using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour
{
    [Header("References")]

    NavMeshAgent agent;


    //public Transform patrolCenter;
    [SerializeField] private int patrolRadius;
    private bool isPatrolling;

    public enum EnemyState
    {
        Patrol,
        Attack
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Patrol(Transform patrolCenter)
    {
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        Vector3 destination = patrolCenter.position + new Vector3(randomPoint.x, 0, randomPoint.y);

        agent.SetDestination(destination);
        isPatrolling = true;

        Debug.Log("Enemy Patrol");
    }

    public void AttackPlayer()
    {

    }

}

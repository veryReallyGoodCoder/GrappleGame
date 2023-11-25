using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAction : MonoBehaviour
{

    [Header("Reference")]
    public EnemyStatData enemyStat;

    NavMeshAgent agent;
    Animator animator;

    [SerializeField] private Transform target;
    
    private Vector3 initialPos;
    public Transform endDestination;

    private bool attackingPlayer = false;

    [Header("Attack")]

    [SerializeField] private int damage = 10;
    
    
    
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        initialPos = gameObject.transform.position;
        
    }

    void Update()
    {
        Patrol(initialPos, endDestination.position);
        AttackPlayer();
    }

    //basic patrol script to move enemy between initial position and a destination:
    public virtual void Patrol(Vector3 startPos, Vector3 endPos) 
    {
        Vector3 des;
        animator.SetBool("isPatrolling", true);

        if (!agent.hasPath)
        {
            des = endPos;
            agent.SetDestination(des);
            Debug.Log("new path set");

            if(!agent.pathPending && des == endPos)
            {
                Debug.Log("des reached");

                des = startPos;
                agent.SetDestination(des);
            }

        }

    }

    public virtual void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if(distanceToPlayer <= enemyStat.enemyRange && !attackingPlayer)
        {
            agent.SetDestination(target.position);
            Debug.Log("Attack Player");
            animator.SetBool("isPatrolling", false);
            animator.SetBool("isAttacking", true);
           
            attackingPlayer = true;
        }
        else if(distanceToPlayer > enemyStat.enemyRange && attackingPlayer)
        {
            Patrol(initialPos, endDestination.position);
            animator.SetBool("isAttacking", false);
            attackingPlayer = false;
        }
    }

}

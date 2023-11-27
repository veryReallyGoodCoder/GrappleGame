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

    private float enemySpeed;

    [Header("Attack")]

    [SerializeField] private int damage = 10;
    
    
    
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        initialPos = gameObject.transform.position;

        enemySpeed = enemyStat.enemyWalkSpeed;
        agent.speed = enemySpeed;
    }

    void Update()
    {
        Patrol(initialPos, endDestination.position);
        DetectPlayer();
        //AttackPlayer();
    }

    //basic patrol script to move enemy between initial position and a destination:
    public virtual void Patrol(Vector3 startPos, Vector3 endPos) 
    {
        Vector3 des;
        animator.SetBool("isPatrolling", true);

        agent.speed = enemyStat.enemyWalkSpeed;

        if (!agent.hasPath)
        {
            des = endPos;
            agent.SetDestination(des);

            if(!agent.pathPending && des == endPos)
            {
                des = startPos;
                agent.SetDestination(des);
            }

        }

    }

    public virtual void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= enemyStat.enemyRange && distanceToPlayer >= enemyStat.attackRange && !attackingPlayer)
        {
            agent.SetDestination(target.position);
            agent.speed = enemyStat.enemyRunSpeed;
            Debug.Log("Player Detect");

            animator.SetBool("isPatrolling", false);
            animator.SetBool("isRunning", true);

            //attackingPlayer = true;
        }
        else if(distanceToPlayer <= enemyStat.attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer > enemyStat.enemyRange && attackingPlayer)
        {
            Patrol(initialPos, endDestination.position);
            animator.SetBool("isAttacking", false);
            attackingPlayer = false;
        }
    }

    public virtual void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if(distanceToPlayer <= enemyStat.attackRange && !attackingPlayer)
        {
            agent.SetDestination(target.position);
            Debug.Log("Attack Player");
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
           
            attackingPlayer = true;
        }
        else if(distanceToPlayer > enemyStat.attackRange && attackingPlayer)
        {
            DetectPlayer();
        }
    }

    public virtual void TakeDamage()
    {
        enemyStat.enemyMaxHealth -= damage;
    }

}

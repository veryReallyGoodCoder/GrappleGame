using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAction : MonoBehaviour
{

    public NavMeshAgent agent;
    private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Patrol(Vector3 destination)
    {

        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if(agent.destination == destination)
            {
                agent.SetDestination(startPosition);
            }
            else if(agent.destination == startPosition)
            {
                agent.SetDestination(destination);
            }
        }
        
        Debug.Log("Enemy is Patrolling");
    }

}

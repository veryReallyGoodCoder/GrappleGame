using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAction : MonoBehaviour
{

    public NavMeshAgent agent;
    private Vector3 initialPos;

    public Transform endDestination;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        initialPos = gameObject.transform.position;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol(initialPos, endDestination.position);
        Debug.Log(initialPos);
    }

    public virtual void Patrol(Vector3 startPos, Vector3 endPos)
    {
        Vector3 des;
        //agent.SetDestination(des);

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
            //else if(!agent.pathPending)

        }

    }

}

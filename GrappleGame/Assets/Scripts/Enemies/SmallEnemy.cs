using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : EnemyAction
{
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log("Small Enemy Created");

        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public override void Patrol(Vector3 destination)
    {
        base.agent.SetDestination(destination);
        
        Debug.Log("Small Enemy Patrol");
    }*/

}

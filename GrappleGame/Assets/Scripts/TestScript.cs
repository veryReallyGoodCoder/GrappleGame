using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour
{

    NavMeshAgent agent;

    bool offMeshLinkInProgress = false;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if(agent.isOnOffMeshLink && !offMeshLinkInProgress)
        {
            offMeshLinkInProgress = true;
            StartCoroutine(FollowOffMeshLink());

        }
    }

    IEnumerator FollowOffMeshLink()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Vector3 newPosition = transform.position;
        Vector3 endPosition = agent.currentOffMeshLinkData.endPos;

        while(!Mathf.Approximately(Vector3.SqrMagnitude(endPosition - transform.position), 0f))
        {
            newPosition = Vector3.MoveTowards(transform.position, endPosition, agent.speed * Time.deltaTime);
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }

        offMeshLinkInProgress = false;
        agent.CompleteOffMeshLink();

        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.updateUpAxis = true;

    }

}

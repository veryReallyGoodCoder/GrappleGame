using System;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    [Header("References")]

    public GameObject playerInput;
    
    private LineRenderer lr;
    private SpringJoint joint;

    public LayerMask whatIsGrappable;

    [SerializeField] private new Transform firePoint, camera;

    [Header("Variables")]

    private Vector3 grapplePoint;
    private bool isRaycastEnabled;

    private Transform GrabbedObjectPostion;

    [SerializeField] private float maxDistance = 100f;

    private bool shoot, grappling;

    public float grapplingCd;
    private float grapplingCdTimer;
    public float grappleDelay;

    public float overshootY;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        //speedLines.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        shoot = playerInput.GetComponent<PlayerScript>().shoot;

        //Debug.Log(shoot); 

        if (shoot)
        {
            //Debug.Log("hallo");
            StartGrapple();
        }
        else if (!shoot)
        {
            StopGrapple();
        }

        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }

        if (grappling)
        {
            lr.SetPosition(0, firePoint.position);
        }

    }

    private void StartGrapple()
    {

        isRaycastEnabled = true;

        if (isRaycastEnabled)
        {

            if (grapplingCdTimer > 0) return;

            grappling = true;

            RaycastHit hit;

            if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappable))
            {
                grapplePoint = hit.point;
                //GrabbedObjectPostion = hit.transform;

                isRaycastEnabled = false;

                Invoke(nameof(ExecuteGrapple), grappleDelay);

                //Debug.Log("grapple");

            }
            else
            {
                grapplePoint = camera.position + camera.forward * maxDistance;
                Invoke(nameof(StopGrapple), grappleDelay);
                //Debug.Log("missed target");
            }

        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPo = grapplePoint.y - lowestPoint.y; ;
        float highestArcPoint = grapplePointRelativeYPo + overshootY;

        if(grapplePointRelativeYPo < 0)
        {
            highestArcPoint = overshootY;
        }

        playerInput.GetComponent<PlayerScript>().JumpToPosition(grapplePoint, highestArcPoint);

        //speedLines.SetActive(true);


       Invoke(nameof(StopGrapple), 1f);

    }

    private void StopGrapple()
    {
        //lr.positionCount = 0;
        //Destroy(joint);

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;

        //speedLines.SetActive(false);

        //Debug.Log("stop grapple");
    }

    private void DrawRope()
    {
        /*if (!joint) return;
        
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, grapplePoint);

        Debug.Log("draw rope");*/
    }

}

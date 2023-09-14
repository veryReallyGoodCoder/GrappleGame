using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    [Header("References")]

    public GameObject playerInput;
    
    private LineRenderer lr;
    private SpringJoint joint;

    public LayerMask whatIsGrappable;

    [SerializeField] private new Transform firePoint, camera, player;

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

    }

    // Update is called once per frame
    void Update()
    {
        shoot = playerInput.GetComponent<PlayerScript>().shoot;

        Debug.Log(shoot); 

        if (shoot)
        {
            Debug.Log("hallo");
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

    private void LateUpdate()
    {
        //DrawRope();


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
                GrabbedObjectPostion = hit.transform;
                isRaycastEnabled = false;

                Invoke(nameof(ExecuteGrapple), grappleDelay);

                /*joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

                //mess w/ these
                joint.minDistance = distanceFromPoint * 0.1f;
                joint.maxDistance = distanceFromPoint * 0.1f;

                joint.spring = 10f;
                joint.damper = 15f;
                //joint.massScale = 10;*/



                Debug.Log("grapple");

            }
            else
            {
                grapplePoint = camera.position + camera.forward * maxDistance;
                Invoke(nameof(StopGrapple), grappleDelay);

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

        playerInput.GetComponent<PlayerScript>().JumpToPosition(GrabbedObjectPostion.position, highestArcPoint);



       Invoke(nameof(StopGrapple), 1f);

    }

    private void StopGrapple()
    {
        //lr.positionCount = 0;
        //Destroy(joint);

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;

        Debug.Log("stop grapple");
    }

    private void DrawRope()
    {
        /*if (!joint) return;
        
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, grapplePoint);

        Debug.Log("draw rope");*/
    }

}

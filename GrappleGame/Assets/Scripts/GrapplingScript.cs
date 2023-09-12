using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    public GameObject playerInput;
    private bool shoot;
    
    private LineRenderer lr;
    private SpringJoint joint;

    [SerializeField] private new Transform firePoint, camera, player;
    private Vector3 grapplePoint;

    [SerializeField] private float maxDistance = 100f;

    public LayerMask whatIsGrappable;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        shoot = playerInput.GetComponent<PlayerScript>().shoot;

        if (shoot)
        {
            Debug.Log("hallo");
            StartGrapple();
        }
        else
        {
            StopGrapple();
        }

        DrawRope();
    }

    private void StartGrapple()
    {
        RaycastHit hit;

        if(Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappable))
        {
            grapplePoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //mess w/ these
            joint.minDistance = distanceFromPoint * 0.1f;
            joint.maxDistance = distanceFromPoint * 0.5f;

            joint.spring = 2f;
            joint.damper = 15f;
            //joint.massScale = -10;

        }
    }

    private void StopGrapple()
    {
        
    }

    private void DrawRope()
    {
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, grapplePoint);
    }

}

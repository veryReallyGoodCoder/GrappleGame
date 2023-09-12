using UnityEngine;

public class GrapplingScript : MonoBehaviour
{

    PlayerScript playerInput;
    
    private LineRenderer lr;

    [SerializeField] private new Transform firePoint, camera, player;
    private Vector3 grapplePoint;

    public LayerMask whatIsGrappable;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        playerInput = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;

    [Header("Movement")]
    private Vector2 moveInput, mouseInput;
    public float moveSpeed = 10f;

    [Header("Camera")]
    public Transform camera;
    public Texture2D mouseTexture;
    [SerializeField] private float lookSpeed = 10f;
    private float xRotation = 0f;

    [Header("Jump")]
    public bool jump;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.5f;
    private bool isGrounded;

    [Header("Grapple")]
    public bool shoot;
    public bool activeGrapple;

    [Header("Boost")]
    public GameObject speedLines;

    private bool boost;
    public float boostSpeed = 20;

    [SerializeField] private float setBoostTimer = 3;
    private float boostTimer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        //Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;

        boostTimer = setBoostTimer;
        speedLines.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        
        //movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
        
        if(moveInput.magnitude > 0 || moveInput.magnitude < 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(jump && isGrounded)
        {
            //Vector3 jumpMove = Vector3.up;
            rb.velocity = Vector3.up * jumpForce * Time.deltaTime;
            Debug.Log(jump);
        }


        //boost
        if (boost && boostTimer > 0)
        {
            //Debug.Log("boost");
            rb.AddForce(transform.forward * boostSpeed * Time.deltaTime, ForceMode.Impulse);
            //rb.velocity = Vector3.forward * boostSpeed * Time.deltaTime;

            boostTimer -= Time.deltaTime;
            //Debug.Log(boostTimer);

            speedLines.SetActive(true);
        }
        else
        {
            speedLines.SetActive(false);
        }


        //player fall
        if(transform.position.y < -200)
        {
            Debug.Log("you died");
            SceneManager.LoadScene("DeathScene");
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void MouseScript(InputAction.CallbackContext ctx)
    {
        mouseInput = ctx.ReadValue < Vector2>() * lookSpeed;

        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.Rotate(Vector3.up * mouseInput.x);

        //Debug.Log(mouseInput);

    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = ctx.action.triggered;
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        /*if (ctx.action.triggered)
        {
            shoot = true;
           // Debug.Log("pew");
        }
        else 
        {
            shoot = false;
        }*/

        shoot = ctx.action.triggered;

    }

    public void OnBoost(InputAction.CallbackContext ctx)
    {
        boost = ctx.action.triggered;
        boostTimer = setBoostTimer;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;
        
        rb.velocity = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
    }


    // grapple jump script
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            Debug.Log("you died");

            SceneManager.LoadScene("DeathScene");
            Cursor.lockState = CursorLockMode.None;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickupable pickupItem = other.GetComponent<IPickupable>();
        if(pickupItem != null)
        {
            pickupItem.OnPickup(this);
        }
    }

}

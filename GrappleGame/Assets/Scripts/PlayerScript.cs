using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;

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

    [Header("Grapple")]
    public bool shoot;
    public bool activeGrapple;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);

        if(jump)
        {
            //Vector3 jumpMove = Vector3.up;
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
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
        if (ctx.action.triggered)
        {
            shoot = true;
            //Debug.Log("pew");
        }
        else 
        {
            shoot = false;
        }
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

}

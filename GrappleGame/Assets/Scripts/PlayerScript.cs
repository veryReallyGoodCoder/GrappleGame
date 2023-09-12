using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    
    private Vector2 moveInput, mouseInput;
    public float moveSpeed = 10f;

    public Transform camera;
    public Texture2D mouseTexture;
    [SerializeField] private float lookSpeed = 10f;
    private float xRotation = 0f;

    public bool shoot;


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

        Debug.Log(mouseInput);

    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            shoot = true;
            Debug.Log("pew");
        }
    }

}

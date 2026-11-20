using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jump = 5f;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 0.5f;
    private float gravityValue = -9.81f;
    private float verticalVelocity;
    private Vector2 moveInput;
    private CharacterController controller;
    private bool isGrounded;
    public Transform cam;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        movementAction.action.Enable();
        jumpAction.action.Enable();
    }

    void OnDisable()
    {
        movementAction.action.Disable();
        jumpAction.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        moveInput = movementAction.action.ReadValue<Vector2>();

        Vector3 camForward = cam.forward; camForward.y = 0f; camForward.Normalize();
        Vector3 camRight = cam.right; camRight.y = 0f; camRight.Normalize();

        Vector3 direction = (camForward * moveInput.y + camRight * moveInput.x) * speed;

        if (isGrounded && jumpAction.action.WasPressedThisFrame())
            verticalVelocity = jump;

        verticalVelocity += gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        controller.Move(direction * Time.deltaTime);
    }
}

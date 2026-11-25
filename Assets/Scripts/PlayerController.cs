using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private PlayerInput pi;
    [SerializeField] private float walkspeed = 1f;
    [SerializeField] private float lookSensitivity = 1f;
    [SerializeField] private float YLookMultiplier = 1f;
    private InputAction moveAction;
    private InputAction lookAction;
    private GameObject camera;
    private float CameraXRotation = 0f;
    [SerializeField] private float gravity = 9.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        pi = GetComponent<PlayerInput>();
        moveAction = pi.actions["Move"];
        lookAction = pi.actions["Look"];
        camera = transform.Find("Main Camera").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        cc.Move(transform.TransformVector(new Vector3(moveValue.x * walkspeed, -gravity, moveValue.y * walkspeed)));
        Vector2 lookValue = lookAction.ReadValue<Vector2>();
        transform.Rotate(0f, lookValue.x*lookSensitivity, 0f);
        CameraXRotation = Mathf.Clamp(CameraXRotation - lookValue.y*YLookMultiplier, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(CameraXRotation, 0f, 0f);
    }
}

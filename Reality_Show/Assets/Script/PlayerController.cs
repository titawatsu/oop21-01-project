using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float rbDrag = 6.0f;
    [SerializeField] float mouseSensitivity = 3.0f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float moveMultiplier = 5.0f;
 
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    private float cameraPitch = 0.0f;
    private float horizontalMovement;
    private float verticalMovement;

    [SerializeField] bool cursorLock = true;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cursorLock){

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update(){

        MoveInput();
        UpdateCam();
        ControlDrag();

    }
    private void UpdateCam(){

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -85.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void MoveInput(){

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

    }

    private void FixedUpdate(){
        rb.AddForce(moveDirection.normalized * walkSpeed * moveMultiplier, ForceMode.Acceleration);
    }

    void ControlDrag(){

        rb.drag = rbDrag;
    }

    /*
    # Movement using CharacterController

    void UpdateMoveInput(){
        
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded){

            velocityY = 0.0f;
        }
            
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }*/

}

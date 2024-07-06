using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera; // Assign this in the inspector
    public float gravity = -9.81f;
    public float interactDistance = 3f;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private Vector3 velocity;
    private InteractiveObject heldObject = null;

    void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
    }

    void Update() {
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        // Apply gravity
        if (characterController.isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Small value to ensure the player stays grounded
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Interaction
        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    void Interact() {
        if (heldObject != null) {
            Vector3 dropPosition = transform.position + transform.forward * 2;
            heldObject.PutDown(dropPosition);
            heldObject = null;
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactDistance)) {
            InteractiveObject interactiveObject = hit.collider.GetComponent<InteractiveObject>();
            if (interactiveObject != null) {
                interactiveObject.PickUp();
                heldObject = interactiveObject;
            }
        }
    }
}

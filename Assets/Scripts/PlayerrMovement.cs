using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerrMovement : MonoBehaviour
{
   [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Degrees per second
    public float jumpForce = 5f;

    [Header("Camera Settings")]
    public Transform cameraTransform;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        MovePlayer();
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Direction relative to camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignore camera's vertical tilt
        forward.y = 0f;
        right.y = 0f;

       Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

       Vector3 targetVelocity = moveDirection * moveSpeed;
       Vector3 velocityChange = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);

       rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }
}
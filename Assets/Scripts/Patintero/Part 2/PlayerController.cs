using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
   public float moveSpeed = 5f;
    public float gravity = -9.81f;
    CharacterController cc;
    Vector3 velocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = forward * v + right * h;
        if (move.magnitude > 1f) move.Normalize();

        cc.Move(move * moveSpeed * Time.deltaTime);

        // simple gravity so CharacterController stays grounded
        if (!cc.isGrounded)
            velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0)
            velocity.y = -1f;

        cc.Move(velocity * Time.deltaTime);
    }
}

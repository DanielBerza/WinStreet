using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f);
    public float sensitivity = 3f;
    public float distance = 4f;

    public float rotationY;
    public float rotationX;

    void LateUpdate()
    {
        if(!target) return; 
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -35f, 60f);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 desiredPosition = target.position + rotation * new Vector3(0, offset.y, -distance);
        transform.position = desiredPosition;

        transform.LookAt(target.position + Vector3.up * offset.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFly : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rollSpeed = 2.0f; // Speed of rolling when pressing Q/E
    public float sensitivity = 2.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        // Rotation based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += mouseX * sensitivity;
        rotation.x -= mouseY * sensitivity;
        transform.rotation = Quaternion.Euler(rotation);

        // Translation based on keyboard input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 forward = transform.forward * verticalInput;
        Vector3 right = transform.right * horizontalInput;
        moveDirection = (forward + right).normalized;

        // Check for Q or E key presses for rolling movement
        if (Input.GetKey(KeyCode.Q))
        {
            moveDirection += transform.up * rollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveDirection -= transform.up * rollSpeed * Time.deltaTime;
        }

        // Move the character
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

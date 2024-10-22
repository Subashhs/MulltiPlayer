using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 150f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private Camera playerCamera;

    private void Start()
    {
        if (photonView.IsMine)
        {
            // Enable the player's own camera
            playerCamera = Camera.main;
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, 1.5f, -2f); // Adjust the camera position behind the player
        }

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (photonView.IsMine) // Only allow local player to control
        {
            HandleMovement();
            HandleRotation();
            HandleJump();
        }
    }

    void HandleMovement()
    {
        float v = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, v);
    }

    void HandleRotation()
    {
        float h = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, h, 0);

        // Mouse input for camera control (optional)
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

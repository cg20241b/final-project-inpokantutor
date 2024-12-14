using UnityEngine;

public class PlayerMovementRigidbody : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 5f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Periksa apakah pemain menyentuh tanah
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);

        // Gerakan horizontal (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);

        // Lompatan (spasi)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

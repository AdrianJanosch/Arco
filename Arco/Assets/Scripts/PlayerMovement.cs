using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // --- Settings ---
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Transform cam; // Kamera-Referenz

    // --- Internal ---
    private CharacterController player;
    private Vector3 moveDirection;
    private Vector3 velocity; // Für Vertikalbewegung
    public bool isGrounded;

    void Start()
    {
        player = GetComponent<CharacterController>();

        if (cam == null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // --- Ground Check ---
        isGrounded = player.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // hält den Spieler stabil am Boden
        }

        // --- Input ---
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // --- Richtung relativ zur Kamera ---
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // --- Zielrichtung berechnen ---
        Vector3 desiredMove = (camForward * vertical + camRight * horizontal).normalized;

        // --- Bewegung & Rotation nur bei Input ---
        if (desiredMove.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float currentSpeed = isRunning ? runSpeed : moveSpeed;
            moveDirection = desiredMove * currentSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // --- Sprung ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // klassische Jump-Physik
        }

        // --- Gravitation ---
        velocity.y += gravity * Time.deltaTime;

        // --- Bewegung anwenden ---
        player.Move((moveDirection + velocity) * Time.deltaTime);
    }
}
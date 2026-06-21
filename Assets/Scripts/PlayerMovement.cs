using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Set to true in the Inspector if horizontal/vertical input should be swapped
    public bool swapControls = false;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Reseting the default velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Getting the input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Moving the player (allow swapping if controls are reversed in the Input settings)
        Vector3 move;
        if (swapControls)
        {
            move = transform.right * vertical + transform.forward * horizontal;
        }
        else
        {
            move = transform.right * horizontal + transform.forward * vertical;
        }

        //Actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Actully jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Falling down
        velocity.y += gravity * Time.deltaTime;

        //Executing the jump
        controller.Move(velocity * Time.deltaTime);

        lastPosition = gameObject.transform.position;
    }
}

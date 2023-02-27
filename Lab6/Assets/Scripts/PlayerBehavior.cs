using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHieght = 3.0f;
    public Vector3 velocity;
    [Header("Jump")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

  
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }
    // Start is called before the first frame update
   private void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        controller.Move(move * maxSpeed * Time.deltaTime);



        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHieght * -2.0f * gravity);
            AudioController.Instance.PlaySFXAudio("Hop");
        }

        velocity.y = gravity += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathArea"))
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = startPosition;
            GetComponent<CharacterController>().enabled = true;
        }
    }
}

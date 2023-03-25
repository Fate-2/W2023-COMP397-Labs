using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [Header("Player Data")]
    public string playerName;
    public int playerHealth;
    public int playLevel;

    [Header("Character Controller")]
    public CharacterController controller;

    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Jump")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Health")]
    [SerializeField] private HealthSystem _healthSystem;
    
    public Vector3 startPosition;
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

        // Movement Section
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * y;
        controller.Move(move * maxSpeed * Time.deltaTime);

        // Jump Section
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2.0f * gravity);
            AudioController.Instance.PlaySFXAudio("Hop");
        }
        velocity.y += gravity * Time.deltaTime;
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

    private void OntriggerStay(Collider other)
    {
        if (other.CompareTag("DamageArea"))
        {
            StartCoroutine(DoDamageOverTime());
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DamageArea"))
        {
            StopCoroutine(DoDamageOverTime());
        }
    }



    private IEnumerator DoDamageOverTime()
    {
        int counter = 3;
       while (counter > 0)
        {
            _healthSystem.Damage();
            yield return new WaitForSeconds(2f);
            counter--;
        }

        yield return null;
        
    }


    public void SaveButton_Pressed()
    {
        SaveSystem.SavePlayer(this.GetComponent<PlayerBehavior>());
    }
    public void LoadButton_Pressed()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            playerName = data.name;
            playerHealth = data.health;
            playLevel = data.level;
        }
    }
}
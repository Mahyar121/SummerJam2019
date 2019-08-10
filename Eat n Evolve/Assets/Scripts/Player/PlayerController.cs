using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    // character top down movement
    private Vector3 inputMovement;
    private Vector3 startPosition;
    // Player properties
    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidBody2D { get; set; }
    public Transform MyTransform { get; set; }

    // Creates a singleton of the Player so we dont make multiple instances of the player
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<PlayerController>(); }
            return instance;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        MyTransform = GetComponent<Transform>();
        MyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Put anything non physics related that needs updating here
    private void Update()
    {
        HandleInput();
    }

    // Put anything physics related that needs updating here
    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Will handle the top down movement of the player
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MyRigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void HandleInput()
    {
        // This is the attack
        if (Input.GetKeyDown(KeyCode.Space)) {  }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    // character top down movement
    private Vector3 inputMovement;
    private Vector3 startPosition;
    // Player properties
    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidBody2D { get; set; }
    public Transform MyTransform { get; set; }

    public bool HasClaws { get; set; }
    public bool HasHorns { get; set; }
    public bool HasSpikes { get; set; }

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
        Initialize();
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

    public override void Initialize()
    {
        RandomCharacterTraitSelection();
    }

    // Will handle the top down movement of the player
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MyRigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
    }

    private void HandleInput()
    {
        // This is the attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if the player has claws do the claws animation
            if (Instance.HasClaws)
            {
                Debug.Log("Striked with claws!");
            }
            // if player has horns do the horns animation
            if (Instance.HasHorns)
            {
                Debug.Log("Striked with horns!");
            }
            // if player has spikes do the spikes animation
            if (Instance.HasSpikes)
            {
                Debug.Log("Striked with spikes!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public override void Death()
    {
        Initialize();
    }

    // Randomly selects a character trait for the player
    public void RandomCharacterTraitSelection()
    {
        Instance.HasClaws = false;
        Instance.HasHorns = false;
        Instance.HasSpikes = false;

        int characterTraitState = Random.Range(1, 4);
        Debug.Log("Trait Number:" + characterTraitState);
        switch (characterTraitState) {
            case 1:
                Instance.HasClaws = true;
                Debug.Log("Player got claws!");
                break;
            case 2:
                Instance.HasHorns = true;
                Debug.Log("Player got horns!");
                break;
            case 3:
                Instance.HasSpikes = true;
                Debug.Log("Player got spikes!");
                break;
            default:
                Instance.HasClaws = true;
                Debug.Log("Defaulted the player to claws!");
                break;
        }
      
    }
}

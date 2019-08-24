using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : Character
{

    [SerializeField] private EdgeCollider2D meleeAttackCollider;
    [SerializeField] private float meleeRange = 50f;

    public SpriteRenderer EnemySpriteRenderer { get; set; }
    public GameObject Target { get; set; }
    public Rigidbody2D MyRigidbody { get; set; }
    public Animator MyAnimator { get; set; }
    public EdgeCollider2D MeleeAttackCollider { get { return meleeAttackCollider; } }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public bool IsDead {  get { return health <= 0; } }

    private float wanderTimer;
    private float wanderTimerSpeed;
    private int wanderTimerState;
    private int wanderTimerMax;
    private float idleTimer;
    private float idleDuration;
    private float movingTimer;
    private float movingDuration;
    private Vector2 randomDirection;
    bool isMoving;
    bool isIdle;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //randomDirection = new Vector2(0, 0);
        //MyRigidBody2D = GetComponent<Rigidbody2D>();
        //isIdle = true;
        //isMoving = false;
        //TakingDamage = false;
        //Attack = false;
        //wanderTimer = 0;
        //wanderTimerSpeed = 20f;
        //wanderTimerState = 0;
        //wanderTimerMax = 1;
        //idleTimer = 0;
        //idleDuration = Random.Range(1, 10);
        //movingTimer = 0;
        //movingDuration = Random.Range(1, 10);
    }

    private void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                ChangeState();
                Wander();
            }
        }
    }

    private void Wander()
    {
        if(wanderTimerState == 0)
        {
            wanderTimer += 0.1f * Time.deltaTime * wanderTimerSpeed;
            if (wanderTimer >= wanderTimerMax)
            {
                wanderTimer = 0;
                if (isMoving)
                {
                    int movDir = Random.Range(0, 8);
                    RandomizeAIDirection(movDir);

                }
                else if (isIdle)
                {
                    randomDirection.x = 0;
                    randomDirection.y = 0;
                }
            }
            // Movement
            float velocityX = randomDirection.x * Time.deltaTime * movementSpeed;
            float velocityY = randomDirection.y * Time.deltaTime * movementSpeed;
            transform.Translate(velocityX, velocityY, 0); 
        }


    }

    private void ChangeState()
    {
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                Debug.Log("Is Moving Now");
                isMoving = true;
                isIdle = false;
                idleTimer = 0;
            }
        }
        if (isMoving)
        {
            movingTimer += Time.deltaTime;
            if (movingTimer >= movingDuration)
            {
                Debug.Log("Is Idle Now");
                isIdle = true;
                isMoving = false;
                movingTimer = 0;
            }
        }

    }

    // Randomizes direction AI goes
    private void RandomizeAIDirection(int movementDirection)
    {
        Vector2 tempVector2 = new Vector2(0, 0);
        // Up
        if (movementDirection == 0)
        {
            tempVector2.x = 0;
            tempVector2.y = 1;
        }
        // Down
        if (movementDirection == 1)
        {
            tempVector2.x = 0;
            tempVector2.y = -1;
        }
        // Left
        if (movementDirection == 2)
        {
            tempVector2.x = -1;
            tempVector2.y = 0;
        }
        // Right
        if (movementDirection == 3)
        {
            tempVector2.x = 1;
            tempVector2.y = 0;
        }
        // UpRight
        if (movementDirection == 4)
        {
            tempVector2.x = 1;
            tempVector2.y = 1;
        }
        // DownRight
        if (movementDirection == 5)
        {
            tempVector2.x = 1;
            tempVector2.y = -1;
        }
        // DownLeft
        if (movementDirection == 6)
        {
            tempVector2.x = -1;
            tempVector2.y = -1;
        }
        // UpLeft
        if (movementDirection == 7)
        {
            tempVector2.x = -1;
            tempVector2.y = 1;
        }

        randomDirection.x = tempVector2.x;
        randomDirection.y = tempVector2.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("I took " + PlayerController.Instance.Damage);
        }
    }

    public override void Death()
    {

    }

    public override void Initialize()
    {
        EnemySpriteRenderer = GetComponent<SpriteRenderer>();
        MyAnimator = GetComponent<Animator>();
    }

}
